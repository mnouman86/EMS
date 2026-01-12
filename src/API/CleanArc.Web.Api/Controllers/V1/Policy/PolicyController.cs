using Asp.Versioning;
using CleanArc.Application.Features.Policy.Command.CreatePolicyCommand;
using CleanArc.Application.Features.Policy.Command.DeletePolicyCommand;
using CleanArc.Application.Features.Policy.Command.UpdatePolicyCommand;
using CleanArc.Application.Features.Policy.Queries.GetAllPolicys;
using CleanArc.Application.Features.Policy.Queries.GetPolicyById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.Policy;
/// <summary>
/// PolicyController is responsible for handling HTTP requests related to Policy operations
/// such as creating, updating, deleting, and retrieving Policys. It extends from a base controller
/// which provides common functionality for CRUD operations.
/// 
/// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
/// endpoint definitions for the following actions:
/// 
/// 1. CreatePolicy: Handles the creation of a new Policy.
/// 2. UpdatePolicy: Handles the updating of an existing Policy.
/// 3. DeletePolicy: Handles the deletion of an existing Policy.
/// 4. GetAllPolicys: Retrieves all Policys.
/// 5. GetPolicyById: Retrieves a specific Policy by its ID.
/// 
/// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
/// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
/// 
/// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
/// version, specified as "api/v{version:apiVersion}/Policy".
/// 
/// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
/// operation result handling and user ID setting.
/// 
/// Example Usage:
/// 
/// [HttpPost("CreatePolicy")]
/// public async Task<IActionResult> CreatePolicy(CreatePolicyCommand model)
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
/// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.Policy.Command.CreatePolicyCommand.CreatePolicyCommand, CleanArc.Application.Features.Policy.Command.UpdatePolicyCommand.UpdatePolicyCommand, CleanArc.Application.Features.Policy.Command.DeletePolicyCommand.DeletePolicyCommand, System.ResponseEntity, CleanArc.Application.Features.Policy.Queries.GetAllPolicys.GetAllPolicysQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.Policy.Queries.GetAllPolicys.GetAllPolicysQueryResult&gt;, CleanArc.Application.Features.Policy.Queries.GetPolicyById.GetRoomViewByIdQuery, CleanArc.Application.Features.Policy.Queries.GetPolicyById.GetPolicyByIdQueryResult&gt;" />
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/Policy")]
public class PolicyController : _BaseController<CreatePolicyCommand, UpdatePolicyCommand, DeletePolicyCommand, ResponseEntity, GetAllPolicysQuery,
    List<GetAllPolicysQueryResult>, GetPolicyByIdQuery, GetPolicyByIdQueryResult>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PolicyController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public PolicyController(ISender sender, ILogger<_BaseController<CreatePolicyCommand, UpdatePolicyCommand, DeletePolicyCommand, ResponseEntity, GetAllPolicysQuery,
List<GetAllPolicysQueryResult>, GetPolicyByIdQuery, GetPolicyByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}

