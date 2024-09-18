using Asp.Versioning;
using CleanArc.Application.Features.ActivityIDImageMapping.Commands.CreateActivityIDImageMappingCommand;
using CleanArc.Application.Features.ActivityIDImageMapping.Commands.DeleteActivityIDImageMappingCommand;
using CleanArc.Application.Features.ActivityIDImageMapping.Commands.UpdateActivityIDImageMappingCommand;
using CleanArc.Application.Features.ActivityIDImageMapping.Queries.GetActivityIDImageMappingById;
using CleanArc.Application.Features.ActivityIDImageMapping.Queries.GetAllActivityIDImageMapping;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.ActivityIDImageMapping
{
    /// <summary>
    /// ActivityIDImageMappingController is responsible for handling HTTP requests related to ActivityIDImageMapping operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateActivityIDImageMapping: Handles the creation of a new age type.
    /// 2. UpdateActivityIDImageMapping: Handles the updating of an existing age type.
    /// 3. DeleteActivityIDImageMapping: Handles the deletion of an existing age type.
    /// 4. GetAllActivityIDImageMapping: Retrieves all age types.
    /// 5. GetActivityIDImageMappingById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/ActivityIDImageMapping".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateActivityIDImageMapping")]
    /// public async Task<IActionResult> CreateActivityIDImageMapping(CreateActivityIDImageMappingCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.ActivityIDImageMapping.Commands.CreateActivityIDImageMappingCommand.CreateActivityIDImageMappingCommand, CleanArc.Application.Features.ActivityIDImageMapping.Commands.UpdateActivityIDImageMappingCommand.UpdateActivityIDImageMappingCommand, CleanArc.Application.Features.ActivityIDImageMapping.Commands.DeleteActivityIDImageMappingCommand.DeleteActivityIDImageMappingCommand, System.ResponseEntity, CleanArc.Application.Features.ActivityIDImageMapping.Queries.GetAllActivityIDImageMapping.GetAllActivityIDImageMappingQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.ActivityIDImageMapping.Queries.GetAllActivityIDImageMapping.GetAllActivityIDImageMappingQueryResult&gt;, CleanArc.Application.Features.ActivityIDImageMapping.Queries.GetActivityIDImageMappingById.GetActivityIDImageMappingByIdQuery, CleanArc.Application.Features.ActivityIDImageMapping.Queries.GetActivityIDImageMappingById.GetActivityIDImageMappingByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/ActivityIDImageMapping")]
    //[Authorize]
    public class ActivityIDImageMappingController : _BaseController<CreateActivityIDImageMappingCommand, UpdateActivityIDImageMappingCommand, DeleteActivityIDImageMappingCommand, ResponseEntity, GetAllActivityIDImageMappingQuery,
    List<GetAllActivityIDImageMappingQueryResult>, GetActivityIDImageMappingByIdQuery, GetActivityIDImageMappingByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public ActivityIDImageMappingController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateActivityIDImageMapping")]
        //public async Task<IActionResult> CreateActivityIDImageMapping(CreateActivityIDImageMappingCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateActivityIDImageMapping")]
        //public async Task<IActionResult> UpdateActivityIDImageMapping(UpdateActivityIDImageMappingCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteActivityIDImageMapping")]
        //public async Task<IActionResult> DeleteActivityIDImageMapping(DeleteActivityIDImageMappingCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllActivityIDImageMapping")]
        //public async Task<IActionResult> GetAllActivityIDImageMapping( )
        //{
        //    var queryResult = await _sender.Send(new GetAllActivityIDImageMappingQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityIDImageMappingController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public ActivityIDImageMappingController(ISender sender, ILogger<_BaseController<CreateActivityIDImageMappingCommand, UpdateActivityIDImageMappingCommand, DeleteActivityIDImageMappingCommand, ResponseEntity, GetAllActivityIDImageMappingQuery,
   List<GetAllActivityIDImageMappingQueryResult>, GetActivityIDImageMappingByIdQuery, GetActivityIDImageMappingByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
