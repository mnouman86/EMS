using Asp.Versioning;
using CleanArc.Application.Features.PolicyType.Command.CreatePolicyTypeCommand;
using CleanArc.Application.Features.PolicyType.Command.DeletePolicyTypeCommand;
using CleanArc.Application.Features.PolicyType.Command.UpdatePolicyTypeCommand;
using CleanArc.Application.Features.PolicyType.Queries.GetAllPolicyTypes;
using CleanArc.Application.Features.PolicyType.Queries.GetPolicyTypeById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.PolicyType;
/// <summary>
/// PolicyTypeController is responsible for handling HTTP requests related to PolicyType operations
/// such as creating, updating, deleting, and retrieving PolicyTypes. It extends from a base controller
/// which provides common functionality for CRUD operations.
/// 
/// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
/// endpoint definitions for the following actions:
/// 
/// 1. CreatePolicyType: Handles the creation of a new PolicyType.
/// 2. UpdatePolicyType: Handles the updating of an existing PolicyType.
/// 3. DeletePolicyType: Handles the deletion of an existing PolicyType.
/// 4. GetAllPolicyTypes: Retrieves all PolicyTypes.
/// 5. GetPolicyTypeById: Retrieves a specific PolicyType by its ID.
/// 
/// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
/// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
/// 
/// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
/// version, specified as "api/v{version:apiVersion}/PolicyType".
/// 
/// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
/// operation result handling and user ID setting.
/// 
/// Example Usage:
/// 
/// [HttpPost("CreatePolicyType")]
/// public async Task<IActionResult> CreatePolicyType(CreatePolicyTypeCommand model)
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
/// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.PolicyType.Command.CreatePolicyTypeCommand.CreatePolicyTypeCommand, CleanArc.Application.Features.PolicyType.Command.UpdatePolicyTypeCommand.UpdatePolicyTypeCommand, CleanArc.Application.Features.PolicyType.Command.DeletePolicyTypeCommand.DeletePolicyTypeCommand, System.ResponseEntity, CleanArc.Application.Features.PolicyType.Queries.GetAllPolicyTypes.GetAllPolicyTypesQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.PolicyType.Queries.GetAllPolicyTypes.GetAllPolicyTypesQueryResult&gt;, CleanArc.Application.Features.PolicyType.Queries.GetPolicyTypeById.GetRoomViewByIdQuery, CleanArc.Application.Features.PolicyType.Queries.GetPolicyTypeById.GetPolicyTypeByIdQueryResult&gt;" />
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/PolicyType")]
public class PolicyTypeController : _BaseController<CreatePolicyTypeCommand, UpdatePolicyTypeCommand, DeletePolicyTypeCommand, ResponseEntity, GetAllPolicyTypesQuery,
    List<GetAllPolicyTypesQueryResult>, GetPolicyTypeByIdQuery, GetPolicyTypeByIdQueryResult>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PolicyTypeController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public PolicyTypeController(ISender sender, ILogger<_BaseController<CreatePolicyTypeCommand, UpdatePolicyTypeCommand, DeletePolicyTypeCommand, ResponseEntity, GetAllPolicyTypesQuery,
List<GetAllPolicyTypesQueryResult>, GetPolicyTypeByIdQuery, GetPolicyTypeByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}

