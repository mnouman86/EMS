using Asp.Versioning;
using CleanArc.Application.Features.Service.Command.CreateServiceCommand;
using CleanArc.Application.Features.Service.Command.DeleteServiceCommand;
using CleanArc.Application.Features.Service.Command.UpdateServiceCommand;
using CleanArc.Application.Features.Service.Queries.GetAllServices;
using CleanArc.Application.Features.Service.Queries.GetServiceById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; 
using CleanArc.Domain.Common;
using CleanArc.Application.Features.Admin.Queries.GetToken;

namespace CleanArc.Web.Api.Controllers.V1.StartupData;
/// <summary>
/// Startup Data
/// </summary>
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/StartupData")]
//[Authorize]
public class StartupDataController : BaseController
{
    private readonly ISender _sender;

    public StartupDataController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("GetStartupData")]
    public async Task<IActionResult> GetStartupData(GetStartupDataQuery request)
    {
        var query = await _sender.Send(request);

        return base.OperationResult(query);
    }
    //[HttpGet("GetActivityCheckoutDetail/{id}/{userid}")]
    //public async Task<IActionResult> GetActivityCheckoutDetail(GetActivityCheckoutDetailQuery query)
    //{
    //    //GetActivityCheckoutDetailQuery query = new GetActivityCheckoutDetailQuery { searchRequestById = searchRequestById,UserId=userid };
    //    var result = await _sender.Send(query);

    //    return base.OperationResult(result);
    //}
}
