using Asp.Versioning;
using CleanArc.Application.Features.FeedbackSubjectType.Commands.CreateFeedbackSubjectTypeCommand;
using CleanArc.Application.Features.FeedbackSubjectType.Commands.DeleteFeedbackSubjectTypeCommand;
using CleanArc.Application.Features.FeedbackSubjectType.Commands.UpdateFeedbackSubjectTypeCommand;
using CleanArc.Application.Features.FeedbackSubjectType.Queries.GetFeedbackSubjectTypeById;
using CleanArc.Application.Features.FeedbackSubjectType.Queries.GetAllFeedbackSubjectType;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.FeedbackSubjectType
{
    /// <summary>
    /// FeedbackSubjectTypeController is responsible for handling HTTP requests related to FeedbackSubjectType operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateFeedbackSubjectType: Handles the creation of a new age type.
    /// 2. UpdateFeedbackSubjectType: Handles the updating of an existing age type.
    /// 3. DeleteFeedbackSubjectType: Handles the deletion of an existing age type.
    /// 4. GetAllFeedbackSubjectType: Retrieves all age types.
    /// 5. GetFeedbackSubjectTypeById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/FeedbackSubjectType".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateFeedbackSubjectType")]
    /// public async Task<IActionResult> CreateFeedbackSubjectType(CreateFeedbackSubjectTypeCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.FeedbackSubjectType.Commands.CreateFeedbackSubjectTypeCommand.CreateFeedbackSubjectTypeCommand, CleanArc.Application.Features.FeedbackSubjectType.Commands.UpdateFeedbackSubjectTypeCommand.UpdateFeedbackSubjectTypeCommand, CleanArc.Application.Features.FeedbackSubjectType.Commands.DeleteFeedbackSubjectTypeCommand.DeleteFeedbackSubjectTypeCommand, System.ResponseEntity, CleanArc.Application.Features.FeedbackSubjectType.Queries.GetAllFeedbackSubjectType.GetAllFeedbackSubjectTypeQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.FeedbackSubjectType.Queries.GetAllFeedbackSubjectType.GetAllFeedbackSubjectTypeQueryResult&gt;, CleanArc.Application.Features.FeedbackSubjectType.Queries.GetFeedbackSubjectTypeById.GetFeedbackSubjectTypeByIdQuery, CleanArc.Application.Features.FeedbackSubjectType.Queries.GetFeedbackSubjectTypeById.GetFeedbackSubjectTypeByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/FeedbackSubjectType")]
    //[Authorize]
    public class FeedbackSubjectTypeController : _BaseController<CreateFeedbackSubjectTypeCommand, UpdateFeedbackSubjectTypeCommand, DeleteFeedbackSubjectTypeCommand, ResponseEntity, GetAllFeedbackSubjectTypeQuery,
    List<GetAllFeedbackSubjectTypeQueryResult>, GetFeedbackSubjectTypeByIdQuery, GetFeedbackSubjectTypeByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public FeedbackSubjectTypeController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateFeedbackSubjectType")]
        //public async Task<IActionResult> CreateFeedbackSubjectType(CreateFeedbackSubjectTypeCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateFeedbackSubjectType")]
        //public async Task<IActionResult> UpdateFeedbackSubjectType(UpdateFeedbackSubjectTypeCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteFeedbackSubjectType")]
        //public async Task<IActionResult> DeleteFeedbackSubjectType(DeleteFeedbackSubjectTypeCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllFeedbackSubjectType")]
        //public async Task<IActionResult> GetAllFeedbackSubjectType( )
        //{
        //    var queryResult = await _sender.Send(new GetAllFeedbackSubjectTypeQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackSubjectTypeController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public FeedbackSubjectTypeController(ISender sender, ILogger<_BaseController<CreateFeedbackSubjectTypeCommand, UpdateFeedbackSubjectTypeCommand, DeleteFeedbackSubjectTypeCommand, ResponseEntity, GetAllFeedbackSubjectTypeQuery,
   List<GetAllFeedbackSubjectTypeQueryResult>, GetFeedbackSubjectTypeByIdQuery, GetFeedbackSubjectTypeByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
