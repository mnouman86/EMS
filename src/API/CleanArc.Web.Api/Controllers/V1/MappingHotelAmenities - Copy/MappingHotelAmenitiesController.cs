using Asp.Versioning;
using CleanArc.Application.Features.AgeType.Commands.CreateAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Commands.DeleteAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Commands.UpdateAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
using CleanArc.Application.Features.AgeType.Queries.GetAllAgeType;
using CleanArc.Application.Features.MappingCarAmenity.Command.CreateMappingCarAmenityCommand;
using CleanArc.Application.Features.MappingCarAmenity.Command.UpdateMappingCarAmenityCommand;
using CleanArc.Application.Features.MappingCarAmenity.Queries.GetAllMappingCarAmenity;
using CleanArc.Application.Features.MappingCarAmenity.Queries.GetMappingCarAmenityByID;
using CleanArc.Application.FeaturesppingCarAmenity.Command.DeleteMappingCarAmenityCommand;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.MappingCarAmenities
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/MappingCarAmenities")]
    public class MappingCarAmenitiesController : _BaseController<CreateMappingCarAmenityCommand, UpdateMappingCarAmenityCommand, DeleteMappingCarAmenityCommand, bool, GetAllMappingCarAmenityQuery,
    List<GetAllMappingCarAmenityQueryResult>, GetMappingCarAmenityByIDQuery, GetMappingCarAmenityByIDQueryResult>
    {
        
        public MappingCarAmenitiesController(ISender sender, ILogger<_BaseController<CreateMappingCarAmenityCommand, UpdateMappingCarAmenityCommand, DeleteMappingCarAmenityCommand, bool, GetAllMappingCarAmenityQuery,
   List<GetAllMappingCarAmenityQueryResult>, GetMappingCarAmenityByIDQuery, GetMappingCarAmenityByIDQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}


