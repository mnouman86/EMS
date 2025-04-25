using System.Security.Claims;
using Asp.Versioning;
using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Entities.User;
using CleanArc.SharedKernel.Extensions;
using CleanArc.WebFramework.Filters;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging; using CleanArc.Domain.Common;
using Serilog.Context;
using CleanArc.Application.Models.Request;


namespace CleanArc.WebFramework.BaseController;
/// <summary>
/// Base controller providing generic CRUD operations for entities.
/// </summary>
/// <typeparam name="TCreateCommand">Type of the command for creating entities.</typeparam>
/// <typeparam name="TUpdateCommand">Type of the command for updating entities.</typeparam>
/// <typeparam name="TDeleteCommand">Type of the command for deleting entities.</typeparam>
/// <typeparam name="TResult">Type representing the result of operations.</typeparam>
/// <typeparam name="TQuery">Type of the query for retrieving entities.</typeparam>
/// <typeparam name="TQueryResult">Type representing the result of query operations.</typeparam>
/// <typeparam name="TByIdQuery">Type of the query for retrieving entities by ID.</typeparam>
/// <typeparam name="TByIdQueryResult">Type representing the result of query operations by ID.</typeparam>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
//[Authorize]
public class _BaseController<TCreateCommand, TUpdateCommand, TDeleteCommand, TResult, TQuery, TQueryResult, TByIdQuery, TByIdQueryResult> : ControllerBase
    where TCreateCommand : IRequest<OperationResult<TResult>>
    where TUpdateCommand : IRequest<OperationResult<TResult>>
    where TDeleteCommand : IRequest<OperationResult<TResult>>
    //where TQuery : IRequest<OperationResult<TQueryResult>>
    where TQuery : IRequest<OperationResult<TQueryResult>>
    where TByIdQuery : IRequest<OperationResult<TByIdQueryResult>>
    //ControllerBase where TCommand : IRequest<TResult>
{
    /// <summary>
    /// The sender
    /// </summary>
    private readonly ISender _sender;
    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<_BaseController<TCreateCommand, TUpdateCommand, TDeleteCommand, TResult, TQuery, TQueryResult, TByIdQuery, TByIdQueryResult>> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor

    /// <summary>
    /// Initializes a new instance of the <see cref="_BaseController{TCreateCommand, TUpdateCommand, TDeleteCommand, TResult, TQuery, TQueryResult, TByIdQuery, TByIdQueryResult}"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    protected _BaseController(ISender sender,ILogger<_BaseController<TCreateCommand, TUpdateCommand, TDeleteCommand, TResult, TQuery, TQueryResult, TByIdQuery, TByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor)
    {
        _sender = sender;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        LogContext.PushProperty("UserId", _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "anonymous");
        string token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        LogContext.PushProperty("Token", token); // Push the token to the log context

        _httpContextAccessor = httpContextAccessor;
    }
    /// <summary>
    /// Gets all entities.
    /// </summary>
    /// <returns>An action result representing the operation result.</returns>
    //[Authorize]
    [HttpPost("[controller]GetAll")]
    public async Task<IActionResult> GetAll([FromBody] TQuery query)
    {
        //dynamic query = Activator.CreateInstance(typeof(TQuery));
        ////var query = Activator.CreateInstance(typeof(TQuery)) as IQuery<OperationResult<List<TQueryResult>>>;
        ////string actionName = "GetAll";
        ////_logger.LogInformation("Executing generic implementation of {@actionName} action in {@controllerName} with query {@query}", actionName, controllerName, Activator.CreateInstance(typeof(TQuery)));
        var result = await _sender.Send(query);
        ////_logger.LogInformation("Executed generic implementation of {@actionName} action in {@controllerName}", actionName, controllerName);

        
        return OperationResult(result);
    }

    /// <summary>
    /// Gets an entity by its ID.
    /// </summary>
    /// <param name="id">The ID of the entity.</param>
    /// <returns>An action result representing the operation result.</returns>
    [HttpPost("[controller]GetById")]
    public async Task<IActionResult> GetById([FromBody] TByIdQuery query)
    {
        //dynamic query = Activator.CreateInstance(typeof(TByIdQuery));
        //query.Id = id;
        var result = await _sender.Send(query);
        return OperationResult(result);
    }

    /// <summary>
    /// Creates a new entity.
    /// </summary>
    /// <param name="model">The model for creating the entity.</param>
    /// <returns>An action result representing the operation result.</returns>
    /// 
   [Authorize]
    [HttpPost("[controller]Create")]
    public async Task<IActionResult> Create([FromBody] TCreateCommand command)
    {
        SetUserId(command);
        var commandResult = await _sender.Send(command);
        return OperationResult(commandResult);
    }

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="model">The model for updating the entity.</param>
    /// <returns>An action result representing the operation result.</returns>
    [Authorize]
    [HttpPost("[controller]Update")]
    public async Task<IActionResult> Update([FromBody] TUpdateCommand command)
    {
        SetUserId(command);
        var commandResult = await _sender.Send(command);
        return OperationResult(commandResult);
    }

    /// <summary>
    /// Deletes an entity.
    /// </summary>
    /// <param name="model">The model for deleting the entity.</param>
    /// <returns>An action result representing the operation result.</returns>
    /// 
    [Authorize]
    [HttpPost("[controller]Delete")]
    public async Task<IActionResult> Delete([FromBody] TDeleteCommand command)
    {
        SetUserId(command);
        var commandResult = await _sender.Send(command);
        return OperationResult(commandResult);
    }
    /// <summary>
    /// Sets the user ID on the command model.
    /// </summary>
    /// <param name="model">The dynamic model to set the user ID on.</param>
    protected virtual void SetUserId(dynamic model)
    {
        // Implement the logic to set UserId on the command model if needed
        model.UserId = int.Parse(User.Identity.GetUserId());

    }
    /// <summary>
    /// Handles the operation result and returns an appropriate action result.
    /// </summary>
    /// <param name="result">The result to handle.</param>
    /// <returns>An action result representing the operation result.</returns>
    protected IActionResult OperationResult(TResult result)
    {
        // Implement your custom logic for handling the result here
        // For example, return Ok(result), BadRequest(error), etc.
       
        return Ok(result);
    }
    /// <summary>
    /// Handles the operation result and returns an appropriate action result.
    /// </summary>
    /// <typeparam name="TModel">Type of the model in the operation result.</typeparam>
    /// <param name="result">The result to handle.</param>
    /// <returns>An action result representing the operation result.</returns>
    protected IActionResult OperationResult<TModel>(OperationResult<TModel> result)
    {
        //if (result is null)
        //    return new ServerErrorResult("Server Error");
        if (result is null)
            return StatusCode(500, new { Message= "Server Error" });

        //if (result.IsSuccess) return result.Result is bool ? Ok() : Ok(result);
        //if (result.IsSuccess)
        //{
            object data = result.Result;

            // Check if result.Result is not null and is of type CleanArc.Domain.Common.ResponseEntity
            if (result.Result is CleanArc.Domain.Common.ResponseEntity responseEntity && responseEntity != null)
            {
                data =new { RecordID = responseEntity.RecordID }; // Assign RecordID instead of the whole object
            }
            var successResponse = new
            {
                //Data = result.Result,
                Data = data,
                Message=result.Message, // Use custom success message
                StatusCode=result.StatusCode,
                IsSuccess=result.IsSuccess,
                TotalCount=result.TotalCount,
            };

            return result.Result is bool ? Ok() : StatusCode(result.StatusCode, successResponse);
        //}
        //if (result.IsNotFound || result.Result==null)
        //{

        //   // ModelState.AddModelError("404 Not Found", result.ErrorMessage);

        //    //var notFoundErrors = new ValidationProblemDetails(ModelState);

        //    //return NotFound(notFoundErrors.Errors);
        //    return Ok(result);
        //}
        //if (result.IsNotFound || result.Result == null)
        //{
        //    var failResponse = new
        //    {
        //        Message = result.ErrorMessage, // Use custom success message
        //        StatusCode = result.StatusCode,
        //        IsSuccess = false
        //    };
        //    return StatusCode(result.StatusCode,failResponse);
        //}
        //ModelState.AddModelError("GeneralError", result.ErrorMessage);

        //var badRequestErrors = new ValidationProblemDetails(ModelState);

        //return StatusCode(result.StatusCode,new { Message = result.ErrorMessage, StatusCode = result.StatusCode });

    }
    /// <summary>
    /// Gets the name of the user.
    /// </summary>
    /// <value>
    /// The name of the user.
    /// </value>
    protected string UserName => User.Identity?.Name;
    /// <summary>
    /// Gets the user identifier.
    /// </summary>
    /// <value>
    /// The user identifier.
    /// </value>
    protected int UserId => int.Parse(User.Identity.GetUserId());
    /// <summary>
    /// Gets the user email.
    /// </summary>
    /// <value>
    /// The user email.
    /// </value>
    protected string UserEmail => User.Identity.FindFirstValue(ClaimTypes.Email);
    /// <summary>
    /// Gets the user role.
    /// </summary>
    /// <value>
    /// The user role.
    /// </value>
    protected string UserRole => User.Identity.FindFirstValue(ClaimTypes.Role);

    /// <summary>
    /// Gets the user key.
    /// </summary>
    /// <value>
    /// The user key.
    /// </value>
    protected string UserKey => User.FindFirstValue(ClaimTypes.UserData);

    ////public UserRepository UserRepository { get; set; } => property injection
    /// <summary>
    /// Adds errors from IdentityResult to the model state.
    /// </summary>
    /// <param name="result">The result containing errors.</param>

    protected void AddErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }

    
}