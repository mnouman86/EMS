using Asp.Versioning;
using CleanArc.Application.Features.AgeType.Commands.CreateAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Commands.DeleteAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Commands.UpdateAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
using CleanArc.Application.Features.AgeType.Queries.GetAllAgeType;
using CleanArc.Application.Features.MappingHotelAmenity.Command.CreateMappingHotelAmenityCommand;
using CleanArc.Application.Features.MappingHotelAmenity.Command.UpdateMappingHotelAmenityCommand;
using CleanArc.Application.Features.MappingHotelAmenity.Queries.GetAllMappingHotelAmenity;
using CleanArc.Application.Features.MappingHotelAmenity.Queries.GetMappingHotelAmenityByID;
using CleanArc.Application.FeaturesppingHotelAmenity.Command.DeleteMappingHotelAmenityCommand;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.MappingHotelAmenities
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/MappingHotelAmenities")]
    public class MappingHotelAmenitiesController : _BaseController<CreateMappingHotelAmenityCommand, UpdateMappingHotelAmenityCommand, DeleteMappingHotelAmenityCommand, bool, GetAllMappingHotelAmenityQuery,
    List<GetAllMappingHotelAmenityQueryResult>, GetMappingHotelAmenityByIDQuery, GetMappingHotelAmenityByIDQueryResult>
    {
        
        public MappingHotelAmenitiesController(ISender sender, ILogger<_BaseController<CreateMappingHotelAmenityCommand, UpdateMappingHotelAmenityCommand, DeleteMappingHotelAmenityCommand, bool, GetAllMappingHotelAmenityQuery,
   List<GetAllMappingHotelAmenityQueryResult>, GetMappingHotelAmenityByIDQuery, GetMappingHotelAmenityByIDQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}


