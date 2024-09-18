using Asp.Versioning;
using CleanArc.Application.Features.Country.Command.CreateCountryCommand;
using CleanArc.Application.Features.Country.Command.DeleteCountryCommand;
using CleanArc.Application.Features.Country.Command.UpdateCountryCommand;
using CleanArc.Application.Features.Country.Queries.GetAllCountries;
using CleanArc.Application.Features.Country.Queries.GetCountryById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.Country;

/// <summary>
/// CityController is responsible for handling HTTP requests related to city operations
/// such as creating, updating, deleting, and retrieving cities. It extends from a base controller
/// which provides common functionality for CRUD operations.
/// 
/// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
/// endpoint definitions for the following actions:
/// 
/// 1. CreateCity: Handles the creation of a new city.
/// 2. UpdateCity: Handles the updating of an existing city.
/// 3. DeleteCity: Handles the deletion of an existing city.
/// 4. GetAllCities: Retrieves all cities.
/// 5. GetCityById: Retrieves a specific city by its ID.
/// 
/// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
/// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
/// 
/// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
/// version, specified as "api/v{version:apiVersion}/City".
/// 
/// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
/// operation result handling and user ID setting.
/// 
/// Example Usage:
/// 
/// [HttpPost("CreateCity")]
/// public async Task<IActionResult> CreateCity(CreateCityCommand model)
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
/// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.Country.Command.CreateCountryCommand.CreateCountryCommand, CleanArc.Application.Features.Country.Command.UpdateCountryCommand.UpdateCountryCommand, CleanArc.Application.Features.Country.Command.DeleteCountryCommand.DeleteCountryCommand, System.ResponseEntity, CleanArc.Application.Features.Country.Queries.GetAllCountries.GetAllCountryQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.Country.Queries.GetAllCountries.GetAllCountryQueryResult&gt;, CleanArc.Application.Features.Country.Queries.GetCountryById.GetCountryByIdQuery, CleanArc.Application.Features.Country.Queries.GetCountryById.GetCountryByIdQueryResult&gt;" />
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/Country")]
public class CountryController : _BaseController<CreateCountryCommand, UpdateCountryCommand, DeleteCountryCommand, ResponseEntity, GetAllCountryQuery,
    List<GetAllCountryQueryResult>, GetCountryByIdQuery, GetCountryByIdQueryResult>
{

    /// <summary>
    /// Initializes a new instance of the <see cref="CountryController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public CountryController(ISender sender, ILogger<_BaseController<CreateCountryCommand, UpdateCountryCommand, DeleteCountryCommand, ResponseEntity, GetAllCountryQuery,
List<GetAllCountryQueryResult>, GetCountryByIdQuery, GetCountryByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}


