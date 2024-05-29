using System.Security.Claims;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using CleanArc.WebFramework.Filters;
using Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Asp.Versioning;

namespace CleanArc.WebFramework.BaseController;
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class GenericBaseController<TModel, TResult> : ControllerBase where TModel : IRequest<TResult>
{
    protected ISender Sender { get; }
    protected string UserName => User.Identity?.Name;
    protected int UserId => int.Parse(User.Identity.GetUserId());
    protected string UserEmail => User.Identity.FindFirstValue(ClaimTypes.Email);
    protected string UserRole => User.Identity.FindFirstValue(ClaimTypes.Role);
    protected string UserKey => User.FindFirstValue(ClaimTypes.UserData);

    public GenericBaseController(ISender sender)
    {
        Sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> HandleCommand(TModel model)
    {
        var commandResult = await Sender.Send(model);
        return OperationResult(commandResult);
    }

    protected IActionResult OperationResult(TResult result)
    {
        // Implement your custom logic for handling the result here
        // For example, return Ok(result), BadRequest(error), etc.

        return Ok(result);
    }

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