using Asp.Versioning;
using CleanArc.Application.Features.RatePlan.Command.CreateRatePlanCommand;
using CleanArc.Application.Features.RatePlan.Command.DeleteRatePlanCommand;
using CleanArc.Application.Features.RatePlan.Command.UpdateRatePlanCommand;
using CleanArc.Application.Features.RatePlan.Queries.GetAllRatePlans;
using CleanArc.Application.Features.RatePlan.Queries.GetRatePlanById;
using CleanArc.Domain.Common;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; 
namespace CleanArc.Web.Api.Controllers.V1.RatePlan;
/// <summary>
/// RatePlanController is responsible for handling HTTP requests related to RatePlan operations
/// such as creating, updating, deleting, and retrieving RatePlans. It extends from a base controller
/// which provides common functionality for CRUD operations.
/// 
/// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
/// endpoint definitions for the following actions:
/// 
/// 1. CreateRatePlan: Handles the creation of a new RatePlan.
/// 2. UpdateRatePlan: Handles the updating of an existing RatePlan.
/// 3. DeleteRatePlan: Handles the deletion of an existing RatePlan.
/// 4. GetAllRatePlans: Retrieves all RatePlans.
/// 5. GetRatePlanById: Retrieves a specific RatePlan by its ID.
/// 
/// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
/// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
/// 
/// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
/// version, specified as "api/v{version:apiVersion}/RatePlan".
/// 
/// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
/// operation result handling and user ID setting.
/// 
/// Example Usage:
/// 
/// [HttpPost("CreateRatePlan")]
/// public async Task<IActionResult> CreateRatePlan(CreateRatePlanCommand model)
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
/// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.RatePlan.Command.CreateRatePlanCommand.CreateRatePlanCommand, CleanArc.Application.Features.RatePlan.Command.UpdateRatePlanCommand.UpdateRatePlanCommand, CleanArc.Application.Features.RatePlan.Command.DeleteRatePlanCommand.DeleteRatePlanCommand, System.ResponseEntity, CleanArc.Application.Features.RatePlan.Queries.GetAllRatePlans.GetAllRatePlansQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.RatePlan.Queries.GetAllRatePlans.GetAllRatePlansQueryResult&gt;, CleanArc.Application.Features.RatePlan.Queries.GetRatePlanById.GetRoomViewByIdQuery, CleanArc.Application.Features.RatePlan.Queries.GetRatePlanById.GetRatePlanByIdQueryResult&gt;" />
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/RatePlan")]
public class RatePlanController : _BaseController<CreateRatePlanCommand, UpdateRatePlanCommand, DeleteRatePlanCommand, ResponseEntity, GetAllRatePlanQuery,
    List<GetAllRatePlanQueryResult>, GetRatePlanByIdQuery, GetRatePlanByIdQueryResult>
{
    private readonly ISender _sender;
    /// <summary>
    /// Initializes a new instance of the <see cref="RatePlanController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public RatePlanController(ISender sender, ILogger<_BaseController<CreateRatePlanCommand, UpdateRatePlanCommand, DeleteRatePlanCommand, ResponseEntity, GetAllRatePlanQuery,
List<GetAllRatePlanQueryResult>, GetRatePlanByIdQuery, GetRatePlanByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {
        _sender = sender;
    }
    [HttpPost("GetAccomodationRatePlan")]
    public async Task<IActionResult> GetAccomodationRatePlan([FromBody] GetAllRatePlansQuery query)
    {
        var result = await _sender.Send(query);

        return base.OperationResult(result);
    }
}

