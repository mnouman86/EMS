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
    /// <summary>
    /// RoomVisualController is responsible for handling HTTP requests related to room visual operations
    /// such as creating, updating, deleting, and retrieving room visuals. It extends from a base controller
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateRoomVisual: Handles the creation of a new room visual.
    /// 2. UpdateRoomVisual: Handles the updating of an existing room visual.
    /// 3. DeleteRoomVisual: Handles the deletion of an existing room visual.
    /// 4. GetAllRoomVisuals: Retrieves all room visuals.
    /// 5. GetRoomVisualById: Retrieves a specific room visual by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/RoomVisual".
    /// 
    /// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
    /// operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateRoomVisual")]
    /// public async Task<IActionResult> CreateRoomVisual(CreateRoomVisualCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.RoomVisual.Command.CreateRoomVisualCommand.CreateRoomVisualCommand, CleanArc.Application.Features.RoomVisual.Command.UpdateRoomVisualCommand.UpdateRoomVisualCommand, CleanArc.Application.Features.RoomVisual.Command.DeleteRoomVisualCommand.DeleteRoomVisualCommand, System.Boolean, CleanArc.Application.Features.RoomVisual.Query.GetAllRoomVisual.GetAllRoomVisualQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.RoomVisual.Query.GetAllRoomVisual.GetAllRoomVisualQueryResult&gt;, CleanArc.Application.Features.RoomVisual.Query.GetRoomVisualById.GetRoomVisualByIdQuery, CleanArc.Application.Features.RoomVisual.Query.GetRoomVisualById.GetRoomVisualByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/RoomVisual")]
    //[Authorize]

    public class RoomVisualController : _BaseController<CreateRoomVisualCommand, UpdateRoomVisualCommand, DeleteRoomVisualCommand, bool, GetAllRoomVisualQuery,
    List<GetAllRoomVisualQueryResult>, GetRoomVisualByIdQuery, GetRoomVisualByIdQueryResult>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomVisualController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public RoomVisualController(ISender sender, ILogger<_BaseController<CreateRoomVisualCommand, UpdateRoomVisualCommand, DeleteRoomVisualCommand, bool, GetAllRoomVisualQuery,
   List<GetAllRoomVisualQueryResult>, GetRoomVisualByIdQuery, GetRoomVisualByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) :
            base(sender, logger, httpContextAccessor)
        {

        }

    }
}

