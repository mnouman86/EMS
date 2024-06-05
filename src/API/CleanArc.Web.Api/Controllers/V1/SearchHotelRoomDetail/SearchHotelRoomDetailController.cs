using Asp.Versioning;
using CleanArc.Application.Features.RoomDetails.Command.CreateRoomDetailCommand;
using CleanArc.Application.Features.RoomDetails.Command.DeleteRoomDetailCommand;
using CleanArc.Application.Features.RoomDetails.Command.UpdateRoomDetailCommand;
using CleanArc.Application.Features.RoomDetails.Queries.GetAllRoomDetail;
using CleanArc.Application.Features.RoomDetails.Queries.GetRoomDetailById;
using CleanArc.Application.Features.SearchHotelRoomDetail.Command.CreateSearchHotelRoomDetail;
using CleanArc.Application.Features.SearchHotelRoomDetail.Command.DeleteSearchHotelRoomDetail;
using CleanArc.Application.Features.SearchHotelRoomDetail.Command.UpdateSearchHotelRoomDetail;
using CleanArc.Application.Features.SearchHotelRoomDetail.Queries.GetAllSearchHotelRoomDetail;
using CleanArc.Application.Features.SearchHotelRoomDetail.Queries.GetSearchHotelRoomDetailById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.SearchHotelRoomDetail;
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/RoomDetail")]
public class SearchHotelRoomDetailController : _BaseController<CreateSearchHotelRoomDetailCommand, UpdateSearchHotelRoomDetailCommand, DeleteSearchHotelRoomDetailCommand, bool, GetAllSearchHotelRoomDetailQuery,
    List<GetAllSearchHotelRoomDetailQueryResult>, GetSearchHotelRoomDetailByIdQuery, GetSearchHotelRoomDetailByIdQueryResult>
{

    public SearchHotelRoomDetailController(ISender sender, ILogger<_BaseController<CreateSearchHotelRoomDetailCommand, UpdateSearchHotelRoomDetailCommand, DeleteSearchHotelRoomDetailCommand, bool, GetAllSearchHotelRoomDetailQuery,
List<GetAllSearchHotelRoomDetailQueryResult>, GetSearchHotelRoomDetailByIdQuery, GetSearchHotelRoomDetailByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}
