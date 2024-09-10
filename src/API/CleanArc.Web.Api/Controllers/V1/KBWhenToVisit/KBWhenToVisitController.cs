using Asp.Versioning;
using CleanArc.Application.Features.KBWhenToVisit.Commands.CreateKBWhenToVisitCommand;
using CleanArc.Application.Features.KBWhenToVisit.Commands.DeleteKBWhenToVisitCommand;
using CleanArc.Application.Features.KBWhenToVisit.Commands.UpdateKBWhenToVisitCommand;
using CleanArc.Application.Features.KBWhenToVisit.Queries.GetKBWhenToVisitById;
using CleanArc.Application.Features.KBWhenToVisit.Queries.GetAllKBWhenToVisit;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.KBWhenToVisit
{
    /// <summary>
    /// KBWhenToVisitController is responsible for handling HTTP requests related to KBWhenToVisit operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateKBWhenToVisit: Handles the creation of a new age type.
    /// 2. UpdateKBWhenToVisit: Handles the updating of an existing age type.
    /// 3. DeleteKBWhenToVisit: Handles the deletion of an existing age type.
    /// 4. GetAllKBWhenToVisit: Retrieves all age types.
    /// 5. GetKBWhenToVisitById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/KBWhenToVisit".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateKBWhenToVisit")]
    /// public async Task<IActionResult> CreateKBWhenToVisit(CreateKBWhenToVisitCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.KBWhenToVisit.Commands.CreateKBWhenToVisitCommand.CreateKBWhenToVisitCommand, CleanArc.Application.Features.KBWhenToVisit.Commands.UpdateKBWhenToVisitCommand.UpdateKBWhenToVisitCommand, CleanArc.Application.Features.KBWhenToVisit.Commands.DeleteKBWhenToVisitCommand.DeleteKBWhenToVisitCommand, System.Boolean, CleanArc.Application.Features.KBWhenToVisit.Queries.GetAllKBWhenToVisit.GetAllKBWhenToVisitQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.KBWhenToVisit.Queries.GetAllKBWhenToVisit.GetAllKBWhenToVisitQueryResult&gt;, CleanArc.Application.Features.KBWhenToVisit.Queries.GetKBWhenToVisitById.GetKBWhenToVisitByIdQuery, CleanArc.Application.Features.KBWhenToVisit.Queries.GetKBWhenToVisitById.GetKBWhenToVisitByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/KBWhenToVisit")]
    //[Authorize]
    public class KBWhenToVisitController : _BaseController<CreateKBWhenToVisitCommand, UpdateKBWhenToVisitCommand, DeleteKBWhenToVisitCommand, bool, GetAllKBWhenToVisitQuery,
    List<GetAllKBWhenToVisitQueryResult>, GetKBWhenToVisitByIdQuery, GetKBWhenToVisitByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public KBWhenToVisitController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateKBWhenToVisit")]
        //public async Task<IActionResult> CreateKBWhenToVisit(CreateKBWhenToVisitCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateKBWhenToVisit")]
        //public async Task<IActionResult> UpdateKBWhenToVisit(UpdateKBWhenToVisitCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteKBWhenToVisit")]
        //public async Task<IActionResult> DeleteKBWhenToVisit(DeleteKBWhenToVisitCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllKBWhenToVisit")]
        //public async Task<IActionResult> GetAllKBWhenToVisit( )
        //{
        //    var queryResult = await _sender.Send(new GetAllKBWhenToVisitQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="KBWhenToVisitController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public KBWhenToVisitController(ISender sender, ILogger<_BaseController<CreateKBWhenToVisitCommand, UpdateKBWhenToVisitCommand, DeleteKBWhenToVisitCommand, bool, GetAllKBWhenToVisitQuery,
   List<GetAllKBWhenToVisitQueryResult>, GetKBWhenToVisitByIdQuery, GetKBWhenToVisitByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
