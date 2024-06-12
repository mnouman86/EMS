using Asp.Versioning;
using CleanArc.Application.Features.AgeType.Commands.CreateAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Commands.DeleteAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Commands.UpdateAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
using CleanArc.Application.Features.AgeType.Queries.GetAllAgeType;
using CleanArc.Application.Features.MappingRoomAmenity.Command.CreateMappingRoomAmenityCommand;
using CleanArc.Application.Features.MappingRoomAmenity.Command.DeleteMappingRoomAmenityCommand;
using CleanArc.Application.Features.MappingRoomAmenity.Command.UpdateMappingRoomAmenityCommand;
using CleanArc.Application.Features.MappingRoomAmenity.Query.GetAllMappingRoomAmenity;
using CleanArc.Application.Features.MappingRoomAmenity.Query.GetMappingRoomAmenityById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.MappingRoomAmenities;

[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/MappingRoomAmenities")]
//[Authorize]
public class MappingRoomAmenitiesController : _BaseController<CreateMappingRoomAmenityCommand, UpdateMappingRoomAmenityCommand, DeleteMappingRoomAmenityCommand, bool, GetAllMappingRoomAmenityQuery,
List<GetAllMappingRoomAmenityQueryResult>, GetMappingRoomAmenityByIdQuery, GetMappingRoomAmenityByIdQueryResult>
{
    
   
    public MappingRoomAmenitiesController(ISender sender, ILogger<_BaseController<CreateMappingRoomAmenityCommand, UpdateMappingRoomAmenityCommand, DeleteMappingRoomAmenityCommand, bool, GetAllMappingRoomAmenityQuery,
List<GetAllMappingRoomAmenityQueryResult>, GetMappingRoomAmenityByIdQuery, GetMappingRoomAmenityByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}


