using Asp.Versioning;
using CleanArc.Application.Features.FeedbackStatus.Commands.CreateFeedbackStatusCommand;
using CleanArc.Application.Features.FeedbackStatus.Commands.DeleteFeedbackStatusCommand;
using CleanArc.Application.Features.FeedbackStatus.Commands.UpdateFeedbackStatusCommand;
using CleanArc.Application.Features.FeedbackStatus.Queries.GetFeedbackStatusById;
using CleanArc.Application.Features.FeedbackStatus.Queries.GetAllFeedbackStatus;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.FeedbackStatus
{
    /// <summary>
    /// FeedbackStatusController is responsible for handling HTTP requests related to FeedbackStatus operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateFeedbackStatus: Handles the creation of a new age type.
    /// 2. UpdateFeedbackStatus: Handles the updating of an existing age type.
    /// 3. DeleteFeedbackStatus: Handles the deletion of an existing age type.
    /// 4. GetAllFeedbackStatus: Retrieves all age types.
    /// 5. GetFeedbackStatusById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/FeedbackStatus".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateFeedbackStatus")]
    /// public async Task<IActionResult> CreateFeedbackStatus(CreateFeedbackStatusCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.FeedbackStatus.Commands.CreateFeedbackStatusCommand.CreateFeedbackStatusCommand, CleanArc.Application.Features.FeedbackStatus.Commands.UpdateFeedbackStatusCommand.UpdateFeedbackStatusCommand, CleanArc.Application.Features.FeedbackStatus.Commands.DeleteFeedbackStatusCommand.DeleteFeedbackStatusCommand, System.ResponseEntity, CleanArc.Application.Features.FeedbackStatus.Queries.GetAllFeedbackStatus.GetAllFeedbackStatusQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.FeedbackStatus.Queries.GetAllFeedbackStatus.GetAllFeedbackStatusQueryResult&gt;, CleanArc.Application.Features.FeedbackStatus.Queries.GetFeedbackStatusById.GetFeedbackStatusByIdQuery, CleanArc.Application.Features.FeedbackStatus.Queries.GetFeedbackStatusById.GetFeedbackStatusByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/FeedbackStatus")]
    //[Authorize]
    public class FeedbackStatusController : _BaseController<CreateFeedbackStatusCommand, UpdateFeedbackStatusCommand, DeleteFeedbackStatusCommand, ResponseEntity, GetAllFeedbackStatusQuery,
    List<GetAllFeedbackStatusQueryResult>, GetFeedbackStatusByIdQuery, GetFeedbackStatusByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public FeedbackStatusController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateFeedbackStatus")]
        //public async Task<IActionResult> CreateFeedbackStatus(CreateFeedbackStatusCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateFeedbackStatus")]
        //public async Task<IActionResult> UpdateFeedbackStatus(UpdateFeedbackStatusCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteFeedbackStatus")]
        //public async Task<IActionResult> DeleteFeedbackStatus(DeleteFeedbackStatusCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllFeedbackStatus")]
        //public async Task<IActionResult> GetAllFeedbackStatus( )
        //{
        //    var queryResult = await _sender.Send(new GetAllFeedbackStatusQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackStatusController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public FeedbackStatusController(ISender sender, ILogger<_BaseController<CreateFeedbackStatusCommand, UpdateFeedbackStatusCommand, DeleteFeedbackStatusCommand, ResponseEntity, GetAllFeedbackStatusQuery,
   List<GetAllFeedbackStatusQueryResult>, GetFeedbackStatusByIdQuery, GetFeedbackStatusByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
