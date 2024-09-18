using Asp.Versioning;
using CleanArc.Application.Features.ActivityDisabilityMapping.Commands.CreateActivityDisabilityMappingCommand;
using CleanArc.Application.Features.ActivityDisabilityMapping.Commands.DeleteActivityDisabilityMappingCommand;
using CleanArc.Application.Features.ActivityDisabilityMapping.Commands.UpdateActivityDisabilityMappingCommand;
using CleanArc.Application.Features.ActivityDisabilityMapping.Queries.GetActivityDisabilityMappingById;
using CleanArc.Application.Features.ActivityDisabilityMapping.Queries.GetAllActivityDisabilityMapping;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.ActivityDisabilityMapping
{
    /// <summary>
    /// ActivityDisabilityMappingController is responsible for handling HTTP requests related to ActivityDisabilityMapping operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateActivityDisabilityMapping: Handles the creation of a new age type.
    /// 2. UpdateActivityDisabilityMapping: Handles the updating of an existing age type.
    /// 3. DeleteActivityDisabilityMapping: Handles the deletion of an existing age type.
    /// 4. GetAllActivityDisabilityMapping: Retrieves all age types.
    /// 5. GetActivityDisabilityMappingById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/ActivityDisabilityMapping".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateActivityDisabilityMapping")]
    /// public async Task<IActionResult> CreateActivityDisabilityMapping(CreateActivityDisabilityMappingCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.ActivityDisabilityMapping.Commands.CreateActivityDisabilityMappingCommand.CreateActivityDisabilityMappingCommand, CleanArc.Application.Features.ActivityDisabilityMapping.Commands.UpdateActivityDisabilityMappingCommand.UpdateActivityDisabilityMappingCommand, CleanArc.Application.Features.ActivityDisabilityMapping.Commands.DeleteActivityDisabilityMappingCommand.DeleteActivityDisabilityMappingCommand, System.ResponseEntity, CleanArc.Application.Features.ActivityDisabilityMapping.Queries.GetAllActivityDisabilityMapping.GetAllActivityDisabilityMappingQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.ActivityDisabilityMapping.Queries.GetAllActivityDisabilityMapping.GetAllActivityDisabilityMappingQueryResult&gt;, CleanArc.Application.Features.ActivityDisabilityMapping.Queries.GetActivityDisabilityMappingById.GetActivityDisabilityMappingByIdQuery, CleanArc.Application.Features.ActivityDisabilityMapping.Queries.GetActivityDisabilityMappingById.GetActivityDisabilityMappingByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/ActivityDisabilityMapping")]
    //[Authorize]
    public class ActivityDisabilityMappingController : _BaseController<CreateActivityDisabilityMappingCommand, UpdateActivityDisabilityMappingCommand, DeleteActivityDisabilityMappingCommand, ResponseEntity, GetAllActivityDisabilityMappingQuery,
    List<GetAllActivityDisabilityMappingQueryResult>, GetActivityDisabilityMappingByIdQuery, GetActivityDisabilityMappingByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public ActivityDisabilityMappingController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateActivityDisabilityMapping")]
        //public async Task<IActionResult> CreateActivityDisabilityMapping(CreateActivityDisabilityMappingCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateActivityDisabilityMapping")]
        //public async Task<IActionResult> UpdateActivityDisabilityMapping(UpdateActivityDisabilityMappingCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteActivityDisabilityMapping")]
        //public async Task<IActionResult> DeleteActivityDisabilityMapping(DeleteActivityDisabilityMappingCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllActivityDisabilityMapping")]
        //public async Task<IActionResult> GetAllActivityDisabilityMapping( )
        //{
        //    var queryResult = await _sender.Send(new GetAllActivityDisabilityMappingQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityDisabilityMappingController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public ActivityDisabilityMappingController(ISender sender, ILogger<_BaseController<CreateActivityDisabilityMappingCommand, UpdateActivityDisabilityMappingCommand, DeleteActivityDisabilityMappingCommand, ResponseEntity, GetAllActivityDisabilityMappingQuery,
   List<GetAllActivityDisabilityMappingQueryResult>, GetActivityDisabilityMappingByIdQuery, GetActivityDisabilityMappingByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
