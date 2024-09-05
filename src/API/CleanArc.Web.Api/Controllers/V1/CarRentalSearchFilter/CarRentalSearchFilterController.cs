using Asp.Versioning;
using CleanArc.Application.Features.CarRentalSearchFilter.Command.CreateCarRentalSearchFilter;
using CleanArc.Application.Features.CarRentalSearchFilter.Command.DeleteCarRentalSearchFilter;
using CleanArc.Application.Features.CarRentalSearchFilter.Command.UpdateCarRentalSearchFilter;
using CleanArc.Application.Features.CarRentalSearchFilter.Queries.GetAllCarRentalSearchFilter;
using CleanArc.Application.Features.CarRentalSearchFilter.Queries.GetCarRentalSearchFilterById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.CarRentalSearchFilter;
/// <summary>
/// CarRentalSearchFilterController is responsible for handling HTTP requests related to hotel room detail search operations
/// such as creating, updating, deleting, and retrieving hotel room details. It extends from a base controller
/// which provides common functionality for CRUD operations.
/// 
/// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
/// endpoint definitions for the following actions:
/// 
/// 1. CreateCarRentalSearchFilter: Handles the creation of a new hotel room detail.
/// 2. UpdateCarRentalSearchFilter: Handles the updating of an existing hotel room detail.
/// 3. DeleteCarRentalSearchFilter: Handles the deletion of an existing hotel room detail.
/// 4. GetAllCarRentalSearchFilters: Retrieves all hotel room details.
/// 5. GetCarRentalSearchFilterById: Retrieves a specific hotel room detail by its ID.
/// 
/// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
/// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
/// 
/// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
/// version, specified as "api/v{version:apiVersion}/CarRentalSearchFilter".
/// 
/// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
/// operation result handling and user ID setting.
/// 
/// Example Usage:
/// 
/// [HttpPost("CreateCarRentalSearchFilter")]
/// public async Task<IActionResult> CreateCarRentalSearchFilter(CreateCarRentalSearchFilterCommand model)
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
/// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.CarRentalSearchFilter.Command.CreateCarRentalSearchFilter.CreateCarRentalSearchFilterCommand, CleanArc.Application.Features.CarRentalSearchFilter.Command.UpdateCarRentalSearchFilter.UpdateCarRentalSearchFilterCommand, CleanArc.Application.Features.CarRentalSearchFilter.Command.DeleteCarRentalSearchFilter.DeleteCarRentalSearchFilterCommand, System.Boolean, CleanArc.Application.Features.CarRentalSearchFilter.Queries.GetAllCarRentalSearchFilter.GetAllCarRentalSearchFilterQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.CarRentalSearchFilter.Queries.GetAllCarRentalSearchFilter.GetAllCarRentalSearchFilterQueryResult&gt;, CleanArc.Application.Features.CarRentalSearchFilter.Queries.GetCarRentalSearchFilterById.GetCarRentalSearchFilterByIdQuery, CleanArc.Application.Features.CarRentalSearchFilter.Queries.GetCarRentalSearchFilterById.GetCarRentalSearchFilterByIdQueryResult&gt;" />
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/CarRentalSearchFilter")]
public class CarRentalSearchFilterController : _BaseController<CreateCarRentalSearchFilterCommand, UpdateCarRentalSearchFilterCommand, DeleteCarRentalSearchFilterCommand, bool, GetAllCarRentalSearchFilterQuery,
    List<GetAllCarRentalSearchFilterQueryResult>, GetCarRentalSearchFilterByIdQuery, GetCarRentalSearchFilterByIdQueryResult>
{

    /// <summary>
    /// Initializes a new instance of the <see cref="CarRentalSearchFilterController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public CarRentalSearchFilterController(ISender sender, ILogger<_BaseController<CreateCarRentalSearchFilterCommand, UpdateCarRentalSearchFilterCommand, DeleteCarRentalSearchFilterCommand, bool, GetAllCarRentalSearchFilterQuery,
List<GetAllCarRentalSearchFilterQueryResult>, GetCarRentalSearchFilterByIdQuery, GetCarRentalSearchFilterByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}
