using Asp.Versioning;
using CleanArc.Application.Features.ActivitySupervisor.Commands.CreateActivitySupervisorCommand;
using CleanArc.Application.Features.ActivitySupervisor.Commands.DeleteActivitySupervisorCommand;
using CleanArc.Application.Features.ActivitySupervisor.Commands.UpdateActivitySupervisorCommand;
using CleanArc.Application.Features.ActivitySupervisor.Queries.GetActivitySupervisorById;
using CleanArc.Application.Features.ActivitySupervisor.Queries.GetAllActivitySupervisor;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.ActivitySupervisor
{
    /// <summary>
    /// ActivitySupervisorController is responsible for handling HTTP requests related to ActivitySupervisor operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateActivitySupervisor: Handles the creation of a new age type.
    /// 2. UpdateActivitySupervisor: Handles the updating of an existing age type.
    /// 3. DeleteActivitySupervisor: Handles the deletion of an existing age type.
    /// 4. GetAllActivitySupervisor: Retrieves all age types.
    /// 5. GetActivitySupervisorById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/ActivitySupervisor".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateActivitySupervisor")]
    /// public async Task<IActionResult> CreateActivitySupervisor(CreateActivitySupervisorCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.ActivitySupervisor.Commands.CreateActivitySupervisorCommand.CreateActivitySupervisorCommand, CleanArc.Application.Features.ActivitySupervisor.Commands.UpdateActivitySupervisorCommand.UpdateActivitySupervisorCommand, CleanArc.Application.Features.ActivitySupervisor.Commands.DeleteActivitySupervisorCommand.DeleteActivitySupervisorCommand, System.ResponseEntity, CleanArc.Application.Features.ActivitySupervisor.Queries.GetAllActivitySupervisor.GetAllActivitySupervisorQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.ActivitySupervisor.Queries.GetAllActivitySupervisor.GetAllActivitySupervisorQueryResult&gt;, CleanArc.Application.Features.ActivitySupervisor.Queries.GetActivitySupervisorById.GetActivitySupervisorByIdQuery, CleanArc.Application.Features.ActivitySupervisor.Queries.GetActivitySupervisorById.GetActivitySupervisorByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/ActivitySupervisor")]
    //[Authorize]
    public class ActivitySupervisorController : _BaseController<CreateActivitySupervisorCommand, UpdateActivitySupervisorCommand, DeleteActivitySupervisorCommand, ResponseEntity, GetAllActivitySupervisorQuery,
    List<GetAllActivitySupervisorQueryResult>, GetActivitySupervisorByIdQuery, GetActivitySupervisorByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public ActivitySupervisorController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateActivitySupervisor")]
        //public async Task<IActionResult> CreateActivitySupervisor(CreateActivitySupervisorCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateActivitySupervisor")]
        //public async Task<IActionResult> UpdateActivitySupervisor(UpdateActivitySupervisorCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteActivitySupervisor")]
        //public async Task<IActionResult> DeleteActivitySupervisor(DeleteActivitySupervisorCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllActivitySupervisor")]
        //public async Task<IActionResult> GetAllActivitySupervisor( )
        //{
        //    var queryResult = await _sender.Send(new GetAllActivitySupervisorQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivitySupervisorController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public ActivitySupervisorController(ISender sender, ILogger<_BaseController<CreateActivitySupervisorCommand, UpdateActivitySupervisorCommand, DeleteActivitySupervisorCommand, ResponseEntity, GetAllActivitySupervisorQuery,
   List<GetAllActivitySupervisorQueryResult>, GetActivitySupervisorByIdQuery, GetActivitySupervisorByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
