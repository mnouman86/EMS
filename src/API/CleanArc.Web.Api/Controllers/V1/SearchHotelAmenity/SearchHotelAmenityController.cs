using Asp.Versioning;
using CleanArc.Application.Features.SearchHotel.Commands.CreateSearchHotelCommand;
using CleanArc.Application.Features.SearchHotel.Commands.DeleteSearchHotelCommand;
using CleanArc.Application.Features.SearchHotel.Commands.UpdateSearchHotelCommand;
using CleanArc.Application.Features.SearchHotel.Queries.GetAllSearchHotels;
using CleanArc.Application.Features.SearchHotel.Queries.GetSearchHotelById;
using CleanArc.Application.Features.SearchHotelAmenities.Commands.CreateSearchHotelAmenitiesCommand;
using CleanArc.Application.Features.SearchHotelAmenities.Commands.DeleteSearchHotelAmenitiesCommand;
using CleanArc.Application.Features.SearchHotelAmenities.Commands.UpdateSearchHotelAmenitiesCommand;
using CleanArc.Application.Features.SearchHotelAmenities.Queries.GetAllSearchHotelAmenities;
using CleanArc.Application.Features.SearchHotelAmenities.Queries.GetSearchHotelAmenitiesById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.SearchHotelAmenity;
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/SearchHotelAmenity")]
[Authorize]
public class SearchHotelAmenityController : _BaseController<CreateSearchHotelAmenitiesCommand, UpdateSearchHotelAmenitiesCommand, DeleteSearchHotelAmenitiesCommand, bool, GetAllSearchHotelAmenitiesQuery,
List<GetAllSearchHotelAmenitiesQueryResult>, GetSearchHotelAmenitiesByIdQuery, GetSearchHotelAmenitiesByIdQueryResult>
{

    public SearchHotelAmenityController(ISender sender, ILogger<_BaseController<CreateSearchHotelAmenitiesCommand, UpdateSearchHotelAmenitiesCommand, DeleteSearchHotelAmenitiesCommand, bool, GetAllSearchHotelAmenitiesQuery,
List<GetAllSearchHotelAmenitiesQueryResult>, GetSearchHotelAmenitiesByIdQuery, GetSearchHotelAmenitiesByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}

