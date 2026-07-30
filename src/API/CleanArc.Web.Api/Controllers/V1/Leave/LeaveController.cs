using Asp.Versioning;
using CleanArc.Application.Features.Leave;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CleanArc.Web.Api.Controllers.V1.Leave;

/// <summary>
/// Leave management — self-service application, approval workflow, admin CRUD
/// for types + policies + routing, and a leaves dashboard. All endpoints require
/// authentication; admin operations are additionally gated by role.
/// </summary>
[ApiVersion("1")]
[ApiController]
[Authorize]
[Route("api/v{version:apiVersion}/Leave")]
public class LeaveController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IWebHostEnvironment _env;
    public LeaveController(ISender sender, IWebHostEnvironment env) { _sender = sender; _env = env; }

    private int CurrentUserId
        => int.TryParse(User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? User?.Identity?.Name, out var id) ? id : 0;

    private IActionResult Wrap<T>(OperationResult<T> r)
    {
        if (r is null) return StatusCode(500, new { Message = "Server Error" });
        return StatusCode(r.StatusCode, new { Data = r.Result, r.Message, r.StatusCode, r.IsSuccess, r.TotalCount });
    }

    /* ---------- Leave Types (admin) ---------- */
    [Authorize(Roles = Roles.Admin), HttpPost("LeaveUpsertType")]
    public async Task<IActionResult> UpsertType([FromBody] UpsertLeaveTypeCommand cmd)
    { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [HttpPost("LeaveGetTypes")]
    public async Task<IActionResult> GetTypes([FromBody] GetLeaveTypesQuery? q)
        => Wrap(await _sender.Send(q ?? new GetLeaveTypesQuery()));

    [Authorize(Roles = Roles.Admin), HttpPost("LeaveDeleteType")]
    public async Task<IActionResult> DeleteType([FromBody] DeleteLeaveTypeCommand cmd)
    { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    /* ---------- Policy (admin) ---------- */
    [Authorize(Roles = Roles.Admin), HttpPost("LeaveUpsertPolicy")]
    public async Task<IActionResult> UpsertPolicy([FromBody] UpsertLeavePolicyCommand cmd)
    { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [HttpPost("LeaveGetPolicies")]
    public async Task<IActionResult> GetPolicies([FromBody] GetLeavePoliciesQuery q) => Wrap(await _sender.Send(q));

    /* ---------- Routing (admin) ---------- */
    [Authorize(Roles = Roles.Admin), HttpPost("LeaveUpsertRoute")]
    public async Task<IActionResult> UpsertRoute([FromBody] UpsertLeaveRouteCommand cmd)
    { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [HttpPost("LeaveGetRoutes")]
    public async Task<IActionResult> GetRoutes() => Wrap(await _sender.Send(new GetLeaveRoutesQuery()));

    /* ---------- Balance ---------- */
    [HttpPost("LeaveGetMyBalance")]
    public async Task<IActionResult> GetMyBalance([FromBody] GetLeaveBalanceQuery? q)
    {
        var query = q ?? new GetLeaveBalanceQuery(null);
        query.UserId = CurrentUserId;
        return Wrap(await _sender.Send(query));
    }

    /* ---------- Application: multipart Submit ---------- */
    [HttpPost("LeaveSubmit")]
    [RequestSizeLimit(15 * 1024 * 1024)]
    public async Task<IActionResult> Submit(
        [FromForm] int LeaveTypeId,
        [FromForm] DateTime StartDate,
        [FromForm] DateTime EndDate,
        [FromForm] bool HalfDayFrom,
        [FromForm] bool HalfDayTo,
        [FromForm] string? Reason,
        IFormFile? Attachment)
    {
        var (path, original, err) = await TryPersistAttachment(Attachment);
        if (err != null) return BadRequest(new { Message = err, StatusCode = 400, IsSuccess = false });

        var cmd = new SubmitLeaveApplicationCommand
        {
            ApplicantUserId = CurrentUserId,
            LeaveTypeId = LeaveTypeId,
            StartDate = StartDate,
            EndDate = EndDate,
            HalfDayFrom = HalfDayFrom,
            HalfDayTo = HalfDayTo,
            Reason = Reason,
            AttachmentPath = path,
            AttachmentOriginalName = original
        };
        return Wrap(await _sender.Send(cmd));
    }

    /* Preview net working-day count (skips weekends + holidays). Used by
       the apply form so the "Duration" hint matches what the backend saves. */
    [HttpPost("LeavePreviewWorkingDays")]
    public async Task<IActionResult> PreviewDays([FromBody] PreviewLeaveWorkingDaysQuery q)
        => Wrap(await _sender.Send(q));

    [HttpPost("LeaveDecide"), Authorize]
    public async Task<IActionResult> Decide([FromBody] DecideLeaveApplicationCommand cmd)
    { cmd.DeciderUserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [HttpPost("LeaveCancel")]
    public async Task<IActionResult> Cancel([FromBody] CancelLeaveApplicationCommand cmd)
    { cmd.ActorUserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [HttpPost("LeaveGetApplications")]
    public async Task<IActionResult> GetApplications([FromBody] GetLeaveApplicationsQuery q)
    { q.CallerUserId = CurrentUserId; return Wrap(await _sender.Send(q)); }

    [HttpPost("LeaveGetDashboard")]
    public async Task<IActionResult> Dashboard() => Wrap(await _sender.Send(new GetLeaveDashboardQuery()));

    /* ---------- Attachment download ---------- */
    [HttpGet("LeaveDownloadAttachment")]
    public IActionResult Download([FromQuery] string path, [FromQuery] string? name = null)
    {
        var safeRoot = Path.GetFullPath(Path.Combine(_env.WebRootPath ?? Directory.GetCurrentDirectory(), "uploads", "leaves"));
        var full = Path.GetFullPath(Path.Combine(_env.WebRootPath ?? Directory.GetCurrentDirectory(), path.TrimStart('/', '\\')));
        if (!full.StartsWith(safeRoot, StringComparison.OrdinalIgnoreCase) || !System.IO.File.Exists(full))
            return NotFound();
        var bytes = System.IO.File.ReadAllBytes(full);
        var contentType = "application/octet-stream";
        return File(bytes, contentType, name ?? Path.GetFileName(full));
    }

    /* ---------- helpers ---------- */
    private static readonly HashSet<string> AllowedExt = new(StringComparer.OrdinalIgnoreCase)
        { ".pdf", ".png", ".jpg", ".jpeg", ".doc", ".docx", ".xls", ".xlsx", ".txt" };

    private async Task<(string? Path, string? Original, string? Error)> TryPersistAttachment(IFormFile? file)
    {
        if (file == null || file.Length == 0) return (null, null, null);
        if (file.Length > 10 * 1024 * 1024) return (null, null, "Attachment exceeds 10 MB limit.");
        var ext = Path.GetExtension(file.FileName);
        if (string.IsNullOrEmpty(ext) || !AllowedExt.Contains(ext))
            return (null, null, "Attachment type not allowed. Use pdf, png, jpg, doc, xls, txt.");

        var now = DateTime.UtcNow;
        var subRel = Path.Combine("uploads", "leaves", now.Year.ToString("D4"), now.Month.ToString("D2"));
        var webRoot = _env.WebRootPath;
        if (string.IsNullOrEmpty(webRoot))
        {
            webRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            if (!Directory.Exists(webRoot)) Directory.CreateDirectory(webRoot);
        }
        var absDir = Path.Combine(webRoot, subRel);
        Directory.CreateDirectory(absDir);

        var stored = $"{Guid.NewGuid():N}{ext}";
        var absPath = Path.Combine(absDir, stored);
        await using (var stream = new FileStream(absPath, FileMode.Create))
            await file.CopyToAsync(stream);
        return ($"{subRel.Replace('\\', '/')}/{stored}", file.FileName, null);
    }
}
