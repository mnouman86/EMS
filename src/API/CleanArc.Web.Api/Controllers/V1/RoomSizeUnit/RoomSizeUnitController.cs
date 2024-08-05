using Asp.Versioning;
using CleanArc.Application.Features.RoomSizeUnit.Command.CreateRoomSizeUnitCommand;
using CleanArc.Application.Features.RoomSizeUnit.Command.DeleteRoomSizeUnitCommand;
using CleanArc.Application.Features.RoomSizeUnit.Command.UpdateRoomSizeUnitCommand;
using CleanArc.Application.Features.RoomSizeUnit.Queries.GetAllRoomSizeUnits;
using CleanArc.Application.Features.RoomSizeUnit.Queries.GetRoomSizeUnitById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.RoomSizeUnit;

/// <summary>
/// RoomSizeUnitController is responsible for handling HTTP requests related to room size unit operations
/// such as creating, updating, deleting, and retrieving room size units. It extends from a base controller
/// which provides common functionality for CRUD operations.
/// 
/// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
/// endpoint definitions for the following actions:
/// 
/// 1. CreateRoomSizeUnit: Handles the creation of a new room size unit.
/// 2. UpdateRoomSizeUnit: Handles the updating of an existing room size unit.
/// 3. DeleteRoomSizeUnit: Handles the deletion of an existing room size unit.
/// 4. GetAllRoomSizeUnits: Retrieves all room size units.
/// 5. GetRoomSizeUnitById: Retrieves a specific room size unit by its ID.
/// 
/// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
/// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
/// 
/// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
/// version, specified as "api/v{version:apiVersion}/RoomSizeUnit".
/// 
/// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
/// operation result handling and user ID setting.
/// 
/// Example Usage:
/// 
/// [HttpPost("CreateRoomSizeUnit")]
/// public async Task<IActionResult> CreateRoomSizeUnit(CreateRoomSizeUnitCommand model)
/// {
///     model.UserId = base.UserId;
///     var command = await _sender.Send(model);
///     return base.OperationResult(command);
/// }
/// 
/// This ensures that the UserId is set from the base controller before sending the command and that the operation
/// result is properly formatted for the response.
/// 
/// The controller is part of the CleanArc architecture, ensuring a clean separation of concerns and adherence to 
/// SOLID principles.
/// </summary>
/// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.RoomSizeUnit.Command.CreateRoomSizeUnitCommand.CreateRoomSizeUnitCommand, CleanArc.Application.Features.RoomSizeUnit.Command.UpdateRoomSizeUnitCommand.UpdateRoomSizeUnitCommand, CleanArc.Application.Features.RoomSizeUnit.Command.DeleteRoomSizeUnitCommand.DeleteRoomSizeUnitCommand, System.Boolean, CleanArc.Application.Features.RoomSizeUnit.Queries.GetAllRoomSizeUnits.GetAllRoomSizeUnitsQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.RoomSizeUnit.Queries.GetAllRoomSizeUnits.GetAllRoomSizeUnitsQueryResult&gt;, CleanArc.Application.Features.RoomSizeUnit.Queries.GetRoomSizeUnitById.GetRoomSizeUnitByIdQuery, CleanArc.Application.Features.RoomSizeUnit.Queries.GetRoomSizeUnitById.GetRoomSizeUnitByIdQueryResult&gt;" />
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/RoomSizeUnit")]
public class RoomSizeUnitController : _BaseController<CreateRoomSizeUnitCommand, UpdateRoomSizeUnitCommand, DeleteRoomSizeUnitCommand, bool, GetAllRoomSizeUnitsQuery,
    List<GetAllRoomSizeUnitsQueryResult>, GetRoomSizeUnitByIdQuery, GetRoomSizeUnitByIdQueryResult>
{

    /// <summary>
    /// Initializes a new instance of the <see cref="RoomSizeUnitController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public RoomSizeUnitController(ISender sender, ILogger<_BaseController<CreateRoomSizeUnitCommand, UpdateRoomSizeUnitCommand, DeleteRoomSizeUnitCommand, bool, GetAllRoomSizeUnitsQuery,
List<GetAllRoomSizeUnitsQueryResult>, GetRoomSizeUnitByIdQuery, GetRoomSizeUnitByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}

