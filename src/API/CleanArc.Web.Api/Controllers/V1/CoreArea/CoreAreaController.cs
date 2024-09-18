using Asp.Versioning;
using CleanArc.Application.Features.CoreArea.Commands.CreateCoreAreaCommand;
using CleanArc.Application.Features.CoreArea.Commands.DeleteCoreAreaCommand;
using CleanArc.Application.Features.CoreArea.Commands.UpdateCoreAreaCommand;
using CleanArc.Application.Features.CoreArea.Queries.GetCoreAreaById;
using CleanArc.Application.Features.CoreArea.Queries.GetAllCoreArea;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.CoreArea
{
    /// <summary>
    /// CoreAreaController is responsible for handling HTTP requests related to CoreArea operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateCoreArea: Handles the creation of a new age type.
    /// 2. UpdateCoreArea: Handles the updating of an existing age type.
    /// 3. DeleteCoreArea: Handles the deletion of an existing age type.
    /// 4. GetAllCoreArea: Retrieves all age types.
    /// 5. GetCoreAreaById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/CoreArea".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateCoreArea")]
    /// public async Task<IActionResult> CreateCoreArea(CreateCoreAreaCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.CoreArea.Commands.CreateCoreAreaCommand.CreateCoreAreaCommand, CleanArc.Application.Features.CoreArea.Commands.UpdateCoreAreaCommand.UpdateCoreAreaCommand, CleanArc.Application.Features.CoreArea.Commands.DeleteCoreAreaCommand.DeleteCoreAreaCommand, System.ResponseEntity, CleanArc.Application.Features.CoreArea.Queries.GetAllCoreArea.GetAllCoreAreaQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.CoreArea.Queries.GetAllCoreArea.GetAllCoreAreaQueryResult&gt;, CleanArc.Application.Features.CoreArea.Queries.GetCoreAreaById.GetCoreAreaByIdQuery, CleanArc.Application.Features.CoreArea.Queries.GetCoreAreaById.GetCoreAreaByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/CoreArea")]
    //[Authorize]
    public class CoreAreaController : _BaseController<CreateCoreAreaCommand, UpdateCoreAreaCommand, DeleteCoreAreaCommand, ResponseEntity, GetAllCoreAreaQuery,
    List<GetAllCoreAreaQueryResult>, GetCoreAreaByIdQuery, GetCoreAreaByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public CoreAreaController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateCoreArea")]
        //public async Task<IActionResult> CreateCoreArea(CreateCoreAreaCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateCoreArea")]
        //public async Task<IActionResult> UpdateCoreArea(UpdateCoreAreaCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteCoreArea")]
        //public async Task<IActionResult> DeleteCoreArea(DeleteCoreAreaCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllCoreArea")]
        //public async Task<IActionResult> GetAllCoreArea( )
        //{
        //    var queryResult = await _sender.Send(new GetAllCoreAreaQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="CoreAreaController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public CoreAreaController(ISender sender, ILogger<_BaseController<CreateCoreAreaCommand, UpdateCoreAreaCommand, DeleteCoreAreaCommand, ResponseEntity, GetAllCoreAreaQuery,
   List<GetAllCoreAreaQueryResult>, GetCoreAreaByIdQuery, GetCoreAreaByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
