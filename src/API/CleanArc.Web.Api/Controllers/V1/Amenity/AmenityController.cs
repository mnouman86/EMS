using Asp.Versioning;
using CleanArc.Application.Features.Amenities.Command.CreateAmenitiesCommand;
using CleanArc.Application.Features.Amenities.Command.DeleteAmenitiesCommand;
using CleanArc.Application.Features.Amenities.Command.UpdateAmenitiesCommand;
using CleanArc.Application.Features.Amenities.Queries.GetAllAmenities;
using CleanArc.Application.Features.Amenities.Queries.GetAmenitiesById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.Amenity
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/Amenity")]
    //[Authorize]
    public class AmenityController: _BaseController<CreateAmenityCommand, UpdateAmenityCommand, DeleteAmenityCommand, bool, GetAllAmenitiesQuery,
    List<GetAllAmenitiesQueryResult>, GetAmenityByIdQuery, GetAmenityByIdQueryResult>
    {    
            public AmenityController(ISender sender, ILogger<_BaseController<CreateAmenityCommand, UpdateAmenityCommand, DeleteAmenityCommand, bool, GetAllAmenitiesQuery,
       List<GetAllAmenitiesQueryResult>, GetAmenityByIdQuery, GetAmenityByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
            {

            }

        
    }
}
