using Asp.Versioning;
using CleanArc.Application.Features.RoomType.Command.CreateRoomTypeCommand;
using CleanArc.Application.Features.RoomType.Command.DeleteRoomTypeCommand;
using CleanArc.Application.Features.RoomType.Command.UpdateRoomTypeCommand;
using CleanArc.Application.Features.RoomType.Queries.GetAllRoomTypes;
using CleanArc.Application.Features.RoomType.Queries.GetRoomTypeById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.RoomType
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/RoomType")]
    //[Authorize]
    public class RoomTypeController : _BaseController<CreateRoomTypeCommand, UpdateRoomTypeCommand, DeleteRoomTypeCommand, bool, GetAllRoomTypesQuery,
    List<GetAllRoomTypesQueryResult>, GetRoomTypeByIdQuery, GetRoomTypeByIdQueryResult>
    {
        
        public RoomTypeController(ISender sender, ILogger<_BaseController<CreateRoomTypeCommand, UpdateRoomTypeCommand, DeleteRoomTypeCommand, bool, GetAllRoomTypesQuery,
   List<GetAllRoomTypesQueryResult>, GetRoomTypeByIdQuery, GetRoomTypeByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
