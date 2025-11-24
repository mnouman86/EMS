using Asp.Versioning;
using CleanArc.Application.Features.RatePlanType.Command.CreateRatePlanTypeCommand;
using CleanArc.Application.Features.RatePlanType.Command.DeleteRatePlanTypeCommand;
using CleanArc.Application.Features.RatePlanType.Command.UpdateRatePlanTypeCommand;
using CleanArc.Application.Features.RatePlanType.Queries.GetAllRatePlanTypes;
using CleanArc.Application.Features.RatePlanType.Queries.GetRatePlanTypeById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.RatePlanType;
/// <summary>
/// RatePlanTypeController is responsible for handling HTTP requests related to RatePlanType operations
/// such as creating, updating, deleting, and retrieving RatePlanTypes. It extends from a base controller
/// which provides common functionality for CRUD operations.
/// 
/// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
/// endpoint definitions for the following actions:
/// 
/// 1. CreateRatePlanType: Handles the creation of a new RatePlanType.
/// 2. UpdateRatePlanType: Handles the updating of an existing RatePlanType.
/// 3. DeleteRatePlanType: Handles the deletion of an existing RatePlanType.
/// 4. GetAllRatePlanTypes: Retrieves all RatePlanTypes.
/// 5. GetRatePlanTypeById: Retrieves a specific RatePlanType by its ID.
/// 
/// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
/// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
/// 
/// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
/// version, specified as "api/v{version:apiVersion}/RatePlanType".
/// 
/// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
/// operation result handling and user ID setting.
/// 
/// Example Usage:
/// 
/// [HttpPost("CreateRatePlanType")]
/// public async Task<IActionResult> CreateRatePlanType(CreateRatePlanTypeCommand model)
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
/// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.RatePlanType.Command.CreateRatePlanTypeCommand.CreateRatePlanTypeCommand, CleanArc.Application.Features.RatePlanType.Command.UpdateRatePlanTypeCommand.UpdateRatePlanTypeCommand, CleanArc.Application.Features.RatePlanType.Command.DeleteRatePlanTypeCommand.DeleteRatePlanTypeCommand, System.ResponseEntity, CleanArc.Application.Features.RatePlanType.Queries.GetAllRatePlanTypes.GetAllRatePlanTypesQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.RatePlanType.Queries.GetAllRatePlanTypes.GetAllRatePlanTypesQueryResult&gt;, CleanArc.Application.Features.RatePlanType.Queries.GetRatePlanTypeById.GetRoomViewByIdQuery, CleanArc.Application.Features.RatePlanType.Queries.GetRatePlanTypeById.GetRatePlanTypeByIdQueryResult&gt;" />
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/RatePlanType")]
public class RatePlanTypeController : _BaseController<CreateRatePlanTypeCommand, UpdateRatePlanTypeCommand, DeleteRatePlanTypeCommand, ResponseEntity, GetAllRatePlanTypesQuery,
    List<GetAllRatePlanTypesQueryResult>, GetRatePlanTypeByIdQuery, GetRatePlanTypeByIdQueryResult>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RatePlanTypeController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public RatePlanTypeController(ISender sender, ILogger<_BaseController<CreateRatePlanTypeCommand, UpdateRatePlanTypeCommand, DeleteRatePlanTypeCommand, ResponseEntity, GetAllRatePlanTypesQuery,
List<GetAllRatePlanTypesQueryResult>, GetRatePlanTypeByIdQuery, GetRatePlanTypeByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}

