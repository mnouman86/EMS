using Asp.Versioning;
using CleanArc.Application.Features.Result.Command.ConfigureGradeBandsCommand;
using CleanArc.Application.Features.Result.Command.CreateResultSessionCommand;
using CleanArc.Application.Features.Result.Command.EnterMarksCommand;
using CleanArc.Application.Features.Result.Command.LockClassResultsCommand;
using CleanArc.Application.Features.Result.Command.UnlockClassResultsCommand;
using CleanArc.Application.Features.Result.Queries.GetClassResultSheet;
using CleanArc.Application.Features.Result.Queries.GetGradeBands;
using CleanArc.Application.Features.Result.Queries.GetMarksEntryGrid;
using CleanArc.Application.Features.Result.Queries.GetPreLockReport;
using CleanArc.Application.Features.Result.Queries.GetResultSessions;
using CleanArc.Application.Features.Result.Queries.GetStudentResultCard;
using CleanArc.Application.Features.Result.Queries.ParentSearchResult;
using CleanArc.Infrastructure.Identity.Identity.Extensions;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog.Context;
using System.Security.Claims;

namespace CleanArc.Web.Api.Controllers.V1.Result;

/// <summary>
/// Endpoints for Results Management (RES-01..RES-08).
/// Does NOT inherit _BaseController — this module's surface is workflow-style
/// rather than CRUD, so each endpoint is explicit.
/// </summary>
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/Result")]
public class ResultController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ResultController(ISender sender, IHttpContextAccessor httpContextAccessor)
    {
        _sender = sender;
        _httpContextAccessor = httpContextAccessor;
        LogContext.PushProperty("UserId", _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "anonymous");
    }

    private int CurrentUserId
        => int.TryParse(User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? User?.Identity?.Name, out var id) ? id : 0;

    private IActionResult Wrap<T>(CleanArc.Application.Models.Common.OperationResult<T> r)
    {
        if (r is null) return StatusCode(500, new { Message = "Server Error" });
        return StatusCode(r.StatusCode, new
        {
            Data = r.Result,
            Message = r.Message,
            StatusCode = r.StatusCode,
            IsSuccess = r.IsSuccess,
            TotalCount = r.TotalCount
        });
    }

    /* ---------- RES-01: Sessions ---------- */

    [Authorize, HttpPost("ResultCreateSession")]
    public async Task<IActionResult> CreateSession([FromBody] CreateResultSessionCommand command)
    {
        command.UserId = CurrentUserId;
        return Wrap(await _sender.Send(command));
    }

    [HttpPost("ResultGetSessions")]
    public async Task<IActionResult> GetSessions([FromBody] GetResultSessionsQuery query) => Wrap(await _sender.Send(query));

    /* ---------- RES-04: Grade bands ---------- */

    [Authorize, HttpPost("ResultConfigureGradeBands")]
    public async Task<IActionResult> ConfigureBands([FromBody] ConfigureGradeBandsCommand command)
    {
        command.UserId = CurrentUserId;
        return Wrap(await _sender.Send(command));
    }

    [HttpPost("ResultGetGradeBands")]
    public async Task<IActionResult> GetBands() => Wrap(await _sender.Send(new GetGradeBandsQuery()));

    /* ---------- RES-02 / RES-03: Marks entry ---------- */

    [Authorize, HttpPost("ResultEnterMarks")]
    public async Task<IActionResult> EnterMarks([FromBody] EnterMarksCommand command)
    {
        command.UserId = CurrentUserId;
        return Wrap(await _sender.Send(command));
    }

    [Authorize, HttpPost("ResultGetMarksEntryGrid")]
    public async Task<IActionResult> GetGrid([FromBody] GetMarksEntryGridQuery query) => Wrap(await _sender.Send(query));

    /* ---------- RES-05: Lock / Unlock ---------- */

    [Authorize, HttpPost("ResultGetPreLockReport")]
    public async Task<IActionResult> PreLock([FromBody] GetPreLockReportQuery query) => Wrap(await _sender.Send(query));

    [Authorize, HttpPost("ResultLockClass")]
    public async Task<IActionResult> Lock([FromBody] LockClassResultsCommand command)
    {
        command.UserId = CurrentUserId;
        return Wrap(await _sender.Send(command));
    }

    [Authorize, HttpPost("ResultUnlockClass")]
    public async Task<IActionResult> Unlock([FromBody] UnlockClassResultsCommand command)
    {
        command.UserId = CurrentUserId;
        return Wrap(await _sender.Send(command));
    }

    /* ---------- RES-06: Class sheet ---------- */

    [Authorize, HttpPost("ResultGetClassSheet")]
    public async Task<IActionResult> ClassSheet([FromBody] GetClassResultSheetQuery query) => Wrap(await _sender.Send(query));

    /* ---------- RES-07: Parent search (public) ---------- */

    [AllowAnonymous, HttpPost("ResultParentSearch")]
    public async Task<IActionResult> ParentSearch([FromBody] ParentSearchResultQuery query)
    {
        query.ClientIp = HttpContext.Connection.RemoteIpAddress?.ToString();
        return Wrap(await _sender.Send(query));
    }

    /* ---------- RES-08: Result card (data; PDF rendering is a separate concern) ---------- */

    [Authorize, HttpPost("ResultGetStudentCard")]
    public async Task<IActionResult> StudentCard([FromBody] GetStudentResultCardQuery query) => Wrap(await _sender.Send(query));
}
