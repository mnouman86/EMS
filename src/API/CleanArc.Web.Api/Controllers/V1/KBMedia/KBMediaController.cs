using Asp.Versioning;
using CleanArc.Application.Features.KBMedia.Commands.CreateKBMediaCommand;
using CleanArc.Application.Features.KBMedia.Commands.DeleteKBMediaCommand;
using CleanArc.Application.Features.KBMedia.Commands.UpdateKBMediaCommand;
using CleanArc.Application.Features.KBMedia.Queries.GetKBMediaById;
using CleanArc.Application.Features.KBMedia.Queries.GetAllKBMedia;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.KBMedia
{
    /// <summary>
    /// KBMediaController is responsible for handling HTTP requests related to KBMedia operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateKBMedia: Handles the creation of a new age type.
    /// 2. UpdateKBMedia: Handles the updating of an existing age type.
    /// 3. DeleteKBMedia: Handles the deletion of an existing age type.
    /// 4. GetAllKBMedia: Retrieves all age types.
    /// 5. GetKBMediaById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/KBMedia".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateKBMedia")]
    /// public async Task<IActionResult> CreateKBMedia(CreateKBMediaCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.KBMedia.Commands.CreateKBMediaCommand.CreateKBMediaCommand, CleanArc.Application.Features.KBMedia.Commands.UpdateKBMediaCommand.UpdateKBMediaCommand, CleanArc.Application.Features.KBMedia.Commands.DeleteKBMediaCommand.DeleteKBMediaCommand, System.Boolean, CleanArc.Application.Features.KBMedia.Queries.GetAllKBMedia.GetAllKBMediaQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.KBMedia.Queries.GetAllKBMedia.GetAllKBMediaQueryResult&gt;, CleanArc.Application.Features.KBMedia.Queries.GetKBMediaById.GetKBMediaByIdQuery, CleanArc.Application.Features.KBMedia.Queries.GetKBMediaById.GetKBMediaByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/KBMedia")]
    //[Authorize]
    public class KBMediaController : _BaseController<CreateKBMediaCommand, UpdateKBMediaCommand, DeleteKBMediaCommand, bool, GetAllKBMediaQuery,
    List<GetAllKBMediaQueryResult>, GetKBMediaByIdQuery, GetKBMediaByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public KBMediaController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateKBMedia")]
        //public async Task<IActionResult> CreateKBMedia(CreateKBMediaCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateKBMedia")]
        //public async Task<IActionResult> UpdateKBMedia(UpdateKBMediaCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteKBMedia")]
        //public async Task<IActionResult> DeleteKBMedia(DeleteKBMediaCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllKBMedia")]
        //public async Task<IActionResult> GetAllKBMedia( )
        //{
        //    var queryResult = await _sender.Send(new GetAllKBMediaQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="KBMediaController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public KBMediaController(ISender sender, ILogger<_BaseController<CreateKBMediaCommand, UpdateKBMediaCommand, DeleteKBMediaCommand, bool, GetAllKBMediaQuery,
   List<GetAllKBMediaQueryResult>, GetKBMediaByIdQuery, GetKBMediaByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
