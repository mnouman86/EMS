using Asp.Versioning;
using CleanArc.Application.Features.NoShowPolicy.Command.CreateNoShowPolicyCommand;
using CleanArc.Application.Features.NoShowPolicy.Command.DeleteNoShowPolicyCommand;
using CleanArc.Application.Features.NoShowPolicy.Command.UpdateNoShowPolicyCommand;
using CleanArc.Application.Features.NoShowPolicy.Queries.GetAllNoShowPolicys;
using CleanArc.Application.Features.NoShowPolicy.Queries.GetNoShowPolicyById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.NoShowPolicy;
/// <summary>
/// NoShowPolicyController is responsible for handling HTTP requests related to NoShowPolicy operations
/// such as creating, updating, deleting, and retrieving NoShowPolicys. It extends from a base controller
/// which provides common functionality for CRUD operations.
/// 
/// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
/// endpoint definitions for the following actions:
/// 
/// 1. CreateNoShowPolicy: Handles the creation of a new NoShowPolicy.
/// 2. UpdateNoShowPolicy: Handles the updating of an existing NoShowPolicy.
/// 3. DeleteNoShowPolicy: Handles the deletion of an existing NoShowPolicy.
/// 4. GetAllNoShowPolicys: Retrieves all NoShowPolicys.
/// 5. GetNoShowPolicyById: Retrieves a specific NoShowPolicy by its ID.
/// 
/// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
/// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
/// 
/// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
/// version, specified as "api/v{version:apiVersion}/NoShowPolicy".
/// 
/// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
/// operation result handling and user ID setting.
/// 
/// Example Usage:
/// 
/// [HttpPost("CreateNoShowPolicy")]
/// public async Task<IActionResult> CreateNoShowPolicy(CreateNoShowPolicyCommand model)
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
/// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.NoShowPolicy.Command.CreateNoShowPolicyCommand.CreateNoShowPolicyCommand, CleanArc.Application.Features.NoShowPolicy.Command.UpdateNoShowPolicyCommand.UpdateNoShowPolicyCommand, CleanArc.Application.Features.NoShowPolicy.Command.DeleteNoShowPolicyCommand.DeleteNoShowPolicyCommand, System.ResponseEntity, CleanArc.Application.Features.NoShowPolicy.Queries.GetAllNoShowPolicys.GetAllNoShowPolicysQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.NoShowPolicy.Queries.GetAllNoShowPolicys.GetAllNoShowPolicysQueryResult&gt;, CleanArc.Application.Features.NoShowPolicy.Queries.GetNoShowPolicyById.GetRoomViewByIdQuery, CleanArc.Application.Features.NoShowPolicy.Queries.GetNoShowPolicyById.GetNoShowPolicyByIdQueryResult&gt;" />
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/NoShowPolicy")]
public class NoShowPolicyController : _BaseController<CreateNoShowPolicyCommand, UpdateNoShowPolicyCommand, DeleteNoShowPolicyCommand, ResponseEntity, GetAllNoShowPolicysQuery,
    List<GetAllNoShowPolicysQueryResult>, GetNoShowPolicyByIdQuery, GetNoShowPolicyByIdQueryResult>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NoShowPolicyController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public NoShowPolicyController(ISender sender, ILogger<_BaseController<CreateNoShowPolicyCommand, UpdateNoShowPolicyCommand, DeleteNoShowPolicyCommand, ResponseEntity, GetAllNoShowPolicysQuery,
List<GetAllNoShowPolicysQueryResult>, GetNoShowPolicyByIdQuery, GetNoShowPolicyByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}

