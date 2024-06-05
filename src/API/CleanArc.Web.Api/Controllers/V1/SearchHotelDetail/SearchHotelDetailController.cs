using Asp.Versioning;
using CleanArc.Application.Features.SearchHotel.Commands.CreateSearchHotelCommand;
using CleanArc.Application.Features.SearchHotel.Commands.DeleteSearchHotelCommand;
using CleanArc.Application.Features.SearchHotel.Commands.UpdateSearchHotelCommand;
using CleanArc.Application.Features.SearchHotel.Queries.GetAllSearchHotels;
using CleanArc.Application.Features.SearchHotel.Queries.GetSearchHotelById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.SearchHotelDetail;

[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/SearchHotelDetail")]
//[Authorize]
public class SearchHotelDetailController : _BaseController<CreateSearchHotelCommand, UpdateSearchHotelCommand, DeleteSearchHotelCommand, bool, GetAllSearchHotelsQuery,
List<GetAllSearchHotelsQueryResult>, GetSearchHotelByIdQuery, GetSearchHotelByIdQueryResult>
{

    public SearchHotelDetailController(ISender sender, ILogger<_BaseController<CreateSearchHotelCommand, UpdateSearchHotelCommand, DeleteSearchHotelCommand, bool, GetAllSearchHotelsQuery,
List<GetAllSearchHotelsQueryResult>, GetSearchHotelByIdQuery, GetSearchHotelByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : []base(sender, logger, httpContextAccessor)
    {

    }
}
