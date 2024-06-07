using Asp.Versioning;
using CleanArc.Application.Features.AgeType.Commands.CreateAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Commands.DeleteAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Commands.UpdateAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
using CleanArc.Application.Features.AgeType.Queries.GetAllAgeType;
using CleanArc.Application.Features.RoomSizeUnit.Command.CreateRoomSizeUnitCommand;
using CleanArc.Application.Features.RoomSizeUnit.Command.DeleteRoomSizeUnitCommand;
using CleanArc.Application.Features.RoomSizeUnit.Command.UpdateRoomSizeUnitCommand;
using CleanArc.Application.Features.RoomSizeUnit.Queries.GetAllRoomSizeUnits;
using CleanArc.Application.Features.RoomSizeUnit.Queries.GetRoomSizeUnitById;
using CleanArc.Application.Models.RoomSizeUnit;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.RoomSizeUnit;

[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/RoomSizeUnit")]
public class RoomSizeUnitController : _BaseController<CreateRoomSizeUnitCommand, UpdateRoomSizeUnitCommand, DeleteRoomSizeUnitCommand, bool, GetAllRoomSizeUnitsQuery,
    List<GetAllRoomSizeUnitsQueryResult>, GetRoomSizeUnitByIdQuery, GetRoomSizeUnitByIdQueryResult>
{
   
    public RoomSizeUnitController(ISender sender, ILogger<_BaseController<CreateRoomSizeUnitCommand, UpdateRoomSizeUnitCommand, DeleteRoomSizeUnitCommand, bool, GetAllRoomSizeUnitsQuery,
List<GetAllRoomSizeUnitsQueryResult>, GetRoomSizeUnitByIdQuery, GetRoomSizeUnitByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}

