using Asp.Versioning;
using CleanArc.Application.Features.ActivityNature.Commands.CreateActivityNatureCommand;
using CleanArc.Application.Features.ActivityNature.Commands.DeleteActivityNatureCommand;
using CleanArc.Application.Features.ActivityNature.Commands.UpdateActivityNatureCommand;
using CleanArc.Application.Features.ActivityNature.Queries.GetActivityNatureById;
using CleanArc.Application.Features.ActivityNature.Queries.GetAllActivityNature;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.ActivityNature
{
    /// <summary>
    /// ActivityNatureController is responsible for handling HTTP requests related to ActivityNature operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateActivityNature: Handles the creation of a new age type.
    /// 2. UpdateActivityNature: Handles the updating of an existing age type.
    /// 3. DeleteActivityNature: Handles the deletion of an existing age type.
    /// 4. GetAllActivityNature: Retrieves all age types.
    /// 5. GetActivityNatureById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/ActivityNature".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateActivityNature")]
    /// public async Task<IActionResult> CreateActivityNature(CreateActivityNatureCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.ActivityNature.Commands.CreateActivityNatureCommand.CreateActivityNatureCommand, CleanArc.Application.Features.ActivityNature.Commands.UpdateActivityNatureCommand.UpdateActivityNatureCommand, CleanArc.Application.Features.ActivityNature.Commands.DeleteActivityNatureCommand.DeleteActivityNatureCommand, System.ResponseEntity, CleanArc.Application.Features.ActivityNature.Queries.GetAllActivityNature.GetAllActivityNatureQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.ActivityNature.Queries.GetAllActivityNature.GetAllActivityNatureQueryResult&gt;, CleanArc.Application.Features.ActivityNature.Queries.GetActivityNatureById.GetActivityNatureByIdQuery, CleanArc.Application.Features.ActivityNature.Queries.GetActivityNatureById.GetActivityNatureByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/ActivityNature")]
    //[Authorize]
    public class ActivityNatureController : _BaseController<CreateActivityNatureCommand, UpdateActivityNatureCommand, DeleteActivityNatureCommand, ResponseEntity, GetAllActivityNatureQuery,
    List<GetAllActivityNatureQueryResult>, GetActivityNatureByIdQuery, GetActivityNatureByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public ActivityNatureController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateActivityNature")]
        //public async Task<IActionResult> CreateActivityNature(CreateActivityNatureCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateActivityNature")]
        //public async Task<IActionResult> UpdateActivityNature(UpdateActivityNatureCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteActivityNature")]
        //public async Task<IActionResult> DeleteActivityNature(DeleteActivityNatureCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllActivityNature")]
        //public async Task<IActionResult> GetAllActivityNature( )
        //{
        //    var queryResult = await _sender.Send(new GetAllActivityNatureQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityNatureController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public ActivityNatureController(ISender sender, ILogger<_BaseController<CreateActivityNatureCommand, UpdateActivityNatureCommand, DeleteActivityNatureCommand, ResponseEntity, GetAllActivityNatureQuery,
   List<GetAllActivityNatureQueryResult>, GetActivityNatureByIdQuery, GetActivityNatureByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
