using Asp.Versioning;
using CleanArc.Application.Features.RefundPolicy.Command.CreateRefundPolicyCommand;
using CleanArc.Application.Features.RefundPolicy.Command.DeleteRefundPolicyCommand;
using CleanArc.Application.Features.RefundPolicy.Command.UpdateRefundPolicyCommand;
using CleanArc.Application.Features.RefundPolicy.Queries.GetAllRefundPolicys;
using CleanArc.Application.Features.RefundPolicy.Queries.GetRefundPolicyById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.RefundPolicy;
/// <summary>
/// RefundPolicyController is responsible for handling HTTP requests related to RefundPolicy operations
/// such as creating, updating, deleting, and retrieving RefundPolicys. It extends from a base controller
/// which provides common functionality for CRUD operations.
/// 
/// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
/// endpoint definitions for the following actions:
/// 
/// 1. CreateRefundPolicy: Handles the creation of a new RefundPolicy.
/// 2. UpdateRefundPolicy: Handles the updating of an existing RefundPolicy.
/// 3. DeleteRefundPolicy: Handles the deletion of an existing RefundPolicy.
/// 4. GetAllRefundPolicys: Retrieves all RefundPolicys.
/// 5. GetRefundPolicyById: Retrieves a specific RefundPolicy by its ID.
/// 
/// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
/// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
/// 
/// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
/// version, specified as "api/v{version:apiVersion}/RefundPolicy".
/// 
/// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
/// operation result handling and user ID setting.
/// 
/// Example Usage:
/// 
/// [HttpPost("CreateRefundPolicy")]
/// public async Task<IActionResult> CreateRefundPolicy(CreateRefundPolicyCommand model)
/// {
///     model.UserId = base.UserId;
///     var command 
///      = await _sender.Send(model);
///     return base.OperationResult(command);
/// }
/// 
/// This ensures that the UserId is set from the base controller before sending the command and that the operation
/// result is properly formatted for the response.
/// 
/// The controller is part of the CleanArc architecture, ensuring a clean separation of concerns and adherence to 
/// SOLID principles.
/// </summary>
/// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.RefundPolicy.Command.CreateRefundPolicyCommand.CreateRefundPolicyCommand, CleanArc.Application.Features.RefundPolicy.Command.UpdateRefundPolicyCommand.UpdateRefundPolicyCommand, CleanArc.Application.Features.RefundPolicy.Command.DeleteRefundPolicyCommand.DeleteRefundPolicyCommand, System.ResponseEntity, CleanArc.Application.Features.RefundPolicy.Queries.GetAllRefundPolicys.GetAllRefundPolicysQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.RefundPolicy.Queries.GetAllRefundPolicys.GetAllRefundPolicysQueryResult&gt;, CleanArc.Application.Features.RefundPolicy.Queries.GetRefundPolicyById.GetRoomViewByIdQuery, CleanArc.Application.Features.RefundPolicy.Queries.GetRefundPolicyById.GetRefundPolicyByIdQueryResult&gt;" />
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/RefundPolicy")]
public class RefundPolicyController : _BaseController<CreateRefundPolicyCommand, UpdateRefundPolicyCommand, DeleteRefundPolicyCommand, ResponseEntity, GetAllRefundPolicysQuery,
    List<GetAllRefundPolicysQueryResult>, GetRefundPolicyByIdQuery, GetRefundPolicyByIdQueryResult>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RefundPolicyController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public RefundPolicyController(ISender sender, ILogger<_BaseController<CreateRefundPolicyCommand, UpdateRefundPolicyCommand, DeleteRefundPolicyCommand, ResponseEntity, GetAllRefundPolicysQuery,
List<GetAllRefundPolicysQueryResult>, GetRefundPolicyByIdQuery, GetRefundPolicyByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}

