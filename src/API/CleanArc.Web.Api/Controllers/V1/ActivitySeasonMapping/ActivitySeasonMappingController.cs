using Asp.Versioning;
using CleanArc.Application.Features.ActivitySeasonMapping.Commands.CreateActivitySeasonMappingCommand;
using CleanArc.Application.Features.ActivitySeasonMapping.Commands.DeleteActivitySeasonMappingCommand;
using CleanArc.Application.Features.ActivitySeasonMapping.Commands.UpdateActivitySeasonMappingCommand;
using CleanArc.Application.Features.ActivitySeasonMapping.Queries.GetActivitySeasonMappingById;
using CleanArc.Application.Features.ActivitySeasonMapping.Queries.GetAllActivitySeasonMapping;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.ActivitySeasonMapping
{
    /// <summary>
    /// ActivitySeasonMappingController is responsible for handling HTTP requests related to ActivitySeasonMapping operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateActivitySeasonMapping: Handles the creation of a new age type.
    /// 2. UpdateActivitySeasonMapping: Handles the updating of an existing age type.
    /// 3. DeleteActivitySeasonMapping: Handles the deletion of an existing age type.
    /// 4. GetAllActivitySeasonMapping: Retrieves all age types.
    /// 5. GetActivitySeasonMappingById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/ActivitySeasonMapping".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateActivitySeasonMapping")]
    /// public async Task<IActionResult> CreateActivitySeasonMapping(CreateActivitySeasonMappingCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.ActivitySeasonMapping.Commands.CreateActivitySeasonMappingCommand.CreateActivitySeasonMappingCommand, CleanArc.Application.Features.ActivitySeasonMapping.Commands.UpdateActivitySeasonMappingCommand.UpdateActivitySeasonMappingCommand, CleanArc.Application.Features.ActivitySeasonMapping.Commands.DeleteActivitySeasonMappingCommand.DeleteActivitySeasonMappingCommand, System.ResponseEntity, CleanArc.Application.Features.ActivitySeasonMapping.Queries.GetAllActivitySeasonMapping.GetAllActivitySeasonMappingQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.ActivitySeasonMapping.Queries.GetAllActivitySeasonMapping.GetAllActivitySeasonMappingQueryResult&gt;, CleanArc.Application.Features.ActivitySeasonMapping.Queries.GetActivitySeasonMappingById.GetActivitySeasonMappingByIdQuery, CleanArc.Application.Features.ActivitySeasonMapping.Queries.GetActivitySeasonMappingById.GetActivitySeasonMappingByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/ActivitySeasonMapping")]
    //[Authorize]
    public class ActivitySeasonMappingController : _BaseController<CreateActivitySeasonMappingCommand, UpdateActivitySeasonMappingCommand, DeleteActivitySeasonMappingCommand, ResponseEntity, GetAllActivitySeasonMappingQuery,
    List<GetAllActivitySeasonMappingQueryResult>, GetActivitySeasonMappingByIdQuery, GetActivitySeasonMappingByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public ActivitySeasonMappingController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateActivitySeasonMapping")]
        //public async Task<IActionResult> CreateActivitySeasonMapping(CreateActivitySeasonMappingCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateActivitySeasonMapping")]
        //public async Task<IActionResult> UpdateActivitySeasonMapping(UpdateActivitySeasonMappingCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteActivitySeasonMapping")]
        //public async Task<IActionResult> DeleteActivitySeasonMapping(DeleteActivitySeasonMappingCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllActivitySeasonMapping")]
        //public async Task<IActionResult> GetAllActivitySeasonMapping( )
        //{
        //    var queryResult = await _sender.Send(new GetAllActivitySeasonMappingQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivitySeasonMappingController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public ActivitySeasonMappingController(ISender sender, ILogger<_BaseController<CreateActivitySeasonMappingCommand, UpdateActivitySeasonMappingCommand, DeleteActivitySeasonMappingCommand, ResponseEntity, GetAllActivitySeasonMappingQuery,
   List<GetAllActivitySeasonMappingQueryResult>, GetActivitySeasonMappingByIdQuery, GetActivitySeasonMappingByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
