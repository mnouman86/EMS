using Asp.Versioning;
using CleanArc.Application.Features.MappingRoomAmenity.Command.CreateMappingRoomAmenityCommand;
using CleanArc.Application.Features.MappingRoomAmenity.Command.DeleteMappingRoomAmenityCommand;
using CleanArc.Application.Features.MappingRoomAmenity.Command.UpdateMappingRoomAmenityCommand;
using CleanArc.Application.Features.MappingRoomAmenity.Query.GetAllMappingRoomAmenity;
using CleanArc.Application.Features.MappingRoomAmenity.Query.GetMappingRoomAmenityById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.MappingRoomAmenities;

/// <summary>
/// MappingRoomAmenitiesController is responsible for managing HTTP requests related to room-amenity mapping operations
/// such as creating, updating, deleting, and retrieving mappings between rooms and amenities. It extends from a base controller
/// which provides common functionality for CRUD operations.
/// 
/// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
/// endpoint definitions for the following actions:
/// 
/// 1. CreateMappingRoomAmenityCommand: Handles the creation of new mappings between rooms and amenities.
/// 2. UpdateMappingRoomAmenityCommand: Handles the updating of existing mappings between rooms and amenities.
/// 3. DeleteMappingRoomAmenityCommand: Handles the deletion of mappings between rooms and amenities.
/// 4. GetAllMappingRoomAmenityQuery: Retrieves a list of all mappings between rooms and amenities.
/// 5. GetMappingRoomAmenityByIdQuery: Retrieves a specific mapping between a room and amenity by its ID.
/// 
/// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
/// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
/// 
/// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
/// version, specified as "api/v{version:apiVersion}/MappingRoomAmenities".
/// 
/// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
/// operation result handling and user ID setting.
/// 
/// The controller adheres to Clean Architecture principles, ensuring a clean separation of concerns and adherence to 
/// SOLID principles.
/// </summary>
/// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.MappingRoomAmenity.Command.CreateMappingRoomAmenityCommand.CreateMappingRoomAmenityCommand, CleanArc.Application.Features.MappingRoomAmenity.Command.UpdateMappingRoomAmenityCommand.UpdateMappingRoomAmenityCommand, CleanArc.Application.Features.MappingRoomAmenity.Command.DeleteMappingRoomAmenityCommand.DeleteMappingRoomAmenityCommand, System.Boolean, CleanArc.Application.Features.MappingRoomAmenity.Query.GetAllMappingRoomAmenity.GetAllMappingRoomAmenityQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.MappingRoomAmenity.Query.GetAllMappingRoomAmenity.GetAllMappingRoomAmenityQueryResult&gt;, CleanArc.Application.Features.MappingRoomAmenity.Query.GetMappingRoomAmenityById.GetMappingRoomAmenityByIdQuery, CleanArc.Application.Features.MappingRoomAmenity.Query.GetMappingRoomAmenityById.GetMappingRoomAmenityByIdQueryResult&gt;" />
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/MappingRoomAmenities")]
//[Authorize]
public class MappingRoomAmenitiesController : _BaseController<CreateMappingRoomAmenityCommand, UpdateMappingRoomAmenityCommand, DeleteMappingRoomAmenityCommand, bool, GetAllMappingRoomAmenityQuery,
List<GetAllMappingRoomAmenityQueryResult>, GetMappingRoomAmenityByIdQuery, GetMappingRoomAmenityByIdQueryResult>
{

    /// <summary>
    /// Initializes a new instance of the <see cref="MappingRoomAmenitiesController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public MappingRoomAmenitiesController(ISender sender, ILogger<_BaseController<CreateMappingRoomAmenityCommand, UpdateMappingRoomAmenityCommand, DeleteMappingRoomAmenityCommand, bool, GetAllMappingRoomAmenityQuery,
List<GetAllMappingRoomAmenityQueryResult>, GetMappingRoomAmenityByIdQuery, GetMappingRoomAmenityByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}


