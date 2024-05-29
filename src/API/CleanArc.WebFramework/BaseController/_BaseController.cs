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
using Microsoft.Extensions.Logging;
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
[Authorize]
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
    [HttpPost("GetAll")]
    public async Task<IActionResult> GetAll([FromBody] TQuery model)
    {
        //dynamic query = Activator.CreateInstance(typeof(TQuery));
        ////var query = Activator.CreateInstance(typeof(TQuery)) as IQuery<OperationResult<List<TQueryResult>>>;
        ////string actionName = "GetAll";
        ////_logger.LogInformation("Executing generic implementation of {@actionName} action in {@controllerName} with query {@query}", actionName, controllerName, Activator.CreateInstance(typeof(TQuery)));
        var result = await _sender.Send(model);
        ////_logger.LogInformation("Executed generic implementation of {@actionName} action in {@controllerName}", actionName, controllerName);

        
        return OperationResult(result);
    }

    /// <summary>
    /// Gets an entity by its ID.
    /// </summary>
    /// <param name="id">The ID of the entity.</param>
    /// <returns>An action result representing the operation result.</returns>
    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        dynamic query = Activator.CreateInstance(typeof(TByIdQuery));
        query.Id = id;
        var result = await _sender.Send(query);
        return OperationResult(result);
    }

    /// <summary>
    /// Creates a new entity.
    /// </summary>
    /// <param name="model">The model for creating the entity.</param>
    /// <returns>An action result representing the operation result.</returns>
    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] TCreateCommand model)
    {
        SetUserId(model);
        var commandResult = await _sender.Send(model);
        return OperationResult(commandResult);
    }

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="model">The model for updating the entity.</param>
    /// <returns>An action result representing the operation result.</returns>

    [HttpPost("Update")]
    public async Task<IActionResult> Update([FromBody] TUpdateCommand model)
    {
        SetUserId(model);
        var commandResult = await _sender.Send(model);
        return OperationResult(commandResult);
    }

    /// <summary>
    /// Deletes an entity.
    /// </summary>
    /// <param name="model">The model for deleting the entity.</param>
    /// <returns>An action result representing the operation result.</returns>
    [HttpPost("Delete")]
    public async Task<IActionResult> Delete([FromBody] TDeleteCommand model)
    {
        SetUserId(model);
        var commandResult = await _sender.Send(model);
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
        if (result is null)
            return new ServerErrorResult("Server Error");


        if (result.IsSuccess) return result.Result is bool ? Ok() : Ok(result.Result);

        if (result.IsNotFound)
        {

            ModelState.AddModelError("GeneralError", result.ErrorMessage);

            var notFoundErrors = new ValidationProblemDetails(ModelState);

            return NotFound(notFoundErrors.Errors);
        }

        ModelState.AddModelError("GeneralError", result.ErrorMessage);

        var badRequestErrors = new ValidationProblemDetails(ModelState);

        return BadRequest(badRequestErrors.Errors);

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