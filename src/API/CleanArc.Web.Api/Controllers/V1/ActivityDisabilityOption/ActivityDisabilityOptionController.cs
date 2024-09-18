using Asp.Versioning;
using CleanArc.Application.Features.ActivityDisabilityOption.Commands.CreateActivityDisabilityOptionCommand;
using CleanArc.Application.Features.ActivityDisabilityOption.Commands.DeleteActivityDisabilityOptionCommand;
using CleanArc.Application.Features.ActivityDisabilityOption.Commands.UpdateActivityDisabilityOptionCommand;
using CleanArc.Application.Features.ActivityDisabilityOption.Queries.GetActivityDisabilityOptionById;
using CleanArc.Application.Features.ActivityDisabilityOption.Queries.GetAllActivityDisabilityOption;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.ActivityDisabilityOption
{
    /// <summary>
    /// ActivityDisabilityOptionController is responsible for handling HTTP requests related to ActivityDisabilityOption operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateActivityDisabilityOption: Handles the creation of a new age type.
    /// 2. UpdateActivityDisabilityOption: Handles the updating of an existing age type.
    /// 3. DeleteActivityDisabilityOption: Handles the deletion of an existing age type.
    /// 4. GetAllActivityDisabilityOption: Retrieves all age types.
    /// 5. GetActivityDisabilityOptionById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/ActivityDisabilityOption".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateActivityDisabilityOption")]
    /// public async Task<IActionResult> CreateActivityDisabilityOption(CreateActivityDisabilityOptionCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.ActivityDisabilityOption.Commands.CreateActivityDisabilityOptionCommand.CreateActivityDisabilityOptionCommand, CleanArc.Application.Features.ActivityDisabilityOption.Commands.UpdateActivityDisabilityOptionCommand.UpdateActivityDisabilityOptionCommand, CleanArc.Application.Features.ActivityDisabilityOption.Commands.DeleteActivityDisabilityOptionCommand.DeleteActivityDisabilityOptionCommand, System.ResponseEntity, CleanArc.Application.Features.ActivityDisabilityOption.Queries.GetAllActivityDisabilityOption.GetAllActivityDisabilityOptionQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.ActivityDisabilityOption.Queries.GetAllActivityDisabilityOption.GetAllActivityDisabilityOptionQueryResult&gt;, CleanArc.Application.Features.ActivityDisabilityOption.Queries.GetActivityDisabilityOptionById.GetActivityDisabilityOptionByIdQuery, CleanArc.Application.Features.ActivityDisabilityOption.Queries.GetActivityDisabilityOptionById.GetActivityDisabilityOptionByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/ActivityDisabilityOption")]
    //[Authorize]
    public class ActivityDisabilityOptionController : _BaseController<CreateActivityDisabilityOptionCommand, UpdateActivityDisabilityOptionCommand, DeleteActivityDisabilityOptionCommand, ResponseEntity, GetAllActivityDisabilityOptionQuery,
    List<GetAllActivityDisabilityOptionQueryResult>, GetActivityDisabilityOptionByIdQuery, GetActivityDisabilityOptionByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public ActivityDisabilityOptionController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateActivityDisabilityOption")]
        //public async Task<IActionResult> CreateActivityDisabilityOption(CreateActivityDisabilityOptionCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateActivityDisabilityOption")]
        //public async Task<IActionResult> UpdateActivityDisabilityOption(UpdateActivityDisabilityOptionCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteActivityDisabilityOption")]
        //public async Task<IActionResult> DeleteActivityDisabilityOption(DeleteActivityDisabilityOptionCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllActivityDisabilityOption")]
        //public async Task<IActionResult> GetAllActivityDisabilityOption( )
        //{
        //    var queryResult = await _sender.Send(new GetAllActivityDisabilityOptionQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityDisabilityOptionController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public ActivityDisabilityOptionController(ISender sender, ILogger<_BaseController<CreateActivityDisabilityOptionCommand, UpdateActivityDisabilityOptionCommand, DeleteActivityDisabilityOptionCommand, ResponseEntity, GetAllActivityDisabilityOptionQuery,
   List<GetAllActivityDisabilityOptionQueryResult>, GetActivityDisabilityOptionByIdQuery, GetActivityDisabilityOptionByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
