using Asp.Versioning;
using CleanArc.Application.Features.RoomDetails.Command.CreateRoomDetailCommand;
using CleanArc.Application.Features.RoomDetails.Command.DeleteRoomDetailCommand;
using CleanArc.Application.Features.RoomDetails.Command.UpdateRoomDetailCommand;
using CleanArc.Application.Features.RoomDetails.Queries.GetAllRoomDetail;
using CleanArc.Application.Features.RoomDetails.Queries.GetRoomDetailById;
using CleanArc.Application.Features.SearchBusinessCarDetail.Command.CreateSearchBusinessCarDetail;
using CleanArc.Application.Features.SearchBusinessCarDetail.Command.DeleteSearchBusinessCarDetail;
using CleanArc.Application.Features.SearchBusinessCarDetail.Command.UpdateSearchBusinessCarDetail;
using CleanArc.Application.Features.SearchBusinessCarDetail.Queries.GetAllSearchBusinessCarDetail;
using CleanArc.Application.Features.SearchBusinessCarDetail.Queries.GetSearchBusinessCarDetailById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.SearchBusinessCarDetail;
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/SearchBusinessCarDetail")]
public class SearchBusinessCarDetailController : _BaseController<CreateSearchBusinessCarDetailCommand, UpdateSearchBusinessCarDetailCommand, DeleteSearchBusinessCarDetailCommand, bool, GetAllSearchBusinessCarDetailQuery,
    List<GetAllSearchBusinessCarDetailQueryResult>, GetSearchBusinessCarDetailByIdQuery, GetSearchBusinessCarDetailByIdQueryResult>
{

    public SearchBusinessCarDetailController(ISender sender, ILogger<_BaseController<CreateSearchBusinessCarDetailCommand, UpdateSearchBusinessCarDetailCommand, DeleteSearchBusinessCarDetailCommand, bool, GetAllSearchBusinessCarDetailQuery,
List<GetAllSearchBusinessCarDetailQueryResult>, GetSearchBusinessCarDetailByIdQuery, GetSearchBusinessCarDetailByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}
