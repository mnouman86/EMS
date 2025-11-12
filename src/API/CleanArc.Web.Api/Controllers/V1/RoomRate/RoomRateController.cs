using Asp.Versioning;
using CleanArc.Application.Features.RoomRate.Command.CreateRoomRateCommand;
using CleanArc.Application.Features.RoomRate.Command.DeleteRoomRateCommand;
using CleanArc.Application.Features.RoomRate.Command.UpdateRoomRateCommand;
using CleanArc.Application.Features.RoomRate.Queries.GetAllRoomRates;
using CleanArc.Application.Features.RoomRate.Queries.GetRoomRateById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.RoomRate
{
    /// <summary>
    /// RoomRateController is responsible for handling HTTP requests related to room type operations
    /// such as creating, updating, deleting, and retrieving room types. It extends from a base controller
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateRoomRate: Handles the creation of a new room type.
    /// 2. UpdateRoomRate: Handles the updating of an existing room type.
    /// 3. DeleteRoomRate: Handles the deletion of an existing room type.
    /// 4. GetAllRoomRates: Retrieves all room types.
    /// 5. GetRoomRateById: Retrieves a specific room type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/RoomRate".
    /// 
    /// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
    /// operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateRoomRate")]
    /// public async Task<IActionResult> CreateRoomRate(CreateRoomRateCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.RoomRate.Command.CreateRoomRateCommand.CreateRoomRateCommand, CleanArc.Application.Features.RoomRate.Command.UpdateRoomRateCommand.UpdateRoomRateCommand, CleanArc.Application.Features.RoomRate.Command.DeleteRoomRateCommand.DeleteRoomRateCommand, System.ResponseEntity, CleanArc.Application.Features.RoomRate.Queries.GetAllRoomRates.GetAllRoomRatesQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.RoomRate.Queries.GetAllRoomRates.GetAllRoomRatesQueryResult&gt;, CleanArc.Application.Features.RoomRate.Queries.GetRoomRateById.GetRoomRateByIdQuery, CleanArc.Application.Features.RoomRate.Queries.GetRoomRateById.GetRoomRateByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/RoomRate")]
    //[Authorize]
    public class RoomRateController : _BaseController<CreateRoomRateCommand, UpdateRoomRateCommand, DeleteRoomRateCommand, ResponseEntity, GetAllRoomRatesQuery,
    List<GetAllRoomRatesQueryResult>, GetRoomRateByIdQuery, GetRoomRateByIdQueryResult>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomRateController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public RoomRateController(ISender sender, ILogger<_BaseController<CreateRoomRateCommand, UpdateRoomRateCommand, DeleteRoomRateCommand, ResponseEntity, GetAllRoomRatesQuery,
   List<GetAllRoomRatesQueryResult>, GetRoomRateByIdQuery, GetRoomRateByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
