using Asp.Versioning;
using CleanArc.Application.Features.CustomerAwareness.Command.CreateCustomerAwarenessCommand;
using CleanArc.Application.Features.CustomerAwareness.Command.DeleteCustomerAwarenessCommand;
using CleanArc.Application.Features.CustomerAwareness.Command.UpdateCustomerAwarenessCommand;
using CleanArc.Application.Features.CustomerAwareness.Queries.GetAllCustomerAwarenesss;
using CleanArc.Application.Features.CustomerAwareness.Queries.GetCustomerAwarenessById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.CustomerAwareness
{
    /// <summary>
    /// CustomerAwarenessController is responsible for handling HTTP requests related to room type operations
    /// such as creating, updating, deleting, and retrieving room types. It extends from a base controller
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateCustomerAwareness: Handles the creation of a new room type.
    /// 2. UpdateCustomerAwareness: Handles the updating of an existing room type.
    /// 3. DeleteCustomerAwareness: Handles the deletion of an existing room type.
    /// 4. GetAllCustomerAwarenesss: Retrieves all room types.
    /// 5. GetCustomerAwarenessById: Retrieves a specific room type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/CustomerAwareness".
    /// 
    /// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
    /// operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateCustomerAwareness")]
    /// public async Task<IActionResult> CreateCustomerAwareness(CreateCustomerAwarenessCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.CustomerAwareness.Command.CreateCustomerAwarenessCommand.CreateCustomerAwarenessCommand, CleanArc.Application.Features.CustomerAwareness.Command.UpdateCustomerAwarenessCommand.UpdateCustomerAwarenessCommand, CleanArc.Application.Features.CustomerAwareness.Command.DeleteCustomerAwarenessCommand.DeleteCustomerAwarenessCommand, System.ResponseEntity, CleanArc.Application.Features.CustomerAwareness.Queries.GetAllCustomerAwarenesss.GetAllCustomerAwarenesssQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.CustomerAwareness.Queries.GetAllCustomerAwarenesss.GetAllCustomerAwarenesssQueryResult&gt;, CleanArc.Application.Features.CustomerAwareness.Queries.GetCustomerAwarenessById.GetCustomerAwarenessByIdQuery, CleanArc.Application.Features.CustomerAwareness.Queries.GetCustomerAwarenessById.GetCustomerAwarenessByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/CustomerAwareness")]
    //[Authorize]
    public class CustomerAwarenessController : _BaseController<CreateCustomerAwarenessCommand, UpdateCustomerAwarenessCommand, DeleteCustomerAwarenessCommand, ResponseEntity, GetAllCustomerAwarenesssQuery,
    List<GetAllCustomerAwarenesssQueryResult>, GetCustomerAwarenessByIdQuery, GetCustomerAwarenessByIdQueryResult>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerAwarenessController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public CustomerAwarenessController(ISender sender, ILogger<_BaseController<CreateCustomerAwarenessCommand, UpdateCustomerAwarenessCommand, DeleteCustomerAwarenessCommand, ResponseEntity, GetAllCustomerAwarenesssQuery,
   List<GetAllCustomerAwarenesssQueryResult>, GetCustomerAwarenessByIdQuery, GetCustomerAwarenessByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
