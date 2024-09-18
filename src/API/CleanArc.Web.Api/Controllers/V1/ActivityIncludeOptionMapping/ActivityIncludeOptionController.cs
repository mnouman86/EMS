using Asp.Versioning;
using CleanArc.Application.Features.ActivityIncludeOptionMapping.Commands.CreateActivityIncludeOptionMappingCommand;   
using CleanArc.Application.Features.ActivityIncludeOptionMapping.Commands.DeleteActivityIncludeOptionMappingCommand;
using CleanArc.Application.Features.ActivityIncludeOptionMapping.Commands.UpdateActivityIncludeOptionMappingCommand;
using CleanArc.Application.Features.ActivityIncludeOptionMapping.Queries.GetActivityIncludeOptionMappingById;
using CleanArc.Application.Features.ActivityIncludeOptionMapping.Queries.GetAllActivityIncludeOptionMapping;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;
using CleanArc.Application.Features.ActivityIncludedOption.Queries.GetAllActivityIncludedOption;

namespace CleanArc.Web.Api.Controllers.V1.ActivityIncludeOption
{
    /// <summary>
    /// ActivityIncludeOptionController is responsible for handling HTTP requests related to ActivityIncludeOption operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateActivityIncludeOption: Handles the creation of a new age type.
    /// 2. UpdateActivityIncludeOption: Handles the updating of an existing age type.
    /// 3. DeleteActivityIncludeOption: Handles the deletion of an existing age type.
    /// 4. GetAllActivityIncludeOption: Retrieves all age types.
    /// 5. GetActivityIncludeOptionById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/ActivityIncludeOption".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateActivityIncludeOption")]
    /// public async Task<IActionResult> CreateActivityIncludeOption(CreateActivityIncludeOptionCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.ActivityIncludeOption.Commands.CreateActivityIncludeOptionCommand.CreateActivityIncludeOptionCommand, CleanArc.Application.Features.ActivityIncludeOption.Commands.UpdateActivityIncludeOptionCommand.UpdateActivityIncludeOptionCommand, CleanArc.Application.Features.ActivityIncludeOption.Commands.DeleteActivityIncludeOptionCommand.DeleteActivityIncludeOptionCommand, System.ResponseEntity, CleanArc.Application.Features.ActivityIncludeOption.Queries.GetAllActivityIncludeOption.GetAllActivityIncludeOptionQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.ActivityIncludeOption.Queries.GetAllActivityIncludeOption.GetAllActivityIncludeOptionQueryResult&gt;, CleanArc.Application.Features.ActivityIncludeOption.Queries.GetActivityIncludeOptionById.GetActivityIncludeOptionByIdQuery, CleanArc.Application.Features.ActivityIncludeOption.Queries.GetActivityIncludeOptionById.GetActivityIncludeOptionByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/ActivityIncludeOption")]
    //[Authorize]
    public class ActivityIncludeOptionController : _BaseController<CreateActivityIncludeOptionMappingCommand, UpdateActivityIncludeOptionMappingCommand, DeleteActivityIncludeOptionMappingCommand, ResponseEntity, GetAllActivityIncludeOptionMappingQuery,
    List<GetAllActivityIncludeOptionMappingQueryResult>, GetActivityIncludeOptionMappingByIdQuery, GetActivityIncludeOptionMappingByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public ActivityIncludeOptionController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateActivityIncludeOption")]
        //public async Task<IActionResult> CreateActivityIncludeOption(CreateActivityIncludeOptionCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateActivityIncludeOption")]
        //public async Task<IActionResult> UpdateActivityIncludeOption(UpdateActivityIncludeOptionCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteActivityIncludeOption")]
        //public async Task<IActionResult> DeleteActivityIncludeOption(DeleteActivityIncludeOptionCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllActivityIncludeOption")]
        //public async Task<IActionResult> GetAllActivityIncludeOption( )
        //{
        //    var queryResult = await _sender.Send(new GetAllActivityIncludeOptionQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityIncludeOptionController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public ActivityIncludeOptionController(ISender sender, ILogger<_BaseController<CreateActivityIncludeOptionMappingCommand, UpdateActivityIncludeOptionMappingCommand, DeleteActivityIncludeOptionMappingCommand, ResponseEntity, GetAllActivityIncludeOptionMappingQuery,
    List<GetAllActivityIncludeOptionMappingQueryResult>, GetActivityIncludeOptionMappingByIdQuery, GetActivityIncludeOptionMappingByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
