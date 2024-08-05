using Asp.Versioning;
using CleanArc.Application.Features.SearchHotelAmenities.Commands.CreateSearchHotelAmenitiesCommand;
using CleanArc.Application.Features.SearchHotelAmenities.Commands.DeleteSearchHotelAmenitiesCommand;
using CleanArc.Application.Features.SearchHotelAmenities.Commands.UpdateSearchHotelAmenitiesCommand;
using CleanArc.Application.Features.SearchHotelAmenities.Queries.GetAllSearchHotelAmenities;
using CleanArc.Application.Features.SearchHotelAmenities.Queries.GetSearchHotelAmenitiesById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.SearchHotelAmenity;
/// <summary>
/// SearchHotelAmenityController is responsible for handling HTTP requests related to hotel amenities search operations
/// such as creating, updating, deleting, and retrieving hotel amenities. It extends from a base controller
/// which provides common functionality for CRUD operations.
/// 
/// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
/// endpoint definitions for the following actions:
/// 
/// 1. CreateSearchHotelAmenity: Handles the creation of a new hotel amenity.
/// 2. UpdateSearchHotelAmenity: Handles the updating of an existing hotel amenity.
/// 3. DeleteSearchHotelAmenity: Handles the deletion of an existing hotel amenity.
/// 4. GetAllSearchHotelAmenities: Retrieves all hotel amenities.
/// 5. GetSearchHotelAmenityById: Retrieves a specific hotel amenity by its ID.
/// 
/// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
/// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
/// 
/// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
/// version, specified as "api/v{version:apiVersion}/SearchHotelAmenity".
/// 
/// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
/// operation result handling and user ID setting.
/// 
/// Example Usage:
/// 
/// [HttpPost("CreateSearchHotelAmenity")]
/// public async Task<IActionResult> CreateSearchHotelAmenity(CreateSearchHotelAmenitiesCommand model)
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
/// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.SearchHotelAmenities.Commands.CreateSearchHotelAmenitiesCommand.CreateSearchHotelAmenitiesCommand, CleanArc.Application.Features.SearchHotelAmenities.Commands.UpdateSearchHotelAmenitiesCommand.UpdateSearchHotelAmenitiesCommand, CleanArc.Application.Features.SearchHotelAmenities.Commands.DeleteSearchHotelAmenitiesCommand.DeleteSearchHotelAmenitiesCommand, System.Boolean, CleanArc.Application.Features.SearchHotelAmenities.Queries.GetAllSearchHotelAmenities.GetAllSearchHotelAmenitiesQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.SearchHotelAmenities.Queries.GetAllSearchHotelAmenities.GetAllSearchHotelAmenitiesQueryResult&gt;, CleanArc.Application.Features.SearchHotelAmenities.Queries.GetSearchHotelAmenitiesById.GetSearchHotelAmenitiesByIdQuery, CleanArc.Application.Features.SearchHotelAmenities.Queries.GetSearchHotelAmenitiesById.GetSearchHotelAmenitiesByIdQueryResult&gt;" />
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/SearchHotelAmenity")]
//[Authorize]
public class SearchHotelAmenityController : _BaseController<CreateSearchHotelAmenitiesCommand, UpdateSearchHotelAmenitiesCommand, DeleteSearchHotelAmenitiesCommand, bool, GetAllSearchHotelAmenitiesQuery,
List<GetAllSearchHotelAmenitiesQueryResult>, GetSearchHotelAmenitiesByIdQuery, GetSearchHotelAmenitiesByIdQueryResult>
{

    /// <summary>
    /// Initializes a new instance of the <see cref="SearchHotelAmenityController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public SearchHotelAmenityController(ISender sender, ILogger<_BaseController<CreateSearchHotelAmenitiesCommand, UpdateSearchHotelAmenitiesCommand, DeleteSearchHotelAmenitiesCommand, bool, GetAllSearchHotelAmenitiesQuery,
List<GetAllSearchHotelAmenitiesQueryResult>, GetSearchHotelAmenitiesByIdQuery, GetSearchHotelAmenitiesByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}

