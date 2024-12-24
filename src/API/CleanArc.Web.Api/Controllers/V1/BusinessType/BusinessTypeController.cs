using Asp.Versioning;
using CleanArc.Application.Features.BusinessType.Command.CreateBusinessTypeCommand;
using CleanArc.Application.Features.BusinessType.Command.DeleteBusinessTypeCommand;
using CleanArc.Application.Features.BusinessType.Command.UpdateBusinessTypeCommand;
using CleanArc.Application.Features.BusinessType.Queries.GetAllBusinessType;
using CleanArc.Application.Features.BusinessType.Queries.GetBusinessTypeById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.BusinessType;

/// <summary>
/// BusinessTypeController is responsible for handling HTTP requests related to BusinessType operations
/// such as creating, updating, deleting, and retrieving BusinessType information. It extends from a 
/// base controller which provides common functionality for CRUD operations.
/// 
/// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
/// endpoint definitions for the following actions:
/// 
/// 1. CreateBusinessType: Handles the creation of a new BusinessType.
/// 2. UpdateBusinessType: Handles the updating of an existing BusinessType.
/// 3. DeleteBusinessType: Handles the deletion of an existing BusinessType.
/// 4. GetAllBusinessType: Retrieves all BusinessTypees.
/// 5. GetBusinessTypeById: Retrieves a specific BusinessType by its ID.
/// 
/// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
/// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
/// 
/// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
/// version, specified as "api/v{version:apiVersion}/BusinessType".
/// 
/// Note: The actual endpoint methods are intended to be implemented to utilize the base controller's 
/// operation result handling and user ID setting.
/// 
/// Example Usage:
/// 
/// [HttpPost("CreateBusinessType")]
/// public async Task<IActionResult> CreateBusinessType(CreateBusinessTypeCommand model)
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
/// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.BusinessType.Command.CreateBusinessTypeCommand.CreateBusinessTypeCommand, CleanArc.Application.Features.BusinessType.Command.UpdateBusinessTypeCommand.UpdateBusinessTypeCommand, CleanArc.Application.Features.BusinessType.Command.DeleteBusinessTypeCommand.DeleteBusinessTypeCommand, System.ResponseEntity, CleanArc.Application.Features.BusinessType.Queries.GetAllBusinessType.GetAllBusinessTypeQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.BusinessType.Queries.GetAllBusinessType.GetAllBusinessTypeQueryResult&gt;, CleanArc.Application.Features.BusinessType.Queries.GetBusinessTypeById.GetBusinessTypeByIdQuery, CleanArc.Application.Features.BusinessType.Queries.GetBusinessTypeById.GetBusinessTypeByIdQueryResult&gt;" />
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/BusinessType")]
public class BusinessTypeController : _BaseController<CreateBusinessTypeCommand, UpdateBusinessTypeCommand, DeleteBusinessTypeCommand, ResponseEntity, GetAllBusinessTypeQuery,
List<GetAllBusinessTypeQueryResult>, GetBusinessTypeByIdQuery, GetBusinessTypeByIdQueryResult>
{

    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessTypeController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public BusinessTypeController(ISender sender, ILogger<_BaseController<CreateBusinessTypeCommand, UpdateBusinessTypeCommand, DeleteBusinessTypeCommand, ResponseEntity, GetAllBusinessTypeQuery,
List<GetAllBusinessTypeQueryResult>, GetBusinessTypeByIdQuery, GetBusinessTypeByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}


