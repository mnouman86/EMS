using Asp.Versioning;
using CleanArc.Application.Features.AgeType.Commands.CreateAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Commands.DeleteAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Commands.UpdateAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
using CleanArc.Application.Features.AgeType.Queries.GetAllAgeType;
using CleanArc.Application.Features.Business.Command.CreateBusinessCommand;
using CleanArc.Application.Features.Business.Command.DeleteBusinessCommand;
using CleanArc.Application.Features.Business.Command.UpdateBusinessCommand;
using CleanArc.Application.Features.Business.Query.GetAllBusiness;
using CleanArc.Application.Features.Business.Query.GetBusinessById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.Business;

[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/Business")]
public class BusinessController: _BaseController<CreateBusinessCommand, UpdateBusinessCommand, DeleteBusinessCommand, bool, GetAllBusinessQuery,
List<GetAllBusinessQueryResult>, GetBusinessByIdQuery, GetBusinessByIdQueryResult>
{
    
    public BusinessController(ISender sender, ILogger<_BaseController<CreateBusinessCommand, UpdateBusinessCommand, DeleteBusinessCommand, bool, GetAllBusinessQuery,
List<GetAllBusinessQueryResult>, GetBusinessByIdQuery, GetBusinessByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}


