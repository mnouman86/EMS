using Asp.Versioning;
using CleanArc.Application.Features.ActivityIncludedOption.Commands.CreateActivityIncludedOptionCommand;
using CleanArc.Application.Features.ActivityIncludedOption.Commands.DeleteActivityIncludedOptionCommand;
using CleanArc.Application.Features.ActivityIncludedOption.Commands.UpdateActivityIncludedOptionCommand;
using CleanArc.Application.Features.ActivityIncludedOption.Queries.GetActivityIncludedOptionById;
using CleanArc.Application.Features.ActivityIncludedOption.Queries.GetAllActivityIncludedOption;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.ActivityIncludedOption
{
    /// <summary>
    /// ActivityIncludedOptionController is responsible for handling HTTP requests related to ActivityIncludedOption operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateActivityIncludedOption: Handles the creation of a new age type.
    /// 2. UpdateActivityIncludedOption: Handles the updating of an existing age type.
    /// 3. DeleteActivityIncludedOption: Handles the deletion of an existing age type.
    /// 4. GetAllActivityIncludedOption: Retrieves all age types.
    /// 5. GetActivityIncludedOptionById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/ActivityIncludedOption".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateActivityIncludedOption")]
    /// public async Task<IActionResult> CreateActivityIncludedOption(CreateActivityIncludedOptionCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.ActivityIncludedOption.Commands.CreateActivityIncludedOptionCommand.CreateActivityIncludedOptionCommand, CleanArc.Application.Features.ActivityIncludedOption.Commands.UpdateActivityIncludedOptionCommand.UpdateActivityIncludedOptionCommand, CleanArc.Application.Features.ActivityIncludedOption.Commands.DeleteActivityIncludedOptionCommand.DeleteActivityIncludedOptionCommand, System.Boolean, CleanArc.Application.Features.ActivityIncludedOption.Queries.GetAllActivityIncludedOption.GetAllActivityIncludedOptionQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.ActivityIncludedOption.Queries.GetAllActivityIncludedOption.GetAllActivityIncludedOptionQueryResult&gt;, CleanArc.Application.Features.ActivityIncludedOption.Queries.GetActivityIncludedOptionById.GetActivityIncludedOptionByIdQuery, CleanArc.Application.Features.ActivityIncludedOption.Queries.GetActivityIncludedOptionById.GetActivityIncludedOptionByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/ActivityIncludedOption")]
    //[Authorize]
    public class ActivityIncludedOptionController : _BaseController<CreateActivityIncludedOptionCommand, UpdateActivityIncludedOptionCommand, DeleteActivityIncludedOptionCommand, bool, GetAllActivityIncludedOptionQuery,
    List<GetAllActivityIncludedOptionQueryResult>, GetActivityIncludedOptionByIdQuery, GetActivityIncludedOptionByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public ActivityIncludedOptionController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateActivityIncludedOption")]
        //public async Task<IActionResult> CreateActivityIncludedOption(CreateActivityIncludedOptionCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateActivityIncludedOption")]
        //public async Task<IActionResult> UpdateActivityIncludedOption(UpdateActivityIncludedOptionCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteActivityIncludedOption")]
        //public async Task<IActionResult> DeleteActivityIncludedOption(DeleteActivityIncludedOptionCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllActivityIncludedOption")]
        //public async Task<IActionResult> GetAllActivityIncludedOption( )
        //{
        //    var queryResult = await _sender.Send(new GetAllActivityIncludedOptionQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityIncludedOptionController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public ActivityIncludedOptionController(ISender sender, ILogger<_BaseController<CreateActivityIncludedOptionCommand, UpdateActivityIncludedOptionCommand, DeleteActivityIncludedOptionCommand, bool, GetAllActivityIncludedOptionQuery,
   List<GetAllActivityIncludedOptionQueryResult>, GetActivityIncludedOptionByIdQuery, GetActivityIncludedOptionByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
