using Asp.Versioning;
using CleanArc.Application.Features.StaffAttendance;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Security;
using CleanArc.Domain.Common;
using CleanArc.Web.Api.Authorization;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CleanArc.Web.Api.Controllers.V1.StaffAttendance;

/// <summary>
/// Staff Attendance — self-service check-in / check-out for ANY authenticated
/// user (their own timesheet, unrelated to the student-attendance module).
/// Overview + reporting endpoints stay restricted to admin + principal.
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

    /* ---------- Self endpoints (any authenticated user, their own timesheet) ---------- */

    [Authorize, HttpPost("StaffAttendanceGetMyToday")]
    public async Task<IActionResult> MyToday([FromBody] GetMyStaffAttendanceTodayQuery q)
    {
        q.CallerUserId = CurrentUserId;
        return Wrap(await _sender.Send(q));
    }

    [Authorize, HttpPost("StaffAttendanceCheckIn")]
    public async Task<IActionResult> CheckIn([FromBody] StaffCheckInCommand cmd)
    {
        cmd.CallerUserId = CurrentUserId;
        return Wrap(await _sender.Send(cmd));
    }

    [Authorize, HttpPost("StaffAttendanceCheckOut")]
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

    /* ---------- Overview (permission-matrix gated) ---------- */
    [HasPermission("StaffAttendanceOverview", PermissionAction.Read), HttpPost("StaffAttendanceGetOverview")]
    public async Task<IActionResult> Overview([FromBody] GetStaffAttendanceOverviewQuery q)
        => Wrap(await _sender.Send(q));

}
