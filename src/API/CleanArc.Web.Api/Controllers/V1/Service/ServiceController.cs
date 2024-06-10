using Asp.Versioning;
using CleanArc.Application.Features.Service.Command.CreateServiceCommand;
using CleanArc.Application.Features.Service.Command.DeleteServiceCommand;
using CleanArc.Application.Features.Service.Command.UpdateServiceCommand;
using CleanArc.Application.Features.Service.Queries.GetAllServices;
using CleanArc.Application.Features.Service.Queries.GetServiceById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.Service;
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/Service")]
//[Authorize]
public class ServiceController : _BaseController<CreateServiceCommand, UpdateServiceCommand, DeleteServiceCommand, bool, GetAllServicesQuery,
    List<GetAllServicesQueryResult>, GetServiceByIdQuery, GetServiceByIdQueryResult>
{
  
    public ServiceController(ISender sender, ILogger<_BaseController<CreateServiceCommand, UpdateServiceCommand, DeleteServiceCommand, bool, GetAllServicesQuery,
List<GetAllServicesQueryResult>, GetServiceByIdQuery, GetServiceByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}
