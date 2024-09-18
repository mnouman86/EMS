using Asp.Versioning;
using CleanArc.Application.Features.CheckProfileStatus.Commands.CreateCheckProfileStatusCommand;
using CleanArc.Application.Features.CheckProfileStatus.Commands.DeleteCheckProfileStatusCommand;
using CleanArc.Application.Features.CheckProfileStatus.Commands.UpdateCheckProfileStatusCommand;
using CleanArc.Application.Features.CheckProfileStatus.Queries.GetCheckProfileStatusById;
using CleanArc.Application.Features.CheckProfileStatus.Queries.GetAllCheckProfileStatus;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.CheckProfileStatus
{
    /// <summary>
    /// CheckProfileStatusController is responsible for handling HTTP requests related to CheckProfileStatus operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateCheckProfileStatus: Handles the creation of a new age type.
    /// 2. UpdateCheckProfileStatus: Handles the updating of an existing age type.
    /// 3. DeleteCheckProfileStatus: Handles the deletion of an existing age type.
    /// 4. GetAllCheckProfileStatus: Retrieves all age types.
    /// 5. GetCheckProfileStatusById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/CheckProfileStatus".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateCheckProfileStatus")]
    /// public async Task<IActionResult> CreateCheckProfileStatus(CreateCheckProfileStatusCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.CheckProfileStatus.Commands.CreateCheckProfileStatusCommand.CreateCheckProfileStatusCommand, CleanArc.Application.Features.CheckProfileStatus.Commands.UpdateCheckProfileStatusCommand.UpdateCheckProfileStatusCommand, CleanArc.Application.Features.CheckProfileStatus.Commands.DeleteCheckProfileStatusCommand.DeleteCheckProfileStatusCommand, System.ResponseEntity, CleanArc.Application.Features.CheckProfileStatus.Queries.GetAllCheckProfileStatus.GetAllCheckProfileStatusQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.CheckProfileStatus.Queries.GetAllCheckProfileStatus.GetAllCheckProfileStatusQueryResult&gt;, CleanArc.Application.Features.CheckProfileStatus.Queries.GetCheckProfileStatusById.GetCheckProfileStatusByIdQuery, CleanArc.Application.Features.CheckProfileStatus.Queries.GetCheckProfileStatusById.GetCheckProfileStatusByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/CheckProfileStatus")]
    //[Authorize]
    public class CheckProfileStatusController : _BaseController<CreateCheckProfileStatusCommand, UpdateCheckProfileStatusCommand, DeleteCheckProfileStatusCommand, ResponseEntity, GetAllCheckProfileStatusQuery,
    List<GetAllCheckProfileStatusQueryResult>, GetCheckProfileStatusByIdQuery, GetCheckProfileStatusByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public CheckProfileStatusController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateCheckProfileStatus")]
        //public async Task<IActionResult> CreateCheckProfileStatus(CreateCheckProfileStatusCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateCheckProfileStatus")]
        //public async Task<IActionResult> UpdateCheckProfileStatus(UpdateCheckProfileStatusCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteCheckProfileStatus")]
        //public async Task<IActionResult> DeleteCheckProfileStatus(DeleteCheckProfileStatusCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllCheckProfileStatus")]
        //public async Task<IActionResult> GetAllCheckProfileStatus( )
        //{
        //    var queryResult = await _sender.Send(new GetAllCheckProfileStatusQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckProfileStatusController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public CheckProfileStatusController(ISender sender, ILogger<_BaseController<CreateCheckProfileStatusCommand, UpdateCheckProfileStatusCommand, DeleteCheckProfileStatusCommand, ResponseEntity, GetAllCheckProfileStatusQuery,
   List<GetAllCheckProfileStatusQueryResult>, GetCheckProfileStatusByIdQuery, GetCheckProfileStatusByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
