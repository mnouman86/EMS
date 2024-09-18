using Asp.Versioning;
using CleanArc.Application.Features.ActivityManager.Commands.CreateActivityManagerCommand;
using CleanArc.Application.Features.ActivityManager.Commands.DeleteActivityManagerCommand;
using CleanArc.Application.Features.ActivityManager.Commands.UpdateActivityManagerCommand;
using CleanArc.Application.Features.ActivityManager.Queries.GetActivityManagerById;
using CleanArc.Application.Features.ActivityManager.Queries.GetAllActivityManager;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.ActivityManager
{
    /// <summary>
    /// ActivityManagerController is responsible for handling HTTP requests related to ActivityManager operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateActivityManager: Handles the creation of a new age type.
    /// 2. UpdateActivityManager: Handles the updating of an existing age type.
    /// 3. DeleteActivityManager: Handles the deletion of an existing age type.
    /// 4. GetAllActivityManager: Retrieves all age types.
    /// 5. GetActivityManagerById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/ActivityManager".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateActivityManager")]
    /// public async Task<IActionResult> CreateActivityManager(CreateActivityManagerCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.ActivityManager.Commands.CreateActivityManagerCommand.CreateActivityManagerCommand, CleanArc.Application.Features.ActivityManager.Commands.UpdateActivityManagerCommand.UpdateActivityManagerCommand, CleanArc.Application.Features.ActivityManager.Commands.DeleteActivityManagerCommand.DeleteActivityManagerCommand, System.ResponseEntity, CleanArc.Application.Features.ActivityManager.Queries.GetAllActivityManager.GetAllActivityManagerQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.ActivityManager.Queries.GetAllActivityManager.GetAllActivityManagerQueryResult&gt;, CleanArc.Application.Features.ActivityManager.Queries.GetActivityManagerById.GetActivityManagerByIdQuery, CleanArc.Application.Features.ActivityManager.Queries.GetActivityManagerById.GetActivityManagerByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/ActivityManager")]
    //[Authorize]
    public class ActivityManagerController : _BaseController<CreateActivityManagerCommand, UpdateActivityManagerCommand, DeleteActivityManagerCommand, ResponseEntity, GetAllActivityManagerQuery,
    List<GetAllActivityManagerQueryResult>, GetActivityManagerByIdQuery, GetActivityManagerByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public ActivityManagerController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateActivityManager")]
        //public async Task<IActionResult> CreateActivityManager(CreateActivityManagerCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateActivityManager")]
        //public async Task<IActionResult> UpdateActivityManager(UpdateActivityManagerCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteActivityManager")]
        //public async Task<IActionResult> DeleteActivityManager(DeleteActivityManagerCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllActivityManager")]
        //public async Task<IActionResult> GetAllActivityManager( )
        //{
        //    var queryResult = await _sender.Send(new GetAllActivityManagerQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityManagerController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public ActivityManagerController(ISender sender, ILogger<_BaseController<CreateActivityManagerCommand, UpdateActivityManagerCommand, DeleteActivityManagerCommand, ResponseEntity, GetAllActivityManagerQuery,
   List<GetAllActivityManagerQueryResult>, GetActivityManagerByIdQuery, GetActivityManagerByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
