using System.Security.Claims;
using System.Threading.Tasks;
using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Security;
using CleanArc.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CleanArc.Web.Api.Authorization;

/// <summary>
/// Declarative permission gate, e.g. [HasPermission("Inventory", PermissionAction.Create)].
/// Users in the 'admin' role bypass the check (super-admin). Unauthenticated → 401,
/// authenticated-but-unauthorized → 403.
/// </summary>
public sealed class HasPermissionAttribute : TypeFilterAttribute
{
    public HasPermissionAttribute(string feature, PermissionAction action)
        : base(typeof(HasPermissionFilter))
    {
        Arguments = new object[] { feature, action };
    }
}

public sealed class HasPermissionFilter : IAsyncAuthorizationFilter
{
    private readonly string _feature;
    private readonly PermissionAction _action;
    private readonly IUserPermissionProvider _provider;

    public HasPermissionFilter(string feature, PermissionAction action, IUserPermissionProvider provider)
    {
        _feature = feature;
        _action = action;
        _provider = provider;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        if (user?.Identity?.IsAuthenticated != true)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        // Super-admin bypass.
        if (user.IsInRole(Roles.Admin))
            return;

        var idStr = user.FindFirstValue(ClaimTypes.NameIdentifier) ?? user.Identity?.Name;
        if (!int.TryParse(idStr, out var userId))
        {
            context.Result = Forbidden();
            return;
        }

        var set = await _provider.GetForUserAsync(userId);
        if (!set.Has(_feature, _action))
            context.Result = Forbidden();
    }

    private static IActionResult Forbidden() =>
        new ObjectResult(new { Message = "You do not have permission to perform this action.", StatusCode = 403, IsSuccess = false })
        {
            StatusCode = 403
        };
}
