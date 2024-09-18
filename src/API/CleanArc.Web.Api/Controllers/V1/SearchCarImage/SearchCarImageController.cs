using Asp.Versioning;
using CleanArc.Application.Features.SearchHotel.Commands.CreateSearchHotelCommand;
using CleanArc.Application.Features.SearchHotel.Commands.DeleteSearchHotelCommand;
using CleanArc.Application.Features.SearchHotel.Commands.UpdateSearchHotelCommand;
using CleanArc.Application.Features.SearchHotel.Queries.GetAllSearchHotels;
using CleanArc.Application.Features.SearchHotel.Queries.GetSearchHotelById;
using CleanArc.Application.Features.SearchCarImage.Command.CreateSearchCarImageCommand;
using CleanArc.Application.Features.SearchCarImage.Command.DeleteSearchCarImageCommand;
using CleanArc.Application.Features.SearchCarImage.Command.UpdateSearchCarImageCommand;
using CleanArc.Application.Features.SearchCarImage.Queries.GetAllSearchCarImage;
using CleanArc.Application.Features.SearchCarImage.Queries.GetByIdSearchCarImage;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;
using CleanArc.Application.Features.SearchHotelImage.Command.CreateSearchHotelImageCommand;

namespace CleanArc.Web.Api.Controllers.V1.SearchCarImage;
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/SearchCarImage")]
//[Authorize]
public class SearchCarImageController : _BaseController<CreateSearchCarImageCommand, UpdateSearchCarImageCommand, DeleteSearchCarImageCommand, ResponseEntity, GetAllSearchCarImageQuery,
List<GetAllSearchCarImageQueryResult>, GetByIdSearchCarImageQuery, GetByIdSearchCarImageQueryResult>
{

    public SearchCarImageController(ISender sender, ILogger<_BaseController<CreateSearchCarImageCommand, UpdateSearchCarImageCommand, DeleteSearchCarImageCommand, ResponseEntity, GetAllSearchCarImageQuery,
List<GetAllSearchCarImageQueryResult>, GetByIdSearchCarImageQuery, GetByIdSearchCarImageQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}
