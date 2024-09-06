using Asp.Versioning;
using CleanArc.Application.Features.KBCAttraction.Commands.CreateKBCAttractionCommand;
using CleanArc.Application.Features.KBCAttraction.Commands.DeleteKBCAttractionCommand;
using CleanArc.Application.Features.KBCAttraction.Commands.UpdateKBCAttractionCommand;
using CleanArc.Application.Features.KBCAttraction.Queries.GetKBCAttractionById;
using CleanArc.Application.Features.KBCAttraction.Queries.GetAllKBCAttraction;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.KBCAttraction
{
    /// <summary>
    /// KBCAttractionController is responsible for handling HTTP requests related to KBCAttraction operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateKBCAttraction: Handles the creation of a new age type.
    /// 2. UpdateKBCAttraction: Handles the updating of an existing age type.
    /// 3. DeleteKBCAttraction: Handles the deletion of an existing age type.
    /// 4. GetAllKBCAttraction: Retrieves all age types.
    /// 5. GetKBCAttractionById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/KBCAttraction".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateKBCAttraction")]
    /// public async Task<IActionResult> CreateKBCAttraction(CreateKBCAttractionCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.KBCAttraction.Commands.CreateKBCAttractionCommand.CreateKBCAttractionCommand, CleanArc.Application.Features.KBCAttraction.Commands.UpdateKBCAttractionCommand.UpdateKBCAttractionCommand, CleanArc.Application.Features.KBCAttraction.Commands.DeleteKBCAttractionCommand.DeleteKBCAttractionCommand, System.Boolean, CleanArc.Application.Features.KBCAttraction.Queries.GetAllKBCAttraction.GetAllKBCAttractionQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.KBCAttraction.Queries.GetAllKBCAttraction.GetAllKBCAttractionQueryResult&gt;, CleanArc.Application.Features.KBCAttraction.Queries.GetKBCAttractionById.GetKBCAttractionByIdQuery, CleanArc.Application.Features.KBCAttraction.Queries.GetKBCAttractionById.GetKBCAttractionByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/KBCAttraction")]
    //[Authorize]
    public class KBCAttractionController : _BaseController<CreateKBCAttractionCommand, UpdateKBCAttractionCommand, DeleteKBCAttractionCommand, bool, GetAllKBCAttractionQuery,
    List<GetAllKBCAttractionQueryResult>, GetKBCAttractionByIdQuery, GetKBCAttractionByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public KBCAttractionController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateKBCAttraction")]
        //public async Task<IActionResult> CreateKBCAttraction(CreateKBCAttractionCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateKBCAttraction")]
        //public async Task<IActionResult> UpdateKBCAttraction(UpdateKBCAttractionCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteKBCAttraction")]
        //public async Task<IActionResult> DeleteKBCAttraction(DeleteKBCAttractionCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllKBCAttraction")]
        //public async Task<IActionResult> GetAllKBCAttraction( )
        //{
        //    var queryResult = await _sender.Send(new GetAllKBCAttractionQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="KBCAttractionController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public KBCAttractionController(ISender sender, ILogger<_BaseController<CreateKBCAttractionCommand, UpdateKBCAttractionCommand, DeleteKBCAttractionCommand, bool, GetAllKBCAttractionQuery,
   List<GetAllKBCAttractionQueryResult>, GetKBCAttractionByIdQuery, GetKBCAttractionByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
