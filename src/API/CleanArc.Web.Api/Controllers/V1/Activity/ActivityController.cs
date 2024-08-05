using Asp.Versioning;
using CleanArc.Application.Features.Activity.Commands.CreateActivityCommand;
using CleanArc.Application.Features.Activity.Commands.DeleteActivityCommand;
using CleanArc.Application.Features.Activity.Commands.UpdateActivityCommand;
using CleanArc.Application.Features.Activity.Queries.GetActivityById;
using CleanArc.Application.Features.Activity.Queries.GetAllActivity;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.Activity
{
    /// <summary>
    /// ActivityController is responsible for handling HTTP requests related to Activity operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateActivity: Handles the creation of a new age type.
    /// 2. UpdateActivity: Handles the updating of an existing age type.
    /// 3. DeleteActivity: Handles the deletion of an existing age type.
    /// 4. GetAllActivity: Retrieves all age types.
    /// 5. GetActivityById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/Activity".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateActivity")]
    /// public async Task<IActionResult> CreateActivity(CreateActivityCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.Activity.Commands.CreateActivityCommand.CreateActivityCommand, CleanArc.Application.Features.Activity.Commands.UpdateActivityCommand.UpdateActivityCommand, CleanArc.Application.Features.Activity.Commands.DeleteActivityCommand.DeleteActivityCommand, System.Boolean, CleanArc.Application.Features.Activity.Queries.GetAllActivity.GetAllActivityQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.Activity.Queries.GetAllActivity.GetAllActivityQueryResult&gt;, CleanArc.Application.Features.Activity.Queries.GetActivityById.GetActivityByIdQuery, CleanArc.Application.Features.Activity.Queries.GetActivityById.GetActivityByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/Activity")]
    //[Authorize]
    public class ActivityController : _BaseController<CreateActivityCommand, UpdateActivityCommand, DeleteActivityCommand, bool, GetAllActivityQuery,
    List<GetAllActivityQueryResult>, GetActivityByIdQuery, GetActivityByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public ActivityController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateActivity")]
        //public async Task<IActionResult> CreateActivity(CreateActivityCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateActivity")]
        //public async Task<IActionResult> UpdateActivity(UpdateActivityCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteActivity")]
        //public async Task<IActionResult> DeleteActivity(DeleteActivityCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllActivity")]
        //public async Task<IActionResult> GetAllActivity( )
        //{
        //    var queryResult = await _sender.Send(new GetAllActivityQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public ActivityController(ISender sender, ILogger<_BaseController<CreateActivityCommand, UpdateActivityCommand, DeleteActivityCommand, bool, GetAllActivityQuery,
   List<GetAllActivityQueryResult>, GetActivityByIdQuery, GetActivityByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
