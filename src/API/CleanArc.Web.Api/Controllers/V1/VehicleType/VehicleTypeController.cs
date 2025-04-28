using Asp.Versioning;
using CleanArc.Application.Features.VehicleType.Command.CreateVehicleTypeCommand;
using CleanArc.Application.Features.VehicleType.Command.DeleteVehicleTypeCommand;
using CleanArc.Application.Features.VehicleType.Command.UpdateVehicleTypeCommand;
using CleanArc.Application.Features.VehicleType.Queries.GetAllVehicleTypes;
using CleanArc.Application.Features.VehicleType.Queries.GetVehicleTypeById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.VehicleType;
/// <summary>
/// VehicleTypeController is responsible for handling HTTP requests related to VehicleType operations
/// such as creating, updating, deleting, and retrieving VehicleTypes. It extends from a base controller
/// which provides common functionality for CRUD operations.
/// 
/// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
/// endpoint definitions for the following actions:
/// 
/// 1. CreateVehicleType: Handles the creation of a new VehicleType.
/// 2. UpdateVehicleType: Handles the updating of an existing VehicleType.
/// 3. DeleteVehicleType: Handles the deletion of an existing VehicleType.
/// 4. GetAllVehicleTypes: Retrieves all VehicleTypes.
/// 5. GetVehicleTypeById: Retrieves a specific VehicleType by its ID.
/// 
/// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
/// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
/// 
/// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
/// version, specified as "api/v{version:apiVersion}/VehicleType".
/// 
/// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
/// operation result handling and user ID setting.
/// 
/// The controller is part of the CleanArc architecture, ensuring a clean separation of concerns and adherence to 
/// SOLID principles.
/// </summary>
/// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.VehicleType.Command.CreateVehicleTypeCommand.CreateVehicleTypeCommand, CleanArc.Application.Features.VehicleType.Command.UpdateVehicleTypeCommand.UpdateVehicleTypeCommand, CleanArc.Application.Features.VehicleType.Command.DeleteVehicleTypeCommand.DeleteVehicleTypeCommand, System.ResponseEntity, CleanArc.Application.Features.VehicleType.Queries.GetAllVehicleTypes.GetAllVehicleTypesQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.VehicleType.Queries.GetAllVehicleTypes.GetAllVehicleTypesQueryResult&gt;, CleanArc.Application.Features.VehicleType.Queries.GetVehicleTypeById.GetVehicleTypeByIdQuery, CleanArc.Application.Features.VehicleType.Queries.GetVehicleTypeById.GetVehicleTypeByIdQueryResult&gt;" />
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/VehicleType")]
//[Authorize]
public class VehicleTypeController : _BaseController<CreateVehicleTypeCommand, UpdateVehicleTypeCommand, DeleteVehicleTypeCommand, ResponseEntity, GetAllVehicleTypesQuery,
    List<GetAllVehicleTypesQueryResult>, GetVehicleTypeByIdQuery, GetVehicleTypeByIdQueryResult>
{

    /// <summary>
    /// Initializes a new instance of the <see cref="VehicleTypeController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public VehicleTypeController(ISender sender, ILogger<_BaseController<CreateVehicleTypeCommand, UpdateVehicleTypeCommand, DeleteVehicleTypeCommand, ResponseEntity, GetAllVehicleTypesQuery,
List<GetAllVehicleTypesQueryResult>, GetVehicleTypeByIdQuery, GetVehicleTypeByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}
