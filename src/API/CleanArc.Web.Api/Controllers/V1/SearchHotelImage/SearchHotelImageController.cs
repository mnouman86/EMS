using Asp.Versioning;
using CleanArc.Application.Features.SearchHotel.Commands.CreateSearchHotelCommand;
using CleanArc.Application.Features.SearchHotel.Commands.DeleteSearchHotelCommand;
using CleanArc.Application.Features.SearchHotel.Commands.UpdateSearchHotelCommand;
using CleanArc.Application.Features.SearchHotel.Queries.GetAllSearchHotels;
using CleanArc.Application.Features.SearchHotel.Queries.GetSearchHotelById;
using CleanArc.Application.Features.SearchHotelImage.Command.CreateSearchHotelImageCommand;
using CleanArc.Application.Features.SearchHotelImage.Command.DeleteSearchHotelImageCommand;
using CleanArc.Application.Features.SearchHotelImage.Command.UpdateSearchHotelImageCommand;
using CleanArc.Application.Features.SearchHotelImage.Queries.GetAllSearchHotelImage;
using CleanArc.Application.Features.SearchHotelImage.Queries.GetByIdSearchHotelImage;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.SearchHotelImage;
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/SearchHotelImage")]
//[Authorize]
public class SearchHotelImageController : _BaseController<CreateSearchCarImageCommand, UpdateSearchHotelImageCommand, DeleteSearchHotelImageCommand, bool, GetAllSearchHotelImageQuery,
List<GetAllSearchHotelImageQueryResult>, GetByIdSearchHotelImageQuery, GetByIdSearchHotelImageQueryResult>
{

    public SearchHotelImageController(ISender sender, ILogger<_BaseController<CreateSearchCarImageCommand, UpdateSearchHotelImageCommand, DeleteSearchHotelImageCommand, bool, GetAllSearchHotelImageQuery,
List<GetAllSearchHotelImageQueryResult>, GetByIdSearchHotelImageQuery, GetByIdSearchHotelImageQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}
