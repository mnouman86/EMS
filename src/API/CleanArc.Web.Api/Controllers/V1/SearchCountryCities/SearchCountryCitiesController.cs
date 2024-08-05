using Asp.Versioning;
using CleanArc.Application.Features.SearchCountryCities.Command.CreateSearchCountryCities;
using CleanArc.Application.Features.SearchCountryCities.Command.DeleteSearchCountryCities;
using CleanArc.Application.Features.SearchCountryCities.Command.UpdateSearchCountryCities;
using CleanArc.Application.Features.SearchCountryCities.Queries.GetAllSearchCountryCities;
using CleanArc.Application.Features.SearchCountryCities.Queries.GetSearchCountryCitiesById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.SearchCountryCities;

/// <summary>
/// SearchCountryCitiesController is responsible for handling HTTP requests related to country and city search operations
/// such as creating, updating, deleting, and retrieving country-city relationships. It extends from a base controller
/// which provides common functionality for CRUD operations.
/// 
/// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
/// endpoint definitions for the following actions:
/// 
/// 1. CreateSearchCountryCity: Handles the creation of a new country-city relationship.
/// 2. UpdateSearchCountryCity: Handles the updating of an existing country-city relationship.
/// 3. DeleteSearchCountryCity: Handles the deletion of an existing country-city relationship.
/// 4. GetAllSearchCountryCities: Retrieves all country-city relationships.
/// 5. GetSearchCountryCityById: Retrieves a specific country-city relationship by its ID.
/// 
/// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
/// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
/// 
/// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
/// version, specified as "api/v{version:apiVersion}/SearchCountryCities".
/// 
/// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
/// operation result handling and user ID setting.
/// 
/// Example Usage:
/// 
/// [HttpPost("CreateSearchCountryCity")]
/// public async Task<IActionResult> CreateSearchCountryCity(CreateSearchCountryCitiesCommand model)
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
/// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.SearchCountryCities.Command.CreateSearchCountryCities.CreateSearchCountryCitiesCommand, CleanArc.Application.Features.SearchCountryCities.Command.UpdateSearchCountryCities.UpdateSearchCountryCitiesCommand, CleanArc.Application.Features.SearchCountryCities.Command.DeleteSearchCountryCities.DeleteSearchCountryCitiesCommand, System.Boolean, CleanArc.Application.Features.SearchCountryCities.Queries.GetAllSearchCountryCities.GetAllSearchCountryCitiesQueries, System.Collections.Generic.List&lt;CleanArc.Application.Features.SearchCountryCities.Queries.GetAllSearchCountryCities.GetAllSearchCountryCitiesQueriesResult&gt;, CleanArc.Application.Features.SearchCountryCities.Queries.GetSearchCountryCitiesById.GetSearchCountryCitiesByIdQuery, CleanArc.Application.Features.SearchCountryCities.Queries.GetSearchCountryCitiesById.GetSearchCountryCitiesByIdQueryResult&gt;" />
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/SearchCountryCities")]
//[Authorize]
public class SearchCountryCitiesController : _BaseController<CreateSearchCountryCitiesCommand, UpdateSearchCountryCitiesCommand, DeleteSearchCountryCitiesCommand, bool, GetAllSearchCountryCitiesQueries,
List<GetAllSearchCountryCitiesQueriesResult>, GetSearchCountryCitiesByIdQuery, GetSearchCountryCitiesByIdQueryResult>
{

    /// <summary>
    /// Initializes a new instance of the <see cref="SearchCountryCitiesController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public SearchCountryCitiesController(ISender sender, ILogger<_BaseController<CreateSearchCountryCitiesCommand, UpdateSearchCountryCitiesCommand, DeleteSearchCountryCitiesCommand, bool, GetAllSearchCountryCitiesQueries,
List<GetAllSearchCountryCitiesQueriesResult>, GetSearchCountryCitiesByIdQuery, GetSearchCountryCitiesByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}

