using Asp.Versioning;
using CleanArc.Application.Features.RoomImage.Command.CreateRoomImageCommand;
using CleanArc.Application.Features.RoomImage.Command.DeleteRoomImageCommand;
using CleanArc.Application.Features.RoomImage.Command.UpdateRoomImageCommand;
using CleanArc.Application.Features.RoomImage.Queries.GetAllRoomImagesQuery;
using CleanArc.Application.Features.RoomImage.Queries.GetRoomImagesByIdQuery;
using CleanArc.Application.Features.SearchHotelRoomDetail.Command.CreateSearchHotelRoomDetail;
using CleanArc.Application.Features.SearchHotelRoomDetail.Command.DeleteSearchHotelRoomDetail;
using CleanArc.Application.Features.SearchHotelRoomDetail.Command.UpdateSearchHotelRoomDetail;
using CleanArc.Application.Features.SearchHotelRoomDetail.Queries.GetAllSearchHotelRoomDetail;
using CleanArc.Application.Features.SearchHotelRoomDetail.Queries.GetSearchHotelRoomDetailById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.RoomImages;
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/RoomImages")]
public class RoomImagesController : _BaseController<CreateRoomImageCommand, UpdateRoomImageCommand, DeleteRoomImageCommand, bool, GetAllRoomImagesQuery,
    List<GetAllRoomImagesQueryResult>, GetRoomImagesByIdQuery, GetRoomImagesByIdQueryResult>
{

    public RoomImagesController(ISender sender, ILogger<_BaseController<CreateRoomImageCommand, UpdateRoomImageCommand, DeleteRoomImageCommand, bool, GetAllRoomImagesQuery,
List<GetAllRoomImagesQueryResult>, GetRoomImagesByIdQuery, GetRoomImagesByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}