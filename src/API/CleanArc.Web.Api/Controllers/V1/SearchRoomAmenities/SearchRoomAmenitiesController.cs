using Asp.Versioning;
using CleanArc.Application.Features.SearchHotelAmenities.Commands.CreateSearchHotelAmenitiesCommand;
using CleanArc.Application.Features.SearchHotelAmenities.Commands.DeleteSearchHotelAmenitiesCommand;
using CleanArc.Application.Features.SearchHotelAmenities.Commands.UpdateSearchHotelAmenitiesCommand;
using CleanArc.Application.Features.SearchHotelAmenities.Queries.GetAllSearchHotelAmenities;
using CleanArc.Application.Features.SearchHotelAmenities.Queries.GetSearchHotelAmenitiesById;
using CleanArc.Application.Features.SearchRoomAmenities.Commands.CreateSearchRoomAmenities;
using CleanArc.Application.Features.SearchRoomAmenities.Commands.DeleteSearchRoomAmenities;
using CleanArc.Application.Features.SearchRoomAmenities.Commands.UpdateSearchRoomAmenities;
using CleanArc.Application.Features.SearchRoomAmenities.Queries.GetAllSearchRoomAmenities;
using CleanArc.Application.Features.SearchRoomAmenities.Queries.GetSearchRoomAmenitiesById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.SearchRoomAmenities;
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/SearchRoomAmenities")]
//[Authorize]
public class SearchRoomAmenitiesController : _BaseController<CreateSearchRoomAmenitiesCommand, UpdateSearchRoomAmenitiesCommand, DeleteSearchRoomAmenitiesCommand, bool, GetAllSearchRoomAmenitiesQuery,
List<GetAllSearchRoomAmenitiesQueryResult>, GetSearchRoomAmenitiesByIdQuery, GetSearchRoomAmenitiesByIdQueryResult>
{

    public SearchRoomAmenitiesController(ISender sender, ILogger<_BaseController<CreateSearchRoomAmenitiesCommand, UpdateSearchRoomAmenitiesCommand, DeleteSearchRoomAmenitiesCommand, bool, GetAllSearchRoomAmenitiesQuery,
List<GetAllSearchRoomAmenitiesQueryResult>, GetSearchRoomAmenitiesByIdQuery, GetSearchRoomAmenitiesByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}
