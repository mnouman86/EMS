using Asp.Versioning;
using CleanArc.Application.Features.City.Command.CreateCityCommand;
using CleanArc.Application.Features.City.Command.DeleteCityCommand;
using CleanArc.Application.Features.City.Command.UpdateCityCommand;
using CleanArc.Application.Features.City.Queries.GetAllCities;
using CleanArc.Application.Features.City.Queries.GetCityById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.City
{
    /// <summary>
    /// CountryController is responsible for handling HTTP requests related to country operations
    /// such as creating, updating, deleting, and retrieving countries. It extends from a base controller
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateCountry: Handles the creation of a new country.
    /// 2. UpdateCountry: Handles the updating of an existing country.
    /// 3. DeleteCountry: Handles the deletion of an existing country.
    /// 4. GetAllCountries: Retrieves all countries.
    /// 5. GetCountryById: Retrieves a specific country by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/Country".
    /// 
    /// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
    /// operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateCountry")]
    /// public async Task<IActionResult> CreateCountry(CreateCountryCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.City.Command.CreateCityCommand.CreateCityCommand, CleanArc.Application.Features.City.Command.UpdateCityCommand.UpdateCityCommand, CleanArc.Application.Features.City.Command.DeleteCityCommand.DeleteCityCommand, System.ResponseEntity, CleanArc.Application.Features.City.Queries.GetAllCities.GetAllCitiesQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.City.Queries.GetAllCities.GetAllCitiesQueryResult&gt;, CleanArc.Application.Features.City.Queries.GetCityById.GetCityByIdQuery, CleanArc.Application.Features.City.Queries.GetCityById.GetCityByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/City")]
    public class CityController : _BaseController<CreateCityCommand, UpdateCityCommand, DeleteCityCommand, ResponseEntity, GetAllCitiesQuery,
    List<GetAllCitiesQueryResult>, GetCityByIdQuery, GetCityByIdQueryResult>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CityController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public CityController(ISender sender, ILogger<_BaseController<CreateCityCommand, UpdateCityCommand, DeleteCityCommand, ResponseEntity, GetAllCitiesQuery,
   List<GetAllCitiesQueryResult>, GetCityByIdQuery, GetCityByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
