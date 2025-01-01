using Asp.Versioning;
using CleanArc.Application.Features.KBTiming.Commands.CreateKBTimingCommand;
using CleanArc.Application.Features.KBTiming.Commands.DeleteKBTimingCommand;
using CleanArc.Application.Features.KBTiming.Commands.UpdateKBTimingCommand;
using CleanArc.Application.Features.KBTiming.Queries.GetKBTimingById;
using CleanArc.Application.Features.KBTiming.Queries.GetAllKBTiming;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.KBTiming
{
    /// <summary>
    /// KBTimingController is responsible for handling HTTP requests related to KBTiming operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateKBTiming: Handles the creation of a new age type.
    /// 2. UpdateKBTiming: Handles the updating of an existing age type.
    /// 3. DeleteKBTiming: Handles the deletion of an existing age type.
    /// 4. GetAllKBTiming: Retrieves all age types.
    /// 5. GetKBTimingById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/KBTiming".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateKBTiming")]
    /// public async Task<IActionResult> CreateKBTiming(CreateKBTimingCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.KBTiming.Commands.CreateKBTimingCommand.CreateKBTimingCommand, CleanArc.Application.Features.KBTiming.Commands.UpdateKBTimingCommand.UpdateKBTimingCommand, CleanArc.Application.Features.KBTiming.Commands.DeleteKBTimingCommand.DeleteKBTimingCommand, System.ResponseEntity, CleanArc.Application.Features.KBTiming.Queries.GetAllKBTiming.GetAllKBTimingQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.KBTiming.Queries.GetAllKBTiming.GetAllKBTimingQueryResult&gt;, CleanArc.Application.Features.KBTiming.Queries.GetKBTimingById.GetKBTimingByIdQuery, CleanArc.Application.Features.KBTiming.Queries.GetKBTimingById.GetKBTimingByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/KBTiming")]
    //[Authorize]
    public class KBTimingController : _BaseController<CreateKBTimingCommand, UpdateKBTimingCommand, DeleteKBTimingCommand, ResponseEntity, GetAllKBTimingQuery,
    List<GetAllKBTimingQueryResult>, GetKBTimingByIdQuery, GetKBTimingByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public KBTimingController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateKBTiming")]
        //public async Task<IActionResult> CreateKBTiming(CreateKBTimingCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateKBTiming")]
        //public async Task<IActionResult> UpdateKBTiming(UpdateKBTimingCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteKBTiming")]
        //public async Task<IActionResult> DeleteKBTiming(DeleteKBTimingCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllKBTiming")]
        //public async Task<IActionResult> GetAllKBTiming( )
        //{
        //    var queryResult = await _sender.Send(new GetAllKBTimingQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="KBTimingController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public KBTimingController(ISender sender, ILogger<_BaseController<CreateKBTimingCommand, UpdateKBTimingCommand, DeleteKBTimingCommand, ResponseEntity, GetAllKBTimingQuery,
   List<GetAllKBTimingQueryResult>, GetKBTimingByIdQuery, GetKBTimingByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
