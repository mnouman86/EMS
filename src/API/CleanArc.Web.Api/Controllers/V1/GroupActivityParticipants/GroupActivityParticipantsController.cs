using Asp.Versioning;
using CleanArc.Application.Features.GroupActivityParticipants.Commands.CreateGroupActivityParticipantsCommand;
using CleanArc.Application.Features.GroupActivityParticipants.Commands.DeleteGroupActivityParticipantsCommand;
using CleanArc.Application.Features.GroupActivityParticipants.Commands.UpdateGroupActivityParticipantsCommand;
using CleanArc.Application.Features.GroupActivityParticipants.Queries.GetGroupActivityParticipantsById;
using CleanArc.Application.Features.GroupActivityParticipants.Queries.GetAllGroupActivityParticipants;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; 
using CleanArc.Domain.Common;
using Microsoft.AspNetCore.Authorization;

namespace CleanArc.Web.Api.Controllers.V1.GroupActivityParticipants
{
    /// <summary>
    /// GroupActivityParticipantsController is responsible for handling HTTP requests related to GroupActivityParticipants operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateGroupActivityParticipants: Handles the creation of a new age type.
    /// 2. UpdateGroupActivityParticipants: Handles the updating of an existing age type.
    /// 3. DeleteGroupActivityParticipants: Handles the deletion of an existing age type.
    /// 4. GetAllGroupActivityParticipants: Retrieves all age types.
    /// 5. GetGroupActivityParticipantsById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/GroupActivityParticipants".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateGroupActivityParticipants")]
    /// public async Task<IActionResult> CreateGroupActivityParticipants(CreateGroupActivityParticipantsCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.GroupActivityParticipants.Commands.CreateGroupActivityParticipantsCommand.CreateGroupActivityParticipantsCommand, CleanArc.Application.Features.GroupActivityParticipants.Commands.UpdateGroupActivityParticipantsCommand.UpdateGroupActivityParticipantsCommand, CleanArc.Application.Features.GroupActivityParticipants.Commands.DeleteGroupActivityParticipantsCommand.DeleteGroupActivityParticipantsCommand, System.ResponseEntity, CleanArc.Application.Features.GroupActivityParticipants.Queries.GetAllGroupActivityParticipants.GetAllGroupActivityParticipantsQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.GroupActivityParticipants.Queries.GetAllGroupActivityParticipants.GetAllGroupActivityParticipantsQueryResult&gt;, CleanArc.Application.Features.GroupActivityParticipants.Queries.GetGroupActivityParticipantsById.GetGroupActivityParticipantsByIdQuery, CleanArc.Application.Features.GroupActivityParticipants.Queries.GetGroupActivityParticipantsById.GetGroupActivityParticipantsByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/GroupActivityParticipants")]
    [AllowAnonymous]
    //[Authorize]
    public class GroupActivityParticipantsController : _BaseController<CreateGroupActivityParticipantsCommand, UpdateGroupActivityParticipantsCommand, DeleteGroupActivityParticipantsCommand, ResponseEntity, GetAllGroupActivityParticipantsQuery,
    List<GetAllGroupActivityParticipantsQueryResult>, GetGroupActivityParticipantsByIdQuery, GetGroupActivityParticipantsByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public GroupActivityParticipantsController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateGroupActivityParticipants")]
        //public async Task<IActionResult> CreateGroupActivityParticipants(CreateGroupActivityParticipantsCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateGroupActivityParticipants")]
        //public async Task<IActionResult> UpdateGroupActivityParticipants(UpdateGroupActivityParticipantsCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteGroupActivityParticipants")]
        //public async Task<IActionResult> DeleteGroupActivityParticipants(DeleteGroupActivityParticipantsCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllGroupActivityParticipants")]
        //public async Task<IActionResult> GetAllGroupActivityParticipants( )
        //{
        //    var queryResult = await _sender.Send(new GetAllGroupActivityParticipantsQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupActivityParticipantsController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public GroupActivityParticipantsController(ISender sender, ILogger<_BaseController<CreateGroupActivityParticipantsCommand, UpdateGroupActivityParticipantsCommand, DeleteGroupActivityParticipantsCommand, ResponseEntity, GetAllGroupActivityParticipantsQuery,
   List<GetAllGroupActivityParticipantsQueryResult>, GetGroupActivityParticipantsByIdQuery, GetGroupActivityParticipantsByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
