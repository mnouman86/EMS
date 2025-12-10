using Asp.Versioning;
using CleanArc.Application.Features.RoomDiscountByUserType.Command.CreateRoomDiscountByUserTypeCommand;
using CleanArc.Application.Features.RoomDiscountByUserType.Command.DeleteRoomDiscountByUserTypeCommand;
using CleanArc.Application.Features.RoomDiscountByUserType.Command.UpdateRoomDiscountByUserTypeCommand;
using CleanArc.Application.Features.RoomDiscountByUserType.Queries.GetAllRoomDiscountByUserTypes;
using CleanArc.Application.Features.RoomDiscountByUserType.Queries.GetRoomDiscountByUserTypeById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.RoomDiscountByUserType
{
    /// <summary>
    /// RoomDiscountByUserTypeController is responsible for handling HTTP requests related to room type operations
    /// such as creating, updating, deleting, and retrieving room types. It extends from a base controller
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateRoomDiscountByUserType: Handles the creation of a new room type.
    /// 2. UpdateRoomDiscountByUserType: Handles the updating of an existing room type.
    /// 3. DeleteRoomDiscountByUserType: Handles the deletion of an existing room type.
    /// 4. GetAllRoomDiscountByUserTypes: Retrieves all room types.
    /// 5. GetRoomDiscountByUserTypeById: Retrieves a specific room type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/RoomDiscountByUserType".
    /// 
    /// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
    /// operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateRoomDiscountByUserType")]
    /// public async Task<IActionResult> CreateRoomDiscountByUserType(CreateRoomDiscountByUserTypeCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.RoomDiscountByUserType.Command.CreateRoomDiscountByUserTypeCommand.CreateRoomDiscountByUserTypeCommand, CleanArc.Application.Features.RoomDiscountByUserType.Command.UpdateRoomDiscountByUserTypeCommand.UpdateRoomDiscountByUserTypeCommand, CleanArc.Application.Features.RoomDiscountByUserType.Command.DeleteRoomDiscountByUserTypeCommand.DeleteRoomDiscountByUserTypeCommand, System.ResponseEntity, CleanArc.Application.Features.RoomDiscountByUserType.Queries.GetAllRoomDiscountByUserTypes.GetAllRoomDiscountByUserTypesQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.RoomDiscountByUserType.Queries.GetAllRoomDiscountByUserTypes.GetAllRoomDiscountByUserTypesQueryResult&gt;, CleanArc.Application.Features.RoomDiscountByUserType.Queries.GetRoomDiscountByUserTypeById.GetRoomDiscountByUserTypeByIdQuery, CleanArc.Application.Features.RoomDiscountByUserType.Queries.GetRoomDiscountByUserTypeById.GetRoomDiscountByUserTypeByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/RoomDiscountByUserType")]
    //[Authorize]
    public class RoomDiscountByUserTypeController : _BaseController<CreateRoomDiscountByUserTypeCommand, UpdateRoomDiscountByUserTypeCommand, DeleteRoomDiscountByUserTypeCommand, ResponseEntity, GetAllRoomDiscountByUserTypesQuery,
    List<GetAllRoomDiscountByUserTypesQueryResult>, GetRoomDiscountByUserTypeByIdQuery, GetRoomDiscountByUserTypeByIdQueryResult>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomDiscountByUserTypeController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public RoomDiscountByUserTypeController(ISender sender, ILogger<_BaseController<CreateRoomDiscountByUserTypeCommand, UpdateRoomDiscountByUserTypeCommand, DeleteRoomDiscountByUserTypeCommand, ResponseEntity, GetAllRoomDiscountByUserTypesQuery,
   List<GetAllRoomDiscountByUserTypesQueryResult>, GetRoomDiscountByUserTypeByIdQuery, GetRoomDiscountByUserTypeByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
