using Asp.Versioning;
using CleanArc.Application.Features.StaffAttendance;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CleanArc.Web.Api.Controllers.V1.StaffAttendance;

/// <summary>
/// Staff Attendance — non-admin users check in/out for the current day.
/// Admin is excluded from check-in by role attribute on the write endpoints;
/// admin CAN see the overview.
/// </summary>
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/StaffAttendance")]
public class StaffAttendanceController : ControllerBase
{
    private readonly ISender _sender;
    public StaffAttendanceController(ISender sender) { _sender = sender; }

    private int CurrentUserId
        => int.TryParse(User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? User?.Identity?.Name, out var id) ? id : 0;

    private IActionResult Wrap<T>(OperationResult<T> r)
    {
        if (r is null) return StatusCode(500, new { Message = "Server Error" });
        return StatusCode(r.StatusCode, new { Data = r.Result, r.Message, r.StatusCode, r.IsSuccess, r.TotalCount });
    }

    /* ---------- Self endpoints (any authenticated non-admin) ---------- */
    private const string NonAdminStaff = Roles.Principal + "," + Roles.Accountant + "," + Roles.Teacher;

    [Authorize, HttpPost("StaffAttendanceGetMyToday")]
    public async Task<IActionResult> MyToday([FromBody] GetMyStaffAttendanceTodayQuery q)
    {
        q.CallerUserId = CurrentUserId;
        return Wrap(await _sender.Send(q));
    }

    [Authorize(Roles = NonAdminStaff), HttpPost("StaffAttendanceCheckIn")]
    public async Task<IActionResult> CheckIn([FromBody] StaffCheckInCommand cmd)
    {
        cmd.CallerUserId = CurrentUserId;
        return Wrap(await _sender.Send(cmd));
    }

    [Authorize(Roles = NonAdminStaff), HttpPost("StaffAttendanceCheckOut")]
    public async Task<IActionResult> CheckOut([FromBody] StaffCheckOutCommand cmd)
    {
        cmd.CallerUserId = CurrentUserId;
        return Wrap(await _sender.Send(cmd));
    }

    [Authorize, HttpPost("StaffAttendanceGetMyHistory")]
    public async Task<IActionResult> MyHistory([FromBody] GetMyStaffAttendanceHistoryQuery q)
    {
        q.CallerUserId = CurrentUserId;
        return Wrap(await _sender.Send(q));
    }

    /* ---------- Overview (admin + principal) ---------- */
    [Authorize(Roles = Roles.AdminOrPrincipal), HttpPost("StaffAttendanceGetOverview")]
    public async Task<IActionResult> Overview([FromBody] GetStaffAttendanceOverviewQuery q)
        => Wrap(await _sender.Send(q));

}
