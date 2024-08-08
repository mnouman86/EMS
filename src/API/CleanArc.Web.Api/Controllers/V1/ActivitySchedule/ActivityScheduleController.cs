using Asp.Versioning;
using CleanArc.Application.Features.ActivitySchedule.Commands.CreateActivityScheduleCommand;
using CleanArc.Application.Features.ActivitySchedule.Commands.DeleteActivityScheduleCommand;
using CleanArc.Application.Features.ActivitySchedule.Commands.UpdateActivityScheduleCommand;
using CleanArc.Application.Features.ActivitySchedule.Queries.GetActivityScheduleById;
using CleanArc.Application.Features.ActivitySchedule.Queries.GetAllActivitySchedule;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.ActivitySchedule
{
    /// <summary>
    /// ActivityScheduleController is responsible for handling HTTP requests related to ActivitySchedule operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateActivitySchedule: Handles the creation of a new age type.
    /// 2. UpdateActivitySchedule: Handles the updating of an existing age type.
    /// 3. DeleteActivitySchedule: Handles the deletion of an existing age type.
    /// 4. GetAllActivitySchedule: Retrieves all age types.
    /// 5. GetActivityScheduleById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/ActivitySchedule".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateActivitySchedule")]
    /// public async Task<IActionResult> CreateActivitySchedule(CreateActivityScheduleCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.ActivitySchedule.Commands.CreateActivityScheduleCommand.CreateActivityScheduleCommand, CleanArc.Application.Features.ActivitySchedule.Commands.UpdateActivityScheduleCommand.UpdateActivityScheduleCommand, CleanArc.Application.Features.ActivitySchedule.Commands.DeleteActivityScheduleCommand.DeleteActivityScheduleCommand, System.Boolean, CleanArc.Application.Features.ActivitySchedule.Queries.GetAllActivitySchedule.GetAllActivityScheduleQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.ActivitySchedule.Queries.GetAllActivitySchedule.GetAllActivityScheduleQueryResult&gt;, CleanArc.Application.Features.ActivitySchedule.Queries.GetActivityScheduleById.GetActivityScheduleByIdQuery, CleanArc.Application.Features.ActivitySchedule.Queries.GetActivityScheduleById.GetActivityScheduleByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/ActivitySchedule")]
    //[Authorize]
    public class ActivityScheduleController : _BaseController<CreateActivityScheduleCommand, UpdateActivityScheduleCommand, DeleteActivityScheduleCommand, bool, GetAllActivityScheduleQuery,
    List<GetAllActivityScheduleQueryResult>, GetActivityScheduleByIdQuery, GetActivityScheduleByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public ActivityScheduleController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateActivitySchedule")]
        //public async Task<IActionResult> CreateActivitySchedule(CreateActivityScheduleCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateActivitySchedule")]
        //public async Task<IActionResult> UpdateActivitySchedule(UpdateActivityScheduleCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteActivitySchedule")]
        //public async Task<IActionResult> DeleteActivitySchedule(DeleteActivityScheduleCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllActivitySchedule")]
        //public async Task<IActionResult> GetAllActivitySchedule( )
        //{
        //    var queryResult = await _sender.Send(new GetAllActivityScheduleQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityScheduleController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public ActivityScheduleController(ISender sender, ILogger<_BaseController<CreateActivityScheduleCommand, UpdateActivityScheduleCommand, DeleteActivityScheduleCommand, bool, GetAllActivityScheduleQuery,
   List<GetAllActivityScheduleQueryResult>, GetActivityScheduleByIdQuery, GetActivityScheduleByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
