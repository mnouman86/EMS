using Asp.Versioning;
using CleanArc.Application.Features.KBInterested.Commands.CreateKBInterestedCommand;
using CleanArc.Application.Features.KBInterested.Commands.DeleteKBInterestedCommand;
using CleanArc.Application.Features.KBInterested.Commands.UpdateKBInterestedCommand;
using CleanArc.Application.Features.KBInterested.Queries.GetKBInterestedById;
using CleanArc.Application.Features.KBInterested.Queries.GetAllKBInterested;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.KBInterested
{
    /// <summary>
    /// KBInterestedController is responsible for handling HTTP requests related to KBInterested operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateKBInterested: Handles the creation of a new age type.
    /// 2. UpdateKBInterested: Handles the updating of an existing age type.
    /// 3. DeleteKBInterested: Handles the deletion of an existing age type.
    /// 4. GetAllKBInterested: Retrieves all age types.
    /// 5. GetKBInterestedById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/KBInterested".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateKBInterested")]
    /// public async Task<IActionResult> CreateKBInterested(CreateKBInterestedCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.KBInterested.Commands.CreateKBInterestedCommand.CreateKBInterestedCommand, CleanArc.Application.Features.KBInterested.Commands.UpdateKBInterestedCommand.UpdateKBInterestedCommand, CleanArc.Application.Features.KBInterested.Commands.DeleteKBInterestedCommand.DeleteKBInterestedCommand, System.ResponseEntity, CleanArc.Application.Features.KBInterested.Queries.GetAllKBInterested.GetAllKBInterestedQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.KBInterested.Queries.GetAllKBInterested.GetAllKBInterestedQueryResult&gt;, CleanArc.Application.Features.KBInterested.Queries.GetKBInterestedById.GetKBInterestedByIdQuery, CleanArc.Application.Features.KBInterested.Queries.GetKBInterestedById.GetKBInterestedByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/KBInterested")]
    //[Authorize]
    public class KBInterestedController : _BaseController<CreateKBInterestedCommand, UpdateKBInterestedCommand, DeleteKBInterestedCommand, ResponseEntity, GetAllKBInterestedQuery,
    List<GetAllKBInterestedQueryResult>, GetKBInterestedByIdQuery, GetKBInterestedByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public KBInterestedController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateKBInterested")]
        //public async Task<IActionResult> CreateKBInterested(CreateKBInterestedCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateKBInterested")]
        //public async Task<IActionResult> UpdateKBInterested(UpdateKBInterestedCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteKBInterested")]
        //public async Task<IActionResult> DeleteKBInterested(DeleteKBInterestedCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllKBInterested")]
        //public async Task<IActionResult> GetAllKBInterested( )
        //{
        //    var queryResult = await _sender.Send(new GetAllKBInterestedQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="KBInterestedController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public KBInterestedController(ISender sender, ILogger<_BaseController<CreateKBInterestedCommand, UpdateKBInterestedCommand, DeleteKBInterestedCommand, ResponseEntity, GetAllKBInterestedQuery,
   List<GetAllKBInterestedQueryResult>, GetKBInterestedByIdQuery, GetKBInterestedByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
