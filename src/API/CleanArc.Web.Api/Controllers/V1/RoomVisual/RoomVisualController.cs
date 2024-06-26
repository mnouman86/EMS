using Asp.Versioning;
using CleanArc.Application.Features.RoomVisual.Command.CreateRoomVisualCommand;
using CleanArc.Application.Features.RoomVisual.Command.DeleteRoomVisualCommand;
using CleanArc.Application.Features.RoomVisual.Command.UpdateRoomVisualCommand;
using CleanArc.Application.Features.RoomVisual.Query.GetAllRoomVisual;
using CleanArc.Application.Features.RoomVisual.Query.GetRoomVisualById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.RoomVisual
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/RoomVisual")]
    //[Authorize]

    public class RoomVisualController : _BaseController<CreateRoomVisualCommand, UpdateRoomVisualCommand, DeleteRoomVisualCommand, bool, GetAllRoomVisualQuery,
    List<GetAllRoomVisualQueryResult>, GetRoomVisualByIdQuery, GetRoomVisualByIdQueryResult>
    {
        
        public RoomVisualController(ISender sender, ILogger<_BaseController<CreateRoomVisualCommand, UpdateRoomVisualCommand, DeleteRoomVisualCommand, bool, GetAllRoomVisualQuery,
   List<GetAllRoomVisualQueryResult>, GetRoomVisualByIdQuery, GetRoomVisualByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : 
            base(sender, logger, httpContextAccessor)
        {

        }

    }
}

