using Asp.Versioning;
using CleanArc.Application.Features.AcademicYear.Command.CreateAcademicYearCommand;
using CleanArc.Application.Features.AcademicYear.Command.DeleteAcademicYearCommand;
using CleanArc.Application.Features.AcademicYear.Command.SetCurrentAcademicYearCommand;
using CleanArc.Application.Features.AcademicYear.Command.UpdateAcademicYearCommand;
using CleanArc.Application.Features.AcademicYear.Queries.GetAcademicYears;
using CleanArc.Application.Features.AcademicYear.Queries.GetCurrentAcademicYear;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CleanArc.Web.Api.Controllers.V1.AcademicYear;

/// <summary>
/// AcademicYear endpoints (foundational, for Modules 6–9).
/// </summary>
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/AcademicYear")]
public class AcademicYearController : ControllerBase
{
    private readonly ISender _sender;
    public AcademicYearController(ISender sender) { _sender = sender; }

    private int CurrentUserId
        => int.TryParse(User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? User?.Identity?.Name, out var id) ? id : 0;

    private IActionResult Wrap<T>(CleanArc.Application.Models.Common.OperationResult<T> r)
    {
        if (r is null) return StatusCode(500, new { Message = "Server Error" });
        return StatusCode(r.StatusCode, new { Data = r.Result, r.Message, r.StatusCode, r.IsSuccess, r.TotalCount });
    }

    [Authorize, HttpPost("AcademicYearCreate")]
    public async Task<IActionResult> Create([FromBody] CreateAcademicYearCommand command)
    { command.UserId = CurrentUserId; return Wrap(await _sender.Send(command)); }

    [Authorize, HttpPost("AcademicYearUpdate")]
    public async Task<IActionResult> Update([FromBody] UpdateAcademicYearCommand command)
    { command.UserId = CurrentUserId; return Wrap(await _sender.Send(command)); }

    [Authorize, HttpPost("AcademicYearSetCurrent")]
    public async Task<IActionResult> SetCurrent([FromBody] SetCurrentAcademicYearCommand command)
    { command.UserId = CurrentUserId; return Wrap(await _sender.Send(command)); }

    [Authorize, HttpPost("AcademicYearDelete")]
    public async Task<IActionResult> Delete([FromBody] DeleteAcademicYearCommand command)
    { command.UserId = CurrentUserId; return Wrap(await _sender.Send(command)); }

    [Authorize, HttpPost("AcademicYearGetAll")]
    public async Task<IActionResult> GetAll([FromBody] GetAcademicYearsQuery query) => Wrap(await _sender.Send(query));

    [Authorize, HttpPost("AcademicYearGetCurrent")]
    public async Task<IActionResult> GetCurrent() => Wrap(await _sender.Send(new GetCurrentAcademicYearQuery()));
}
