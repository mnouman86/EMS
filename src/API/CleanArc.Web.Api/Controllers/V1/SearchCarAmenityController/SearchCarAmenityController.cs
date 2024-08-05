using Asp.Versioning;
using CleanArc.Application.Features.SearchCarAmenities.Commands.CreateSearchCarAmenitiesCommand;
using CleanArc.Application.Features.SearchCarAmenities.Commands.DeleteSearchCarAmenitiesCommand;
using CleanArc.Application.Features.SearchCarAmenities.Commands.UpdateSearchCarAmenitiesCommand;
using CleanArc.Application.Features.SearchCarAmenities.Queries.GetAllSearchCarAmenities;
using CleanArc.Application.Features.SearchCarAmenities.Queries.GetSearchCarAmenitiesById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.SearchCarAmenity;
/// <summary>
/// SearchCarAmenityController is responsible for handling HTTP requests related to car amenities search operations
/// such as creating, updating, deleting, and retrieving car amenities. It extends from a base controller
/// which provides common functionality for CRUD operations.
/// 
/// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
/// endpoint definitions for the following actions:
/// 
/// 1. CreateSearchCarAmenity: Handles the creation of a new car amenity.
/// 2. UpdateSearchCarAmenity: Handles the updating of an existing car amenity.
/// 3. DeleteSearchCarAmenity: Handles the deletion of an existing car amenity.
/// 4. GetAllSearchCarAmenities: Retrieves all car amenities.
/// 5. GetSearchCarAmenityById: Retrieves a specific car amenity by its ID.
/// 
/// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
/// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
/// 
/// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
/// version, specified as "api/v{version:apiVersion}/SearchCarAmenity".
/// 
/// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
/// operation result handling and user ID setting.
/// 
/// Example Usage:
/// 
/// [HttpPost("CreateSearchCarAmenity")]
/// public async Task<IActionResult> CreateSearchCarAmenity(CreateSearchCarAmenitiesCommand model)
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
/// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.SearchCarAmenities.Commands.CreateSearchCarAmenitiesCommand.CreateSearchCarAmenitiesCommand, CleanArc.Application.Features.SearchCarAmenities.Commands.UpdateSearchCarAmenitiesCommand.UpdateSearchCarAmenitiesCommand, CleanArc.Application.Features.SearchCarAmenities.Commands.DeleteSearchCarAmenitiesCommand.DeleteSearchCarAmenitiesCommand, System.Boolean, CleanArc.Application.Features.SearchCarAmenities.Queries.GetAllSearchCarAmenities.GetAllSearchCarAmenitiesQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.SearchCarAmenities.Queries.GetAllSearchCarAmenities.GetAllSearchCarAmenitiesQueryResult&gt;, CleanArc.Application.Features.SearchCarAmenities.Queries.GetSearchCarAmenitiesById.GetSearchCarAmenitiesByIdQuery, CleanArc.Application.Features.SearchCarAmenities.Queries.GetSearchCarAmenitiesById.GetSearchCarAmenitiesByIdQueryResult&gt;" />
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/SearchCarAmenity")]
//[Authorize]
public class SearchCarAmenityController : _BaseController<CreateSearchCarAmenitiesCommand, UpdateSearchCarAmenitiesCommand, DeleteSearchCarAmenitiesCommand, bool, GetAllSearchCarAmenitiesQuery,
List<GetAllSearchCarAmenitiesQueryResult>, GetSearchCarAmenitiesByIdQuery, GetSearchCarAmenitiesByIdQueryResult>
{

    /// <summary>
    /// Initializes a new instance of the <see cref="SearchCarAmenityController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public SearchCarAmenityController(ISender sender, ILogger<_BaseController<CreateSearchCarAmenitiesCommand, UpdateSearchCarAmenitiesCommand, DeleteSearchCarAmenitiesCommand, bool, GetAllSearchCarAmenitiesQuery,
List<GetAllSearchCarAmenitiesQueryResult>, GetSearchCarAmenitiesByIdQuery, GetSearchCarAmenitiesByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}

