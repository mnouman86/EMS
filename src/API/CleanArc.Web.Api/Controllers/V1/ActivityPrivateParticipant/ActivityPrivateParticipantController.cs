using Asp.Versioning;
using CleanArc.Application.Features.ActivityPrivateParticipant.Commands.CreateActivityPrivateParticipantCommand;
using CleanArc.Application.Features.ActivityPrivateParticipant.Commands.DeleteActivityPrivateParticipantCommand;
using CleanArc.Application.Features.ActivityPrivateParticipant.Commands.UpdateActivityPrivateParticipantCommand;
using CleanArc.Application.Features.ActivityPrivateParticipant.Queries.GetActivityPrivateParticipantById;
using CleanArc.Application.Features.ActivityPrivateParticipant.Queries.GetAllActivityPrivateParticipant;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.ActivityPrivateParticipant
{
    /// <summary>
    /// ActivityPrivateParticipantController is responsible for handling HTTP requests related to ActivityPrivateParticipant operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateActivityPrivateParticipant: Handles the creation of a new age type.
    /// 2. UpdateActivityPrivateParticipant: Handles the updating of an existing age type.
    /// 3. DeleteActivityPrivateParticipant: Handles the deletion of an existing age type.
    /// 4. GetAllActivityPrivateParticipant: Retrieves all age types.
    /// 5. GetActivityPrivateParticipantById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/ActivityPrivateParticipant".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateActivityPrivateParticipant")]
    /// public async Task<IActionResult> CreateActivityPrivateParticipant(CreateActivityPrivateParticipantCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.ActivityPrivateParticipant.Commands.CreateActivityPrivateParticipantCommand.CreateActivityPrivateParticipantCommand, CleanArc.Application.Features.ActivityPrivateParticipant.Commands.UpdateActivityPrivateParticipantCommand.UpdateActivityPrivateParticipantCommand, CleanArc.Application.Features.ActivityPrivateParticipant.Commands.DeleteActivityPrivateParticipantCommand.DeleteActivityPrivateParticipantCommand, System.Boolean, CleanArc.Application.Features.ActivityPrivateParticipant.Queries.GetAllActivityPrivateParticipant.GetAllActivityPrivateParticipantQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.ActivityPrivateParticipant.Queries.GetAllActivityPrivateParticipant.GetAllActivityPrivateParticipantQueryResult&gt;, CleanArc.Application.Features.ActivityPrivateParticipant.Queries.GetActivityPrivateParticipantById.GetActivityPrivateParticipantByIdQuery, CleanArc.Application.Features.ActivityPrivateParticipant.Queries.GetActivityPrivateParticipantById.GetActivityPrivateParticipantByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/ActivityPrivateParticipant")]
    //[Authorize]
    public class ActivityPrivateParticipantController : _BaseController<CreateActivityPrivateParticipantCommand, UpdateActivityPrivateParticipantCommand, DeleteActivityPrivateParticipantCommand, bool, GetAllActivityPrivateParticipantQuery,
    List<GetAllActivityPrivateParticipantQueryResult>, GetActivityPrivateParticipantByIdQuery, GetActivityPrivateParticipantByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public ActivityPrivateParticipantController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateActivityPrivateParticipant")]
        //public async Task<IActionResult> CreateActivityPrivateParticipant(CreateActivityPrivateParticipantCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateActivityPrivateParticipant")]
        //public async Task<IActionResult> UpdateActivityPrivateParticipant(UpdateActivityPrivateParticipantCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteActivityPrivateParticipant")]
        //public async Task<IActionResult> DeleteActivityPrivateParticipant(DeleteActivityPrivateParticipantCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllActivityPrivateParticipant")]
        //public async Task<IActionResult> GetAllActivityPrivateParticipant( )
        //{
        //    var queryResult = await _sender.Send(new GetAllActivityPrivateParticipantQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityPrivateParticipantController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public ActivityPrivateParticipantController(ISender sender, ILogger<_BaseController<CreateActivityPrivateParticipantCommand, UpdateActivityPrivateParticipantCommand, DeleteActivityPrivateParticipantCommand, bool, GetAllActivityPrivateParticipantQuery,
   List<GetAllActivityPrivateParticipantQueryResult>, GetActivityPrivateParticipantByIdQuery, GetActivityPrivateParticipantByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }


    } }
