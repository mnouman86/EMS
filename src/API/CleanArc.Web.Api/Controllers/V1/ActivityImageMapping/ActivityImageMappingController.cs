using Asp.Versioning;
using CleanArc.Application.Features.ActivityImageMapping.Commands.CreateActivityImageMappingCommand;
using CleanArc.Application.Features.ActivityImageMapping.Commands.DeleteActivityImageMappingCommand;
using CleanArc.Application.Features.ActivityImageMapping.Commands.UpdateActivityImageMappingCommand;
using CleanArc.Application.Features.ActivityImageMapping.Queries.GetActivityImageMappingById;
using CleanArc.Application.Features.ActivityImageMapping.Queries.GetAllActivityImageMapping;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.ActivityImageMapping
{
    /// <summary>
    /// ActivityImageMappingController is responsible for handling HTTP requests related to ActivityImageMapping operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateActivityImageMapping: Handles the creation of a new age type.
    /// 2. UpdateActivityImageMapping: Handles the updating of an existing age type.
    /// 3. DeleteActivityImageMapping: Handles the deletion of an existing age type.
    /// 4. GetAllActivityImageMapping: Retrieves all age types.
    /// 5. GetActivityImageMappingById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/ActivityImageMapping".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateActivityImageMapping")]
    /// public async Task<IActionResult> CreateActivityImageMapping(CreateActivityImageMappingCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.ActivityImageMapping.Commands.CreateActivityImageMappingCommand.CreateActivityImageMappingCommand, CleanArc.Application.Features.ActivityImageMapping.Commands.UpdateActivityImageMappingCommand.UpdateActivityImageMappingCommand, CleanArc.Application.Features.ActivityImageMapping.Commands.DeleteActivityImageMappingCommand.DeleteActivityImageMappingCommand, System.ResponseEntity, CleanArc.Application.Features.ActivityImageMapping.Queries.GetAllActivityImageMapping.GetAllActivityImageMappingQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.ActivityImageMapping.Queries.GetAllActivityImageMapping.GetAllActivityImageMappingQueryResult&gt;, CleanArc.Application.Features.ActivityImageMapping.Queries.GetActivityImageMappingById.GetActivityImageMappingByIdQuery, CleanArc.Application.Features.ActivityImageMapping.Queries.GetActivityImageMappingById.GetActivityImageMappingByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/ActivityImageMapping")]
    //[Authorize]
    public class ActivityImageMappingController : _BaseController<CreateActivityImageMappingCommand, UpdateActivityImageMappingCommand, DeleteActivityImageMappingCommand, ResponseEntity, GetAllActivityImageMappingQuery,
    List<GetAllActivityImageMappingQueryResult>, GetActivityImageMappingByIdQuery, GetActivityImageMappingByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public ActivityImageMappingController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateActivityImageMapping")]
        //public async Task<IActionResult> CreateActivityImageMapping(CreateActivityImageMappingCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateActivityImageMapping")]
        //public async Task<IActionResult> UpdateActivityImageMapping(UpdateActivityImageMappingCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteActivityImageMapping")]
        //public async Task<IActionResult> DeleteActivityImageMapping(DeleteActivityImageMappingCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllActivityImageMapping")]
        //public async Task<IActionResult> GetAllActivityImageMapping( )
        //{
        //    var queryResult = await _sender.Send(new GetAllActivityImageMappingQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityImageMappingController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public ActivityImageMappingController(ISender sender, ILogger<_BaseController<CreateActivityImageMappingCommand, UpdateActivityImageMappingCommand, DeleteActivityImageMappingCommand, ResponseEntity, GetAllActivityImageMappingQuery,
   List<GetAllActivityImageMappingQueryResult>, GetActivityImageMappingByIdQuery, GetActivityImageMappingByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
