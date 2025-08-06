using System.Security.Claims;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using CleanArc.WebFramework.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.WebFramework.BaseController;

public class BaseController : ControllerBase
{
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


        if (result.IsSuccess) return result.Result is bool ? StatusCode(result.StatusCode, 
            new { Message = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(result.Message.ToLower()), StatusCode = result.StatusCode, result.IsSuccess, ErrorCore = result.ErrorCode }) 
                : Ok(result.Result);

        if (result.IsNotFound)
        {

            ModelState.AddModelError("GeneralError", result.ErrorMessage);

            var notFoundErrors = new ValidationProblemDetails(ModelState);

            return NotFound(notFoundErrors.Errors);
        }

        //ModelState.AddModelError("GeneralError", result.ErrorMessage);

        //var badRequestErrors = new ValidationProblemDetails(ModelState);

        //return BadRequest(badRequestErrors.Errors);
        return StatusCode(result.StatusCode, new { Message = result.ErrorMessage==null?result.Message:result.ErrorMessage, StatusCode = result.StatusCode, ErrorCore=result.ErrorCode });

    }

    protected virtual void SetUserId(dynamic model)
    {
        // Implement the logic to set UserId on the command model if needed
        // model.UserId = int.Parse(User?.Identity?.GetUserId());
        var userIdStr = User?.Identity?.GetUserId();
        if (int.TryParse(userIdStr, out var userId))
        {
            model.UserId = userId;
        }
        else
        {
            // Handle case where userId is null or not a valid int
            model.UserId = 0; // or throw/log/error depending on your use case
        }
    }
}