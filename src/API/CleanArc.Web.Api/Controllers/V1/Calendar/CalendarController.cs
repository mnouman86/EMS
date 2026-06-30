using Asp.Versioning;
using CleanArc.Application.Features.Calendar;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CleanArc.Web.Api.Controllers.V1.Calendar;

/// <summary>
/// School calendar — admin/principal-only writes for holidays + weekend config;
/// any authenticated user can read the non-working dates (used by the daily
/// attendance picker on the teacher screen).
/// </summary>
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/Calendar")]
public class CalendarController : ControllerBase
{
    private readonly ISender _sender;
    public CalendarController(ISender sender) { _sender = sender; }

    private int CurrentUserId
        => int.TryParse(User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? User?.Identity?.Name, out var id) ? id : 0;

    private IActionResult Wrap<T>(OperationResult<T> r)
    {
        if (r is null) return StatusCode(500, new { Message = "Server Error" });
        return StatusCode(r.StatusCode, new { Data = r.Result, r.Message, r.StatusCode, r.IsSuccess, r.TotalCount });
    }

    private const string ManageRoles = Roles.Admin + "," + Roles.Principal;

    /* ---------- Read (any authenticated) ---------- */
    [Authorize, HttpPost("CalendarGetHolidays")]
    public async Task<IActionResult> GetHolidays([FromBody] GetHolidaysQuery q) => Wrap(await _sender.Send(q));

    [Authorize, HttpPost("CalendarGetConfig")]
    public async Task<IActionResult> GetConfig() => Wrap(await _sender.Send(new GetCalendarConfigQuery()));

    [Authorize, HttpPost("CalendarGetNonWorkingDates")]
    public async Task<IActionResult> GetNonWorking([FromBody] GetNonWorkingDatesQuery q) => Wrap(await _sender.Send(q));

    /* ---------- Write (admin / principal) ---------- */
    [Authorize(Roles = ManageRoles), HttpPost("CalendarUpsertHoliday")]
    public async Task<IActionResult> UpsertHoliday([FromBody] UpsertHolidayCommand cmd)
    {
        cmd.ChangedBy = CurrentUserId;
        return Wrap(await _sender.Send(cmd));
    }

    [Authorize(Roles = ManageRoles), HttpPost("CalendarDeleteHoliday")]
    public async Task<IActionResult> DeleteHoliday([FromBody] DeleteHolidayCommand cmd) => Wrap(await _sender.Send(cmd));

    [Authorize(Roles = ManageRoles), HttpPost("CalendarSetConfig")]
    public async Task<IActionResult> SetConfig([FromBody] SetCalendarConfigCommand cmd)
    {
        cmd.ChangedBy = CurrentUserId;
        return Wrap(await _sender.Send(cmd));
    }
}
