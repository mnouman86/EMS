using Asp.Versioning;
using CleanArc.Application.Features.Attendance;
using CleanArc.Application.Features.Fee.Queries.LedgerQueries;
using CleanArc.Application.Features.Fee.Queries.ReceiptQuery;
using CleanArc.Application.Features.Parent;
using CleanArc.Application.Features.Result.Queries.GetResultSessions;
using CleanArc.Application.Features.Result.Queries.GetStudentResultCard;
using CleanArc.Application.Features.Student.Queries.GetStudentById;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Common;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CleanArc.Web.Api.Controllers.V1.Parent;

public class ChildRequest { public int StudentId { get; set; } }
public class ChildReceiptRequest { public int StudentId { get; set; } public int PaymentId { get; set; } }
public class ChildResultCardRequest { public int StudentId { get; set; } public int ResultSessionId { get; set; } }
public class ChildAttendanceRequest { public int StudentId { get; set; } public int? AcademicYearId { get; set; } }
public class ParentUserRequest { public int UserId { get; set; } }

/// <summary>
/// Parent Portal — parent-facing endpoints are scoped to the caller's linked
/// children (ownership enforced on every child-specific call). Admin-only
/// endpoints manage the parent↔student links.
/// </summary>
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/Parent")]
public class ParentController : ControllerBase
{
    private readonly ISender _sender;
    public ParentController(ISender sender) { _sender = sender; }

    private int CurrentUserId
        => int.TryParse(User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? User?.Identity?.Name, out var id) ? id : 0;

    private IActionResult Wrap<T>(OperationResult<T> r)
    {
        if (r is null) return StatusCode(500, new { Message = "Server Error" });
        return StatusCode(r.StatusCode, new { Data = r.Result, r.Message, r.StatusCode, r.IsSuccess, r.TotalCount });
    }

    private IActionResult Forbidden() =>
        new ObjectResult(new { Message = "You can only access your own children's records.", StatusCode = 403, IsSuccess = false }) { StatusCode = 403 };

    private async Task<bool> OwnsAsync(int studentId)
    {
        var r = await _sender.Send(new IsParentOfStudentQuery(CurrentUserId, studentId));
        return r.IsSuccess && r.Result;
    }

    /* ---------- Parent-facing (any authenticated; scoped to own children) ---------- */

    [Authorize, HttpPost("ParentGetMyChildren")]
    public async Task<IActionResult> MyChildren() => Wrap(await _sender.Send(new GetChildrenOfUserQuery(CurrentUserId)));

    [Authorize, HttpPost("ParentGetChildProfile")]
    public async Task<IActionResult> ChildProfile([FromBody] ChildRequest req)
    {
        if (!await OwnsAsync(req.StudentId)) return Forbidden();
        return Wrap(await _sender.Send(new GetStudentByIdQuery(new SearchRequestById { Id = req.StudentId, CultureId = null })));
    }

    [Authorize, HttpPost("ParentGetChildFees")]
    public async Task<IActionResult> ChildFees([FromBody] ChildRequest req)
    {
        if (!await OwnsAsync(req.StudentId)) return Forbidden();
        return Wrap(await _sender.Send(new GetStudentLedgerQuery(req.StudentId, null)));
    }

    [Authorize, HttpPost("ParentGetChildPayments")]
    public async Task<IActionResult> ChildPayments([FromBody] ChildRequest req)
    {
        if (!await OwnsAsync(req.StudentId)) return Forbidden();
        return Wrap(await _sender.Send(new GetStudentPaymentsQuery(req.StudentId)));
    }

    [Authorize, HttpPost("ParentGetChildReceiptPdf")]
    public async Task<IActionResult> ChildReceiptPdf([FromBody] ChildReceiptRequest req)
    {
        if (!await OwnsAsync(req.StudentId)) return Forbidden();
        var res = await _sender.Send(new GetFeeReceiptQuery(req.PaymentId, false));
        if (!res.IsSuccess || res.Result?.Bytes == null)
            return StatusCode(res?.StatusCode ?? 500, new { Message = res?.Message ?? "Server Error" });
        return File(res.Result.Bytes, res.Result.ContentType, res.Result.FileName);
    }

    [Authorize, HttpPost("ParentGetResultSessions")]
    public async Task<IActionResult> ResultSessions()
        => Wrap(await _sender.Send(new GetResultSessionsQuery(new SearchRequest
        {
            PageNumber = 1,
            PageSize = 0, // 0 = all (matches the SP's "if @PageSize > 0" convention)
            FilterArray = new List<FilterParameter>(),
            SortingArray = new List<SortingParameter>()
        })));

    [Authorize, HttpPost("ParentGetChildResultCard")]
    public async Task<IActionResult> ChildResultCard([FromBody] ChildResultCardRequest req)
    {
        if (!await OwnsAsync(req.StudentId)) return Forbidden();
        return Wrap(await _sender.Send(new GetStudentResultCardQuery(req.ResultSessionId, req.StudentId)));
    }

    [Authorize, HttpPost("ParentGetChildAttendanceHistory")]
    public async Task<IActionResult> ChildAttendanceHistory([FromBody] ChildAttendanceRequest req)
    {
        if (!await OwnsAsync(req.StudentId)) return Forbidden();
        return Wrap(await _sender.Send(new GetStudentAttendanceHistoryQuery(req.StudentId, req.AcademicYearId)));
    }

    [Authorize, HttpPost("ParentGetChildAttendanceSummary")]
    public async Task<IActionResult> ChildAttendanceSummary([FromBody] ChildAttendanceRequest req)
    {
        if (!await OwnsAsync(req.StudentId)) return Forbidden();
        return Wrap(await _sender.Send(new GetStudentAttendanceSummaryQuery(req.StudentId, req.AcademicYearId)));
    }

    /* ---------- Admin: manage parent↔student links ---------- */

    [Authorize(Roles = Roles.Admin), HttpPost("ParentGetChildrenOf")]
    public async Task<IActionResult> ChildrenOf([FromBody] ParentUserRequest req)
        => Wrap(await _sender.Send(new GetChildrenOfUserQuery(req.UserId)));

    [Authorize(Roles = Roles.Admin), HttpPost("ParentLinkChild")]
    public async Task<IActionResult> LinkChild([FromBody] LinkChildCommand cmd)
    {
        cmd.ChangedBy = CurrentUserId;
        return Wrap(await _sender.Send(cmd));
    }

    [Authorize(Roles = Roles.Admin), HttpPost("ParentUnlinkChild")]
    public async Task<IActionResult> UnlinkChild([FromBody] UnlinkChildCommand cmd)
        => Wrap(await _sender.Send(cmd));
}
