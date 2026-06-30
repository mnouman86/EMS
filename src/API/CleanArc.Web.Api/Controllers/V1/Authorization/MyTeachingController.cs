using Asp.Versioning;
using CleanArc.Application.Features.Authorization;
using CleanArc.Application.Models.Common;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CleanArc.Web.Api.Controllers.V1.Authorization;

/// <summary>
/// Teacher self-service — the "My Teaching" landing page bundle.
/// Any authenticated user can call it; the SP returns rows only for the
/// employee linked to the caller (no rows for admins not in dbo.Employee,
/// which is fine — the SPA just shows empty sections in that case).
/// </summary>
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/MyTeaching")]
public class MyTeachingController : ControllerBase
{
    private readonly ISender _sender;
    public MyTeachingController(ISender sender) { _sender = sender; }

    private int CurrentUserId
        => int.TryParse(User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? User?.Identity?.Name, out var id) ? id : 0;

    private IActionResult Wrap<T>(OperationResult<T> r)
    {
        if (r is null) return StatusCode(500, new { Message = "Server Error" });
        return StatusCode(r.StatusCode, new { Data = r.Result, r.Message, r.StatusCode, r.IsSuccess, r.TotalCount });
    }

    [Authorize, HttpPost("MyTeachingGet")]
    public async Task<IActionResult> Get() => Wrap(await _sender.Send(new GetMyTeachingQuery(CurrentUserId)));
}
