using Asp.Versioning;
using CleanArc.Application.Features.KBDescription.Commands.CreateKBDescriptionCommand;
using CleanArc.Application.Features.KBDescription.Commands.DeleteKBDescriptionCommand;
using CleanArc.Application.Features.KBDescription.Commands.UpdateKBDescriptionCommand;
using CleanArc.Application.Features.KBDescription.Queries.GetKBDescriptionById;
using CleanArc.Application.Features.KBDescription.Queries.GetAllKBDescription;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.KBDescription
{
    /// <summary>
    /// KBDescriptionController is responsible for handling HTTP requests related to KBDescription operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateKBDescription: Handles the creation of a new age type.
    /// 2. UpdateKBDescription: Handles the updating of an existing age type.
    /// 3. DeleteKBDescription: Handles the deletion of an existing age type.
    /// 4. GetAllKBDescription: Retrieves all age types.
    /// 5. GetKBDescriptionById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/KBDescription".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateKBDescription")]
    /// public async Task<IActionResult> CreateKBDescription(CreateKBDescriptionCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.KBDescription.Commands.CreateKBDescriptionCommand.CreateKBDescriptionCommand, CleanArc.Application.Features.KBDescription.Commands.UpdateKBDescriptionCommand.UpdateKBDescriptionCommand, CleanArc.Application.Features.KBDescription.Commands.DeleteKBDescriptionCommand.DeleteKBDescriptionCommand, System.ResponseEntity, CleanArc.Application.Features.KBDescription.Queries.GetAllKBDescription.GetAllKBDescriptionQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.KBDescription.Queries.GetAllKBDescription.GetAllKBDescriptionQueryResult&gt;, CleanArc.Application.Features.KBDescription.Queries.GetKBDescriptionById.GetKBDescriptionByIdQuery, CleanArc.Application.Features.KBDescription.Queries.GetKBDescriptionById.GetKBDescriptionByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/KBDescription")]
    //[Authorize]
    public class KBDescriptionController : _BaseController<CreateKBDescriptionCommand, UpdateKBDescriptionCommand, DeleteKBDescriptionCommand, ResponseEntity, GetAllKBDescriptionQuery,
    List<GetAllKBDescriptionQueryResult>, GetKBDescriptionByIdQuery, GetKBDescriptionByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public KBDescriptionController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateKBDescription")]
        //public async Task<IActionResult> CreateKBDescription(CreateKBDescriptionCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateKBDescription")]
        //public async Task<IActionResult> UpdateKBDescription(UpdateKBDescriptionCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteKBDescription")]
        //public async Task<IActionResult> DeleteKBDescription(DeleteKBDescriptionCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllKBDescription")]
        //public async Task<IActionResult> GetAllKBDescription( )
        //{
        //    var queryResult = await _sender.Send(new GetAllKBDescriptionQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="KBDescriptionController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public KBDescriptionController(ISender sender, ILogger<_BaseController<CreateKBDescriptionCommand, UpdateKBDescriptionCommand, DeleteKBDescriptionCommand, ResponseEntity, GetAllKBDescriptionQuery,
   List<GetAllKBDescriptionQueryResult>, GetKBDescriptionByIdQuery, GetKBDescriptionByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
