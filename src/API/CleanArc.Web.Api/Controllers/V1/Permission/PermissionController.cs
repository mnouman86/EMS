using Asp.Versioning;
using CleanArc.Application.Features.Permission.Command;
using CleanArc.Application.Features.Permission.Queries;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CleanArc.Web.Api.Controllers.V1.Permission;

/// <summary>
/// Access Control — feature/permission management (admin-only) + the current
/// user's effective permissions for the SPA.
/// </summary>
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/Permission")]
public class PermissionController : ControllerBase
{
    private readonly ISender _sender;
    public PermissionController(ISender sender) { _sender = sender; }

    private int CurrentUserId
        => int.TryParse(User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? User?.Identity?.Name, out var id) ? id : 0;

    private IActionResult Wrap<T>(OperationResult<T> r)
    {
        if (r is null) return StatusCode(500, new { Message = "Server Error" });
        return StatusCode(r.StatusCode, new { Data = r.Result, r.Message, r.StatusCode, r.IsSuccess, r.TotalCount });
    }

    /* ---------- Management (admin-only) ---------- */

    [Authorize(Roles = Roles.Admin), HttpPost("PermissionGetFeatures")]
    public async Task<IActionResult> GetFeatures() => Wrap(await _sender.Send(new GetFeaturesQuery()));

    [Authorize(Roles = Roles.Admin), HttpPost("PermissionGetUsers")]
    public async Task<IActionResult> GetUsers([FromBody] GetPermissionUsersQuery q) => Wrap(await _sender.Send(q));

    [Authorize(Roles = Roles.Admin), HttpPost("PermissionGetUserGrid")]
    public async Task<IActionResult> GetUserGrid([FromBody] GetUserPermissionGridQuery q) => Wrap(await _sender.Send(q));

    [Authorize(Roles = Roles.Admin), HttpPost("PermissionGetRoleTemplate")]
    public async Task<IActionResult> GetRoleTemplate([FromBody] GetRoleTemplateQuery q) => Wrap(await _sender.Send(q));

    [Authorize(Roles = Roles.Admin), HttpPost("PermissionGetRoles")]
    public async Task<IActionResult> GetRoles() => Wrap(await _sender.Send(new GetRolesQuery()));

    [Authorize(Roles = Roles.Admin), HttpPost("PermissionSaveUser")]
    public async Task<IActionResult> SaveUser([FromBody] SaveUserPermissionsCommand cmd)
    {
        cmd.UserId = CurrentUserId;
        return Wrap(await _sender.Send(cmd));
    }

    /* ---------- Current user (any authenticated) ---------- */

    [Authorize, HttpPost("PermissionGetMine")]
    public async Task<IActionResult> GetMine()
        => Wrap(await _sender.Send(new GetMyPermissionsQuery(CurrentUserId, User.IsInRole(Roles.Admin))));
}
