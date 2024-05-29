using System.Security.Claims;
using Asp.Versioning;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using CleanArc.WebFramework.Filters;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CleanArc.WebFramework.BaseController;
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public abstract class _BaseController_Back<TCommand, TResult> : ControllerBase where TCommand : IRequest<TResult>
{
    private readonly ISender _sender;

    protected _BaseController_Back(ISender sender)
    {
        _sender = sender;
    }
    [HttpPost("CreateNew")]
    public virtual async Task<IActionResult> CreateNew([FromBody] TCommand model)
    {
        // You may add common logic here if needed before sending the command
        SetUserId(model);
        var commandResult = await _sender.Send(model);
        return OperationResult(commandResult);
    }
    protected virtual void SetUserId(dynamic model)
    {
        // Implement the logic to set UserId on the command model if needed
        model.UserId = int.Parse(User.Identity.GetUserId());
    }
    protected IActionResult OperationResult(TResult result)
    {

        var str=result.GetType();
        string val = str.Name;
        
        // Implement your custom logic for handling the result here
        // For example, return Ok(result), BadRequest(error), etc.
       
        return Ok(result);
    }
    protected string UserName => User.Identity?.Name;
    protected int UserId => int.Parse(User.Identity.GetUserId());
    protected string UserEmail => User.Identity.FindFirstValue(ClaimTypes.Email);
    protected string UserRole => User.Identity.FindFirstValue(ClaimTypes.Role);

    protected string UserKey => User.FindFirstValue(ClaimTypes.UserData);

    //public UserRepository UserRepository { get; set; } => property injection
    protected void AddErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }

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
}