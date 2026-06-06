using Asp.Versioning;
using CleanArc.Application.Features.Attendance;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CleanArc.Web.Api.Controllers.V1.Attendance;

/// <summary>
/// Student Attendance — monthly summary entry by Admin / Principal / Teacher.
/// Parent-facing reads live on ParentController (ownership-checked).
/// </summary>
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/Attendance")]
public class AttendanceController : ControllerBase
{
    private readonly ISender _sender;
    public AttendanceController(ISender sender) { _sender = sender; }

    private int CurrentUserId
        => int.TryParse(User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? User?.Identity?.Name, out var id) ? id : 0;

    private IActionResult Wrap<T>(OperationResult<T> r)
    {
        if (r is null) return StatusCode(500, new { Message = "Server Error" });
        return StatusCode(r.StatusCode, new { Data = r.Result, r.Message, r.StatusCode, r.IsSuccess, r.TotalCount });
    }

    private const string StaffRoles = Roles.Admin + "," + Roles.Principal + "," + Roles.Teacher;

    /* ---------- Staff: entry grid + bulk save ---------- */

    [Authorize(Roles = StaffRoles), HttpPost("AttendanceGetClassGrid")]
    public async Task<IActionResult> GetGrid([FromBody] GetClassAttendanceGridQuery q) => Wrap(await _sender.Send(q));

    [Authorize(Roles = StaffRoles), HttpPost("AttendanceBulkSave")]
    public async Task<IActionResult> BulkSave([FromBody] BulkSaveClassAttendanceCommand cmd)
    {
        cmd.ChangedBy = CurrentUserId;
        return Wrap(await _sender.Send(cmd));
    }

    /* ---------- Staff / Admin: per-student history + summary (for student profile view) ---------- */

    [Authorize(Roles = StaffRoles), HttpPost("AttendanceGetStudentHistory")]
    public async Task<IActionResult> GetHistory([FromBody] GetStudentAttendanceHistoryQuery q) => Wrap(await _sender.Send(q));

    [Authorize(Roles = StaffRoles), HttpPost("AttendanceGetStudentSummary")]
    public async Task<IActionResult> GetSummary([FromBody] GetStudentAttendanceSummaryQuery q) => Wrap(await _sender.Send(q));
}
