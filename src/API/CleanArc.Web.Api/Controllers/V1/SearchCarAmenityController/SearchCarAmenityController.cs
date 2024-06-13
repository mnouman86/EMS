using Asp.Versioning;
using CleanArc.Application.Features.SearchHotel.Commands.CreateSearchHotelCommand;
using CleanArc.Application.Features.SearchHotel.Commands.DeleteSearchHotelCommand;
using CleanArc.Application.Features.SearchHotel.Commands.UpdateSearchHotelCommand;
using CleanArc.Application.Features.SearchHotel.Queries.GetAllSearchHotels;
using CleanArc.Application.Features.SearchHotel.Queries.GetSearchHotelById;
using CleanArc.Application.Features.SearchCarAmenities.Commands.CreateSearchCarAmenitiesCommand;
using CleanArc.Application.Features.SearchCarAmenities.Commands.DeleteSearchCarAmenitiesCommand;
using CleanArc.Application.Features.SearchCarAmenities.Commands.UpdateSearchCarAmenitiesCommand;
using CleanArc.Application.Features.SearchCarAmenities.Queries.GetAllSearchCarAmenities;
using CleanArc.Application.Features.SearchCarAmenities.Queries.GetSearchCarAmenitiesById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.SearchCarAmenity;
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/SearchCarAmenity")]
//[Authorize]
public class SearchCarAmenityController : _BaseController<CreateSearchCarAmenitiesCommand, UpdateSearchCarAmenitiesCommand, DeleteSearchCarAmenitiesCommand, bool, GetAllSearchCarAmenitiesQuery,
List<GetAllSearchCarAmenitiesQueryResult>, GetSearchCarAmenitiesByIdQuery, GetSearchCarAmenitiesByIdQueryResult>
{

    public SearchCarAmenityController(ISender sender, ILogger<_BaseController<CreateSearchCarAmenitiesCommand, UpdateSearchCarAmenitiesCommand, DeleteSearchCarAmenitiesCommand, bool, GetAllSearchCarAmenitiesQuery,
List<GetAllSearchCarAmenitiesQueryResult>, GetSearchCarAmenitiesByIdQuery, GetSearchCarAmenitiesByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}

