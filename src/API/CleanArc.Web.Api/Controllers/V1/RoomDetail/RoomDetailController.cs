using Asp.Versioning;
using CleanArc.Application.Features.RoomDetails.Command.CreateRoomDetailCommand;
using CleanArc.Application.Features.RoomDetails.Command.DeleteRoomDetailCommand;
using CleanArc.Application.Features.RoomDetails.Command.UpdateRoomDetailCommand;
using CleanArc.Application.Features.RoomDetails.Queries.GetAllRoomDetail;
using CleanArc.Application.Features.RoomDetails.Queries.GetRoomDetailById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.RoomDetail
{
    /// <summary>
    /// RoomDetailController is responsible for handling HTTP requests related to room detail operations
    /// such as creating, updating, deleting, and retrieving room details. It extends from a base controller
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateRoomDetail: Handles the creation of a new room detail.
    /// 2. UpdateRoomDetail: Handles the updating of an existing room detail.
    /// 3. DeleteRoomDetail: Handles the deletion of an existing room detail.
    /// 4. GetAllRoomDetails: Retrieves all room details.
    /// 5. GetRoomDetailById: Retrieves a specific room detail by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/RoomDetail".
    /// 
    /// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
    /// operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateRoomDetail")]
    /// public async Task<IActionResult> CreateRoomDetail(CreateRoomDetailCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.RoomDetails.Command.CreateRoomDetailCommand.CreateRoomDetailCommand, CleanArc.Application.Features.RoomDetails.Command.UpdateRoomDetailCommand.UpdateRoomDetailCommand, CleanArc.Application.Features.RoomDetails.Command.DeleteRoomDetailCommand.DeleteRoomDetailCommand, System.Boolean, CleanArc.Application.Features.RoomDetails.Queries.GetAllRoomDetail.GetAllRoomDetailQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.RoomDetails.Queries.GetAllRoomDetail.GetAllRoomDetailQueryResult&gt;, CleanArc.Application.Features.RoomDetails.Queries.GetRoomDetailById.GetRoomDetailByIdQuery, CleanArc.Application.Features.RoomDetails.Queries.GetRoomDetailById.GetRoomDetailByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/RoomDetail")]
    //[Authorize]
    public class RoomDetailController : _BaseController<CreateRoomDetailCommand, UpdateRoomDetailCommand, DeleteRoomDetailCommand, bool, GetAllRoomDetailQuery,
    List<GetAllRoomDetailQueryResult>, GetRoomDetailByIdQuery, GetRoomDetailByIdQueryResult>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomDetailController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public RoomDetailController(ISender sender, ILogger<_BaseController<CreateRoomDetailCommand, UpdateRoomDetailCommand, DeleteRoomDetailCommand, bool, GetAllRoomDetailQuery,
   List<GetAllRoomDetailQueryResult>, GetRoomDetailByIdQuery, GetRoomDetailByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}


