using Asp.Versioning;
using CleanArc.Application.Features.Complaint.Command;
using CleanArc.Application.Features.Complaint.Queries;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CleanArc.Web.Api.Controllers.V1.Complaint;

/// <summary>
/// Complaint / Suggestion module. See docs/EMS_User_Story_Complaints.md.
/// All endpoints require auth. Mutations on the admin/reviewer side are
/// restricted to admin role (super-admin bypass keeps it simple — the
/// principal report uses Read-only endpoints).
/// </summary>
[ApiVersion("1")]
[ApiController]
[Authorize]
[Route("api/v{version:apiVersion}/Complaint")]
public class ComplaintController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IWebHostEnvironment _env;
    public ComplaintController(ISender sender, IWebHostEnvironment env) { _sender = sender; _env = env; }

    private int CurrentUserId
        => int.TryParse(User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? User?.Identity?.Name, out var id) ? id : 0;

    private bool IsAdmin => User?.IsInRole(Roles.Admin) == true;

    private IActionResult Wrap<T>(OperationResult<T> r)
    {
        if (r is null) return StatusCode(500, new { Message = "Server Error" });
        return StatusCode(r.StatusCode, new { Data = r.Result, r.Message, r.StatusCode, r.IsSuccess, r.TotalCount });
    }

    /* ============== NATURE CATALOGUE (admin) ============== */

    [Authorize(Roles = Roles.Admin), HttpPost("ComplaintUpsertNature")]
    public async Task<IActionResult> UpsertNature([FromBody] UpsertComplaintNatureCommand cmd)
        => Wrap(await _sender.Send(cmd));

    [HttpPost("ComplaintGetNatures")]
    public async Task<IActionResult> GetNatures([FromBody] GetComplaintNaturesQuery q)
        => Wrap(await _sender.Send(q ?? new GetComplaintNaturesQuery()));

    [Authorize(Roles = Roles.Admin), HttpPost("ComplaintDeactivateNature")]
    public async Task<IActionResult> DeactivateNature([FromBody] DeactivateComplaintNatureCommand cmd)
        => Wrap(await _sender.Send(cmd));

    /* ============== COMPLAINT — LOG (multipart) ============== */

    /// <summary>
    /// Creates a complaint with optional supporting attachment.
    /// Accepts multipart form-data so the file can ride along with the form fields.
    /// </summary>
    [HttpPost("ComplaintCreate")]
    [RequestSizeLimit(15 * 1024 * 1024)]  // 15 MB ceiling (form + attachment)
    public async Task<IActionResult> Create(
        [FromForm] int ComplaintNatureId,
        [FromForm] string ComplainantName,
        [FromForm] string ContactNumber,
        [FromForm] string Description,
        [FromForm] string? ComplaintAgainst,
        IFormFile? Attachment)
    {
        var (path, original, err) = await TryPersistAttachment(Attachment);
        if (err != null) return BadRequest(new { Message = err, StatusCode = 400, IsSuccess = false });

        var res = await _sender.Send(new CreateComplaintCommand
        {
            LogonUserId = CurrentUserId,
            ComplaintNatureId = ComplaintNatureId,
            ComplainantName = ComplainantName,
            ContactNumber = ContactNumber,
            ComplaintAgainst = ComplaintAgainst,
            Description = Description,
            AttachmentPath = path,
            AttachmentOriginalName = original
        });
        return Wrap(res);
    }

    [HttpPost("ComplaintUpdate")]
    [RequestSizeLimit(15 * 1024 * 1024)]
    public async Task<IActionResult> Update(
        [FromForm] int ComplaintId,
        [FromForm] int ComplaintNatureId,
        [FromForm] string ComplainantName,
        [FromForm] string ContactNumber,
        [FromForm] string Description,
        [FromForm] string? ComplaintAgainst,
        [FromForm] bool ClearAttachment,
        IFormFile? Attachment)
    {
        string? path = null;
        string? original = null;
        if (Attachment != null)
        {
            var (p, o, err) = await TryPersistAttachment(Attachment);
            if (err != null) return BadRequest(new { Message = err, StatusCode = 400, IsSuccess = false });
            path = p; original = o;
        }

        var res = await _sender.Send(new UpdateComplaintCommand
        {
            ComplaintId = ComplaintId,
            ActorUserId = CurrentUserId,
            ComplaintNatureId = ComplaintNatureId,
            ComplainantName = ComplainantName,
            ContactNumber = ContactNumber,
            ComplaintAgainst = ComplaintAgainst,
            Description = Description,
            AttachmentPath = path,
            AttachmentOriginalName = original,
            ClearAttachment = ClearAttachment
        });
        return Wrap(res);
    }

    /* ============== READ ============== */

    [HttpPost("ComplaintGetMine")]
    public async Task<IActionResult> GetMine()
    {
        var q = new GetMyComplaintsQuery { LogonUserId = CurrentUserId };
        return Wrap(await _sender.Send(q));
    }

    [HttpPost("ComplaintGetAll")]
    public async Task<IActionResult> GetAll([FromBody] GetAllComplaintsQuery q)
        => Wrap(await _sender.Send(q));

    [HttpPost("ComplaintGetDetail")]
    public async Task<IActionResult> GetDetail([FromBody] GetComplaintDetailQuery q)
    {
        q.CallerUserId = CurrentUserId;
        q.IsAdmin = IsAdmin;
        return Wrap(await _sender.Send(q));
    }

    /* ============== ADMIN ACTIONS ============== */

    [Authorize(Roles = Roles.Admin), HttpPost("ComplaintChangeStatus")]
    public async Task<IActionResult> ChangeStatus([FromBody] ChangeComplaintStatusCommand cmd)
    { cmd.ActorUserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [Authorize(Roles = Roles.Admin), HttpPost("ComplaintAddNote")]
    public async Task<IActionResult> AddNote([FromBody] AddComplaintNoteCommand cmd)
    { cmd.ActorUserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [Authorize(Roles = Roles.Admin), HttpPost("ComplaintSoftDelete")]
    public async Task<IActionResult> SoftDelete([FromBody] SoftDeleteComplaintCommand cmd)
    { cmd.ActorUserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [Authorize(Roles = Roles.Admin), HttpPost("ComplaintRestore")]
    public async Task<IActionResult> Restore([FromBody] RestoreComplaintCommand cmd)
    { cmd.ActorUserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    /* ============== ATTACHMENT DOWNLOAD ============== */

    /// <summary>Streams a previously-uploaded complaint attachment to the caller.
    /// Path is validated to live under wwwroot/uploads/complaints so callers
    /// can't escape into the rest of the filesystem.</summary>
    [HttpGet("ComplaintDownloadAttachment")]
    public IActionResult Download([FromQuery] string path, [FromQuery] string? name = null)
    {
        var safeRoot = Path.GetFullPath(Path.Combine(_env.WebRootPath ?? Directory.GetCurrentDirectory(), "uploads", "complaints"));
        var full = Path.GetFullPath(Path.Combine(_env.WebRootPath ?? Directory.GetCurrentDirectory(), path.TrimStart('/', '\\')));
        if (!full.StartsWith(safeRoot, StringComparison.OrdinalIgnoreCase) || !System.IO.File.Exists(full))
            return NotFound();
        var bytes = System.IO.File.ReadAllBytes(full);
        var contentType = "application/octet-stream";
        var fileName = name ?? Path.GetFileName(full);
        return File(bytes, contentType, fileName);
    }

    /* ============== HELPERS ============== */

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
        var subRel = Path.Combine("uploads", "complaints", now.Year.ToString("D4"), now.Month.ToString("D2"));
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

        var relWeb = $"{subRel.Replace('\\', '/')}/{stored}";
        return (relWeb, file.FileName, null);
    }
}
