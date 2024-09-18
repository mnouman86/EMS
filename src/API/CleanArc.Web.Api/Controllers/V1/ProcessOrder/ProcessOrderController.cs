using Asp.Versioning;
using CleanArc.Application.Features.ProcessOrder.Commands.CreateProcessOrderCommand;
using CleanArc.Application.Features.ProcessOrder.Commands.DeleteProcessOrderCommand;
using CleanArc.Application.Features.ProcessOrder.Commands.UpdateProcessOrderCommand;
using CleanArc.Application.Features.ProcessOrder.Queries.GetProcessOrderById;
using CleanArc.Application.Features.ProcessOrder.Queries.GetAllProcessOrder;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.ProcessOrder
{
    /// <summary>
    /// ProcessOrderController is responsible for handling HTTP requests related to ProcessOrder operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateProcessOrder: Handles the creation of a new age type.
    /// 2. UpdateProcessOrder: Handles the updating of an existing age type.
    /// 3. DeleteProcessOrder: Handles the deletion of an existing age type.
    /// 4. GetAllProcessOrder: Retrieves all age types.
    /// 5. GetProcessOrderById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/ProcessOrder".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateProcessOrder")]
    /// public async Task<IActionResult> CreateProcessOrder(CreateProcessOrderCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.ProcessOrder.Commands.CreateProcessOrderCommand.CreateProcessOrderCommand, CleanArc.Application.Features.ProcessOrder.Commands.UpdateProcessOrderCommand.UpdateProcessOrderCommand, CleanArc.Application.Features.ProcessOrder.Commands.DeleteProcessOrderCommand.DeleteProcessOrderCommand, System.ResponseEntity, CleanArc.Application.Features.ProcessOrder.Queries.GetAllProcessOrder.GetAllProcessOrderQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.ProcessOrder.Queries.GetAllProcessOrder.GetAllProcessOrderQueryResult&gt;, CleanArc.Application.Features.ProcessOrder.Queries.GetProcessOrderById.GetProcessOrderByIdQuery, CleanArc.Application.Features.ProcessOrder.Queries.GetProcessOrderById.GetProcessOrderByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/ProcessOrder")]
    //[Authorize]
    public class ProcessOrderController : _BaseController<CreateProcessOrderCommand, UpdateProcessOrderCommand, DeleteProcessOrderCommand, ResponseEntity, GetAllProcessOrderQuery,
    List<GetAllProcessOrderQueryResult>, GetProcessOrderByIdQuery, GetProcessOrderByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public ProcessOrderController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateProcessOrder")]
        //public async Task<IActionResult> CreateProcessOrder(CreateProcessOrderCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateProcessOrder")]
        //public async Task<IActionResult> UpdateProcessOrder(UpdateProcessOrderCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteProcessOrder")]
        //public async Task<IActionResult> DeleteProcessOrder(DeleteProcessOrderCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllProcessOrder")]
        //public async Task<IActionResult> GetAllProcessOrder( )
        //{
        //    var queryResult = await _sender.Send(new GetAllProcessOrderQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessOrderController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public ProcessOrderController(ISender sender, ILogger<_BaseController<CreateProcessOrderCommand, UpdateProcessOrderCommand, DeleteProcessOrderCommand, ResponseEntity, GetAllProcessOrderQuery,
   List<GetAllProcessOrderQueryResult>, GetProcessOrderByIdQuery, GetProcessOrderByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
