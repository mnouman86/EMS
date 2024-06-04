using Asp.Versioning;
using CleanArc.Application.Features.RoomDetails.Command.CreateRoomDetailCommand;
using CleanArc.Application.Features.RoomDetails.Command.DeleteRoomDetailCommand;
using CleanArc.Application.Features.RoomDetails.Command.UpdateRoomDetailCommand;
using CleanArc.Application.Features.RoomDetails.Queries.GetAllRoomDetail;
using CleanArc.Application.Features.RoomDetails.Queries.GetRoomDetailById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.RoomDetail
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/RoomDetail")]
    [Authorize]
    public class RoomDetailController : _BaseController<CreateRoomDetailCommand, UpdateRoomDetailCommand, DeleteRoomDetailCommand, bool, GetAllRoomDetailQuery,
    List<GetAllRoomDetailQueryResult>, GetRoomDetailByIdQuery, GetRoomDetailByIdQueryResult>
    {
       
        public RoomDetailController(ISender sender, ILogger<_BaseController<CreateRoomDetailCommand, UpdateRoomDetailCommand, DeleteRoomDetailCommand, bool, GetAllRoomDetailQuery,
   List<GetAllRoomDetailQueryResult>, GetRoomDetailByIdQuery, GetRoomDetailByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}


