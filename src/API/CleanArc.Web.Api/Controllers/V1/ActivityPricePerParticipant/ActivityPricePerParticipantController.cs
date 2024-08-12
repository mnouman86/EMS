using Asp.Versioning;
using CleanArc.Application.Features.ActivityPricePerParticipant.Commands.CreateActivityPricePerParticipantCommand;
using CleanArc.Application.Features.ActivityPricePerParticipant.Commands.DeleteActivityPricePerParticipantCommand;
using CleanArc.Application.Features.ActivityPricePerParticipant.Commands.UpdateActivityPricePerParticipantCommand;
using CleanArc.Application.Features.ActivityPricePerParticipant.Queries.GetActivityPricePerParticipantById;
using CleanArc.Application.Features.ActivityPricePerParticipant.Queries.GetAllActivityPricePerParticipant;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.ActivityPricePerParticipant
{
    /// <summary>
    /// ActivityPricePerParticipantController is responsible for handling HTTP requests related to ActivityPricePerParticipant operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateActivityPricePerParticipant: Handles the creation of a new age type.
    /// 2. UpdateActivityPricePerParticipant: Handles the updating of an existing age type.
    /// 3. DeleteActivityPricePerParticipant: Handles the deletion of an existing age type.
    /// 4. GetAllActivityPricePerParticipant: Retrieves all age types.
    /// 5. GetActivityPricePerParticipantById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/ActivityPricePerParticipant".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateActivityPricePerParticipant")]
    /// public async Task<IActionResult> CreateActivityPricePerParticipant(CreateActivityPricePerParticipantCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.ActivityPricePerParticipant.Commands.CreateActivityPricePerParticipantCommand.CreateActivityPricePerParticipantCommand, CleanArc.Application.Features.ActivityPricePerParticipant.Commands.UpdateActivityPricePerParticipantCommand.UpdateActivityPricePerParticipantCommand, CleanArc.Application.Features.ActivityPricePerParticipant.Commands.DeleteActivityPricePerParticipantCommand.DeleteActivityPricePerParticipantCommand, System.Boolean, CleanArc.Application.Features.ActivityPricePerParticipant.Queries.GetAllActivityPricePerParticipant.GetAllActivityPricePerParticipantQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.ActivityPricePerParticipant.Queries.GetAllActivityPricePerParticipant.GetAllActivityPricePerParticipantQueryResult&gt;, CleanArc.Application.Features.ActivityPricePerParticipant.Queries.GetActivityPricePerParticipantById.GetActivityPricePerParticipantByIdQuery, CleanArc.Application.Features.ActivityPricePerParticipant.Queries.GetActivityPricePerParticipantById.GetActivityPricePerParticipantByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/ActivityPricePerParticipant")]
    //[Authorize]
    public class ActivityPricePerParticipantController : _BaseController<CreateActivityPricePerParticipantCommand, UpdateActivityPricePerParticipantCommand, DeleteActivityPricePerParticipantCommand, bool, GetAllActivityPricePerParticipantQuery,
    List<GetAllActivityPricePerParticipantQueryResult>, GetActivityPricePerParticipantByIdQuery, GetActivityPricePerParticipantByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public ActivityPricePerParticipantController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateActivityPricePerParticipant")]
        //public async Task<IActionResult> CreateActivityPricePerParticipant(CreateActivityPricePerParticipantCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateActivityPricePerParticipant")]
        //public async Task<IActionResult> UpdateActivityPricePerParticipant(UpdateActivityPricePerParticipantCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteActivityPricePerParticipant")]
        //public async Task<IActionResult> DeleteActivityPricePerParticipant(DeleteActivityPricePerParticipantCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllActivityPricePerParticipant")]
        //public async Task<IActionResult> GetAllActivityPricePerParticipant( )
        //{
        //    var queryResult = await _sender.Send(new GetAllActivityPricePerParticipantQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityPricePerParticipantController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public ActivityPricePerParticipantController(ISender sender, ILogger<_BaseController<CreateActivityPricePerParticipantCommand, UpdateActivityPricePerParticipantCommand, DeleteActivityPricePerParticipantCommand, bool, GetAllActivityPricePerParticipantQuery,
   List<GetAllActivityPricePerParticipantQueryResult>, GetActivityPricePerParticipantByIdQuery, GetActivityPricePerParticipantByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
