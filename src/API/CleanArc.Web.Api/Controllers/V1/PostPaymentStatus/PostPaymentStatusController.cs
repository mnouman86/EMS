using Asp.Versioning;
using CleanArc.Application.Features.PostPaymentStatus.Commands.CreatePostPaymentStatusCommand;
using CleanArc.Application.Features.PostPaymentStatus.Commands.DeletePostPaymentStatusCommand;
using CleanArc.Application.Features.PostPaymentStatus.Commands.UpdatePostPaymentStatusCommand;
using CleanArc.Application.Features.PostPaymentStatus.Queries.GetPostPaymentStatusById;
using CleanArc.Application.Features.PostPaymentStatus.Queries.GetAllPostPaymentStatus;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; 
using CleanArc.Domain.Common;
using Microsoft.AspNetCore.Authorization;

namespace CleanArc.Web.Api.Controllers.V1.PostPaymentStatus
{
    /// <summary>
    /// PostPaymentStatusController is responsible for handling HTTP requests related to PostPaymentStatus operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreatePostPaymentStatus: Handles the creation of a new age type.
    /// 2. UpdatePostPaymentStatus: Handles the updating of an existing age type.
    /// 3. DeletePostPaymentStatus: Handles the deletion of an existing age type.
    /// 4. GetAllPostPaymentStatus: Retrieves all age types.
    /// 5. GetPostPaymentStatusById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/PostPaymentStatus".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreatePostPaymentStatus")]
    /// public async Task<IActionResult> CreatePostPaymentStatus(CreatePostPaymentStatusCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.PostPaymentStatus.Commands.CreatePostPaymentStatusCommand.CreatePostPaymentStatusCommand, CleanArc.Application.Features.PostPaymentStatus.Commands.UpdatePostPaymentStatusCommand.UpdatePostPaymentStatusCommand, CleanArc.Application.Features.PostPaymentStatus.Commands.DeletePostPaymentStatusCommand.DeletePostPaymentStatusCommand, System.ResponseEntity, CleanArc.Application.Features.PostPaymentStatus.Queries.GetAllPostPaymentStatus.GetAllPostPaymentStatusQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.PostPaymentStatus.Queries.GetAllPostPaymentStatus.GetAllPostPaymentStatusQueryResult&gt;, CleanArc.Application.Features.PostPaymentStatus.Queries.GetPostPaymentStatusById.GetPostPaymentStatusByIdQuery, CleanArc.Application.Features.PostPaymentStatus.Queries.GetPostPaymentStatusById.GetPostPaymentStatusByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/PostPaymentStatus")]
    [AllowAnonymous]
    //[Authorize]
    public class PostPaymentStatusController : _BaseController<CreatePostPaymentStatusCommand, UpdatePostPaymentStatusCommand, DeletePostPaymentStatusCommand, ResponseEntity, GetAllPostPaymentStatusQuery,
    List<GetAllPostPaymentStatusQueryResult>, GetPostPaymentStatusByIdQuery, GetPostPaymentStatusByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public PostPaymentStatusController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreatePostPaymentStatus")]
        //public async Task<IActionResult> CreatePostPaymentStatus(CreatePostPaymentStatusCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdatePostPaymentStatus")]
        //public async Task<IActionResult> UpdatePostPaymentStatus(UpdatePostPaymentStatusCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeletePostPaymentStatus")]
        //public async Task<IActionResult> DeletePostPaymentStatus(DeletePostPaymentStatusCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllPostPaymentStatus")]
        //public async Task<IActionResult> GetAllPostPaymentStatus( )
        //{
        //    var queryResult = await _sender.Send(new GetAllPostPaymentStatusQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="PostPaymentStatusController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public PostPaymentStatusController(ISender sender, ILogger<_BaseController<CreatePostPaymentStatusCommand, UpdatePostPaymentStatusCommand, DeletePostPaymentStatusCommand, ResponseEntity, GetAllPostPaymentStatusQuery,
   List<GetAllPostPaymentStatusQueryResult>, GetPostPaymentStatusByIdQuery, GetPostPaymentStatusByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
