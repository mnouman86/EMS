using Asp.Versioning;
using CleanArc.Application.Features.ActivityTransportation.Commands.CreateActivityTransportationCommand;
using CleanArc.Application.Features.ActivityTransportation.Commands.DeleteActivityTransportationCommand;
using CleanArc.Application.Features.ActivityTransportation.Commands.UpdateActivityTransportationCommand;
using CleanArc.Application.Features.ActivityTransportation.Queries.GetActivityTransportationById;
using CleanArc.Application.Features.ActivityTransportation.Queries.GetAllActivityTransportation;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.ActivityTransportation
{
    /// <summary>
    /// ActivityTransportationController is responsible for handling HTTP requests related to ActivityTransportation operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateActivityTransportation: Handles the creation of a new age type.
    /// 2. UpdateActivityTransportation: Handles the updating of an existing age type.
    /// 3. DeleteActivityTransportation: Handles the deletion of an existing age type.
    /// 4. GetAllActivityTransportation: Retrieves all age types.
    /// 5. GetActivityTransportationById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/ActivityTransportation".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateActivityTransportation")]
    /// public async Task<IActionResult> CreateActivityTransportation(CreateActivityTransportationCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.ActivityTransportation.Commands.CreateActivityTransportationCommand.CreateActivityTransportationCommand, CleanArc.Application.Features.ActivityTransportation.Commands.UpdateActivityTransportationCommand.UpdateActivityTransportationCommand, CleanArc.Application.Features.ActivityTransportation.Commands.DeleteActivityTransportationCommand.DeleteActivityTransportationCommand, System.ResponseEntity, CleanArc.Application.Features.ActivityTransportation.Queries.GetAllActivityTransportation.GetAllActivityTransportationQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.ActivityTransportation.Queries.GetAllActivityTransportation.GetAllActivityTransportationQueryResult&gt;, CleanArc.Application.Features.ActivityTransportation.Queries.GetActivityTransportationById.GetActivityTransportationByIdQuery, CleanArc.Application.Features.ActivityTransportation.Queries.GetActivityTransportationById.GetActivityTransportationByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/ActivityTransportation")]
    //[Authorize]
    public class ActivityTransportationController : _BaseController<CreateActivityTransportationCommand, UpdateActivityTransportationCommand, DeleteActivityTransportationCommand, ResponseEntity, GetAllActivityTransportationQuery,
    List<GetAllActivityTransportationQueryResult>, GetActivityTransportationByIdQuery, GetActivityTransportationByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public ActivityTransportationController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateActivityTransportation")]
        //public async Task<IActionResult> CreateActivityTransportation(CreateActivityTransportationCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateActivityTransportation")]
        //public async Task<IActionResult> UpdateActivityTransportation(UpdateActivityTransportationCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteActivityTransportation")]
        //public async Task<IActionResult> DeleteActivityTransportation(DeleteActivityTransportationCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllActivityTransportation")]
        //public async Task<IActionResult> GetAllActivityTransportation( )
        //{
        //    var queryResult = await _sender.Send(new GetAllActivityTransportationQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityTransportationController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public ActivityTransportationController(ISender sender, ILogger<_BaseController<CreateActivityTransportationCommand, UpdateActivityTransportationCommand, DeleteActivityTransportationCommand, ResponseEntity, GetAllActivityTransportationQuery,
   List<GetAllActivityTransportationQueryResult>, GetActivityTransportationByIdQuery, GetActivityTransportationByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
