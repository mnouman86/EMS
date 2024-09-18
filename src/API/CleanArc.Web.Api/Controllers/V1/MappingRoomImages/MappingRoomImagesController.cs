using Asp.Versioning;
using CleanArc.Application.Features.MappingRoomImage.Command.CreateMappingRoomImageCommand;
using CleanArc.Application.Features.MappingRoomImage.Command.DeleteMappingRoomImageCommand;
using CleanArc.Application.Features.MappingRoomImage.Command.UpdateMappingRoomImageCommand;
using CleanArc.Application.Features.MappingRoomImage.Query.GetAllMappingRoomImage;
using CleanArc.Application.Features.MappingRoomImage.Query.GetMappingRoomImageById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.MappingRoomImages;

/// <summary>
/// MappingRoomImagesController is responsible for managing HTTP requests related to room-image mapping operations
/// such as creating, updating, deleting, and retrieving mappings between rooms and images. It extends from a base controller
/// which provides common functionality for CRUD operations.
/// 
/// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
/// endpoint definitions for the following actions:
/// 
/// 1. CreateMappingRoomImageCommand: Handles the creation of new mappings between rooms and images.
/// 2. UpdateMappingRoomImageCommand: Handles the updating of existing mappings between rooms and images.
/// 3. DeleteMappingRoomImageCommand: Handles the deletion of mappings between rooms and images.
/// 4. GetAllMappingRoomImageQuery: Retrieves a list of all mappings between rooms and images.
/// 5. GetMappingRoomImageByIdQuery: Retrieves a specific mapping between a room and image by its ID.
/// 
/// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
/// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
/// 
/// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
/// version, specified as "api/v{version:apiVersion}/MappingRoomImages".
/// 
/// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
/// operation result handling and user ID setting.
/// 
/// The controller adheres to Clean Architecture principles, ensuring a clean separation of concerns and adherence to 
/// SOLID principles.
/// </summary>
/// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.MappingRoomImage.Command.CreateMappingRoomImageCommand.CreateMappingRoomImageCommand, CleanArc.Application.Features.MappingRoomImage.Command.UpdateMappingRoomImageCommand.UpdateMappingRoomImageCommand, CleanArc.Application.Features.MappingRoomImage.Command.DeleteMappingRoomImageCommand.DeleteMappingRoomImageCommand, System.ResponseEntity, CleanArc.Application.Features.MappingRoomImage.Query.GetAllMappingRoomImage.GetAllMappingRoomImageQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.MappingRoomImage.Query.GetAllMappingRoomImage.GetAllMappingRoomImageQueryResult&gt;, CleanArc.Application.Features.MappingRoomImage.Query.GetMappingRoomImageById.GetMappingRoomImageByIdQuery, CleanArc.Application.Features.MappingRoomImage.Query.GetMappingRoomImageById.GetMappingRoomImageByIdQueryResult&gt;" />
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/MappingRoomImages")]
public class MappingRoomImagesController : _BaseController<CreateMappingRoomImageCommand, UpdateMappingRoomImageCommand, DeleteMappingRoomImageCommand, ResponseEntity, GetAllMappingRoomImageQuery,
List<GetAllMappingRoomImageQueryResult>, GetMappingRoomImageByIdQuery, GetMappingRoomImageByIdQueryResult>
{

    /// <summary>
    /// Initializes a new instance of the <see cref="MappingRoomImagesController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public MappingRoomImagesController(ISender sender, ILogger<_BaseController<CreateMappingRoomImageCommand, UpdateMappingRoomImageCommand, DeleteMappingRoomImageCommand, ResponseEntity, GetAllMappingRoomImageQuery,
List<GetAllMappingRoomImageQueryResult>, GetMappingRoomImageByIdQuery, GetMappingRoomImageByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}

