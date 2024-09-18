using Asp.Versioning;
using CleanArc.Application.Features.RoomImage.Command.CreateRoomImageCommand;
using CleanArc.Application.Features.RoomImage.Command.DeleteRoomImageCommand;
using CleanArc.Application.Features.RoomImage.Command.UpdateRoomImageCommand;
using CleanArc.Application.Features.RoomImage.Queries.GetAllRoomImagesQuery;
using CleanArc.Application.Features.RoomImage.Queries.GetRoomImagesByIdQuery;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.RoomImages;
/// <summary>
/// RoomImagesController is responsible for handling HTTP requests related to room image operations
/// such as creating, updating, deleting, and retrieving room images. It extends from a base controller
/// which provides common functionality for CRUD operations.
/// 
/// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
/// endpoint definitions for the following actions:
/// 
/// 1. CreateRoomImage: Handles the creation of a new room image.
/// 2. UpdateRoomImage: Handles the updating of an existing room image.
/// 3. DeleteRoomImage: Handles the deletion of an existing room image.
/// 4. GetAllRoomImages: Retrieves all room images.
/// 5. GetRoomImageById: Retrieves a specific room image by its ID.
/// 
/// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
/// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
/// 
/// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
/// version, specified as "api/v{version:apiVersion}/RoomImages".
/// 
/// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
/// operation result handling and user ID setting.
/// 
/// Example Usage:
/// 
/// [HttpPost("CreateRoomImage")]
/// public async Task<IActionResult> CreateRoomImage(CreateRoomImageCommand model)
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
/// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.RoomImage.Command.CreateRoomImageCommand.CreateRoomImageCommand, CleanArc.Application.Features.RoomImage.Command.UpdateRoomImageCommand.UpdateRoomImageCommand, CleanArc.Application.Features.RoomImage.Command.DeleteRoomImageCommand.DeleteRoomImageCommand, System.ResponseEntity, CleanArc.Application.Features.RoomImage.Queries.GetAllRoomImagesQuery.GetAllRoomImagesQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.RoomImage.Queries.GetAllRoomImagesQuery.GetAllRoomImagesQueryResult&gt;, CleanArc.Application.Features.RoomImage.Queries.GetRoomImagesByIdQuery.GetRoomImagesByIdQuery, CleanArc.Application.Features.RoomImage.Queries.GetRoomImagesByIdQuery.GetRoomImagesByIdQueryResult&gt;" />
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/RoomImages")]
public class RoomImagesController : _BaseController<CreateRoomImageCommand, UpdateRoomImageCommand, DeleteRoomImageCommand, ResponseEntity, GetAllRoomImagesQuery,
    List<GetAllRoomImagesQueryResult>, GetRoomImagesByIdQuery, GetRoomImagesByIdQueryResult>
{

    /// <summary>
    /// Initializes a new instance of the <see cref="RoomImagesController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public RoomImagesController(ISender sender, ILogger<_BaseController<CreateRoomImageCommand, UpdateRoomImageCommand, DeleteRoomImageCommand, ResponseEntity, GetAllRoomImagesQuery,
List<GetAllRoomImagesQueryResult>, GetRoomImagesByIdQuery, GetRoomImagesByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}