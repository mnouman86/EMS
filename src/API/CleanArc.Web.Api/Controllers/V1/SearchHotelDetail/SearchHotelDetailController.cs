using Asp.Versioning;
using CleanArc.Application.Features.SearchHotel.Commands.CreateSearchHotelCommand;
using CleanArc.Application.Features.SearchHotel.Commands.DeleteSearchHotelCommand;
using CleanArc.Application.Features.SearchHotel.Commands.UpdateSearchHotelCommand;
using CleanArc.Application.Features.SearchHotel.Queries.GetAllSearchHotels;
using CleanArc.Application.Features.SearchHotel.Queries.GetSearchHotelById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.SearchHotelDetail;

/// <summary>
/// SearchHotelDetailController is responsible for handling HTTP requests related to hotel search operations
/// such as creating, updating, deleting, and retrieving hotel details. It extends from a base controller
/// which provides common functionality for CRUD operations.
/// 
/// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
/// endpoint definitions for the following actions:
/// 
/// 1. CreateSearchHotel: Handles the creation of a new hotel.
/// 2. UpdateSearchHotel: Handles the updating of an existing hotel.
/// 3. DeleteSearchHotel: Handles the deletion of an existing hotel.
/// 4. GetAllSearchHotels: Retrieves all hotels.
/// 5. GetSearchHotelById: Retrieves a specific hotel by its ID.
/// 
/// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
/// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
/// 
/// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
/// version, specified as "api/v{version:apiVersion}/SearchHotelDetail".
/// 
/// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
/// operation result handling and user ID setting.
/// 
/// Example Usage:
/// 
/// [HttpPost("CreateSearchHotel")]
/// public async Task<IActionResult> CreateSearchHotel(CreateSearchHotelCommand model)
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
/// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.SearchHotel.Commands.CreateSearchHotelCommand.CreateSearchHotelCommand, CleanArc.Application.Features.SearchHotel.Commands.UpdateSearchHotelCommand.UpdateSearchHotelCommand, CleanArc.Application.Features.SearchHotel.Commands.DeleteSearchHotelCommand.DeleteSearchHotelCommand, System.ResponseEntity, CleanArc.Application.Features.SearchHotel.Queries.GetAllSearchHotels.GetAllSearchHotelsQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.SearchHotel.Queries.GetAllSearchHotels.GetAllSearchHotelsQueryResult&gt;, CleanArc.Application.Features.SearchHotel.Queries.GetSearchHotelById.GetSearchHotelByIdQuery, CleanArc.Application.Features.SearchHotel.Queries.GetSearchHotelById.GetSearchHotelByIdQueryResult&gt;" />
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/SearchHotelDetail")]
//[Authorize]
public class SearchHotelDetailController : _BaseController<CreateSearchHotelCommand, UpdateSearchHotelCommand, DeleteSearchHotelCommand, ResponseEntity, GetAllSearchHotelsQuery,
List<GetAllSearchHotelsQueryResult>, GetSearchHotelByIdQuery, GetSearchHotelByIdQueryResult>
{

    /// <summary>
    /// Initializes a new instance of the <see cref="SearchHotelDetailController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public SearchHotelDetailController(ISender sender, ILogger<_BaseController<CreateSearchHotelCommand, UpdateSearchHotelCommand, DeleteSearchHotelCommand, ResponseEntity, GetAllSearchHotelsQuery,
List<GetAllSearchHotelsQueryResult>, GetSearchHotelByIdQuery, GetSearchHotelByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }
}
