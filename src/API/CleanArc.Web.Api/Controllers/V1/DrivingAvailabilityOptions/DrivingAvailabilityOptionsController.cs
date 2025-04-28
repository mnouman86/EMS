using Asp.Versioning;
using CleanArc.Application.Features.DrivingAvailabilityOptions.Command.CreateDrivingAvailabilityOptionsCommand;
using CleanArc.Application.Features.DrivingAvailabilityOptions.Command.DeleteDrivingAvailabilityOptionsCommand;
using CleanArc.Application.Features.DrivingAvailabilityOptions.Command.UpdateDrivingAvailabilityOptionsCommand;
using CleanArc.Application.Features.DrivingAvailabilityOptions.Queries.GetAllDrivingAvailabilityOptionss;
using CleanArc.Application.Features.DrivingAvailabilityOptions.Queries.GetDrivingAvailabilityOptionsById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.DrivingAvailabilityOptions;
/// <summary>
/// DrivingAvailabilityOptionsController is responsible for handling HTTP requests related to DrivingAvailabilityOptions operations
/// such as creating, updating, deleting, and retrieving DrivingAvailabilityOptionss. It extends from a base controller
/// which provides common functionality for CRUD operations.
/// 
/// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
/// endpoint definitions for the following actions:
/// 
/// 1. CreateDrivingAvailabilityOptions: Handles the creation of a new DrivingAvailabilityOptions.
/// 2. UpdateDrivingAvailabilityOptions: Handles the updating of an existing DrivingAvailabilityOptions.
/// 3. DeleteDrivingAvailabilityOptions: Handles the deletion of an existing DrivingAvailabilityOptions.
/// 4. GetAllDrivingAvailabilityOptionss: Retrieves all DrivingAvailabilityOptionss.
/// 5. GetDrivingAvailabilityOptionsById: Retrieves a specific DrivingAvailabilityOptions by its ID.
/// 
/// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
/// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
/// 
/// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
/// version, specified as "api/v{version:apiVersion}/DrivingAvailabilityOptions".
/// 
/// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
/// operation result handling and user ID setting.
/// 
/// The controller is part of the CleanArc architecture, ensuring a clean separation of concerns and adherence to 
/// SOLID principles.
/// </summary>
/// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.DrivingAvailabilityOptions.Command.CreateDrivingAvailabilityOptionsCommand.CreateDrivingAvailabilityOptionsCommand, CleanArc.Application.Features.DrivingAvailabilityOptions.Command.UpdateDrivingAvailabilityOptionsCommand.UpdateDrivingAvailabilityOptionsCommand, CleanArc.Application.Features.DrivingAvailabilityOptions.Command.DeleteDrivingAvailabilityOptionsCommand.DeleteDrivingAvailabilityOptionsCommand, System.ResponseEntity, CleanArc.Application.Features.DrivingAvailabilityOptions.Queries.GetAllDrivingAvailabilityOptionss.GetAllDrivingAvailabilityOptionssQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.DrivingAvailabilityOptions.Queries.GetAllDrivingAvailabilityOptionss.GetAllDrivingAvailabilityOptionssQueryResult&gt;, CleanArc.Application.Features.DrivingAvailabilityOptions.Queries.GetDrivingAvailabilityOptionsById.GetDrivingAvailabilityOptionsByIdQuery, CleanArc.Application.Features.DrivingAvailabilityOptions.Queries.GetDrivingAvailabilityOptionsById.GetDrivingAvailabilityOptionsByIdQueryResult&gt;" />
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/DrivingAvailabilityOptions")]
//[Authorize]
public class DrivingAvailabilityOptionsController : _BaseController<CreateDrivingAvailabilityOptionsCommand, UpdateDrivingAvailabilityOptionsCommand, DeleteDrivingAvailabilityOptionsCommand, ResponseEntity, GetAllDrivingAvailabilityOptionssQuery,
    List<GetAllDrivingAvailabilityOptionssQueryResult>, GetDrivingAvailabilityOptionsByIdQuery, GetDrivingAvailabilityOptionsByIdQueryResult>
{

    /// <summary>
    /// Initializes a new instance of the <see cref="DrivingAvailabilityOptionsController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public DrivingAvailabilityOptionsController(ISender sender, ILogger<_BaseController<CreateDrivingAvailabilityOptionsCommand, UpdateDrivingAvailabilityOptionsCommand, DeleteDrivingAvailabilityOptionsCommand, ResponseEntity, GetAllDrivingAvailabilityOptionssQuery,
List<GetAllDrivingAvailabilityOptionssQueryResult>, GetDrivingAvailabilityOptionsByIdQuery, GetDrivingAvailabilityOptionsByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}
