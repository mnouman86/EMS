using Asp.Versioning;
using CleanArc.Application.Features.SearchHotelImage.Command.CreateSearchHotelImageCommand;
using CleanArc.Application.Features.SearchHotelImage.Command.DeleteSearchHotelImageCommand;
using CleanArc.Application.Features.SearchHotelImage.Command.UpdateSearchHotelImageCommand;
using CleanArc.Application.Features.SearchHotelImage.Queries.GetAllSearchHotelImage;
using CleanArc.Application.Features.SearchHotelImage.Queries.GetByIdSearchHotelImage;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.SearchHotelImage;
/// <summary>
/// SearchHotelImageController is responsible for handling HTTP requests related to hotel image search operations
/// such as creating, updating, deleting, and retrieving hotel images. It extends from a base controller
/// which provides common functionality for CRUD operations.
/// 
/// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
/// endpoint definitions for the following actions:
/// 
/// 1. CreateSearchHotelImage: Handles the creation of a new hotel image.
/// 2. UpdateSearchHotelImage: Handles the updating of an existing hotel image.
/// 3. DeleteSearchHotelImage: Handles the deletion of an existing hotel image.
/// 4. GetAllSearchHotelImages: Retrieves all hotel images.
/// 5. GetSearchHotelImageById: Retrieves a specific hotel image by its ID.
/// 
/// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
/// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
/// 
/// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
/// version, specified as "api/v{version:apiVersion}/SearchHotelImage".
/// 
/// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
/// operation result handling and user ID setting.
/// 
/// Example Usage:
/// 
/// [HttpPost("CreateSearchHotelImage")]
/// public async Task<IActionResult> CreateSearchHotelImage(CreateSearchHotelImageCommand model)
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
/// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.SearchHotelImage.Command.CreateSearchHotelImageCommand.CreateSearchCarImageCommand, CleanArc.Application.Features.SearchHotelImage.Command.UpdateSearchHotelImageCommand.UpdateSearchHotelImageCommand, CleanArc.Application.Features.SearchHotelImage.Command.DeleteSearchHotelImageCommand.DeleteSearchHotelImageCommand, System.ResponseEntity, CleanArc.Application.Features.SearchHotelImage.Queries.GetAllSearchHotelImage.GetAllSearchHotelImageQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.SearchHotelImage.Queries.GetAllSearchHotelImage.GetAllSearchHotelImageQueryResult&gt;, CleanArc.Application.Features.SearchHotelImage.Queries.GetByIdSearchHotelImage.GetByIdSearchHotelImageQuery, CleanArc.Application.Features.SearchHotelImage.Queries.GetByIdSearchHotelImage.GetByIdSearchHotelImageQueryResult&gt;" />
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/SearchHotelImage")]
//[Authorize]
public class SearchHotelImageController : _BaseController<CreateSearchCarImageCommand, UpdateSearchHotelImageCommand, DeleteSearchHotelImageCommand, ResponseEntity, GetAllSearchHotelImageQuery,
List<GetAllSearchHotelImageQueryResult>, GetByIdSearchHotelImageQuery, GetByIdSearchHotelImageQueryResult>
{

    /// <summary>
    /// Initializes a new instance of the <see cref="SearchHotelImageController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public SearchHotelImageController(ISender sender, ILogger<_BaseController<CreateSearchCarImageCommand, UpdateSearchHotelImageCommand, DeleteSearchHotelImageCommand, ResponseEntity, GetAllSearchHotelImageQuery,
List<GetAllSearchHotelImageQueryResult>, GetByIdSearchHotelImageQuery, GetByIdSearchHotelImageQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}
