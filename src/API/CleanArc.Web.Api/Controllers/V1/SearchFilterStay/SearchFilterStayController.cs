using Asp.Versioning;
using CleanArc.Application.Features.SearchFilterStay.Commands.CreateSearchFilterStayCommand;
using CleanArc.Application.Features.SearchFilterStay.Commands.DeleteSearchFilterStayCommand;
using CleanArc.Application.Features.SearchFilterStay.Commands.UpdateSearchFilterStayCommand;
using CleanArc.Application.Features.SearchFilterStay.Queries.GetAllSearchFilterStay;
using CleanArc.Application.Features.SearchFilterStay.Queries.GetSearchFilterStayById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.SearchFilterStayDetail;

/// <summary>
/// SearchFilterStayDetailController is responsible for handling HTTP requests related to hotel search operations
/// such as creating, updating, deleting, and retrieving hotel details. It extends from a base controller
/// which provides common functionality for CRUD operations.
/// 
/// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
/// endpoint definitions for the following actions:
/// 
/// 1. CreateSearchFilterStay: Handles the creation of a new hotel.
/// 2. UpdateSearchFilterStay: Handles the updating of an existing hotel.
/// 3. DeleteSearchFilterStay: Handles the deletion of an existing hotel.
/// 4. GetAllSearchFilterStay: Retrieves all hotels.
/// 5. GetSearchFilterStayById: Retrieves a specific hotel by its ID.
/// 
/// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
/// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
/// 
/// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
/// version, specified as "api/v{version:apiVersion}/SearchFilterStayDetail".
/// 
/// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
/// operation result handling and user ID setting.
/// 
/// Example Usage:
/// 
/// [HttpPost("CreateSearchFilterStay")]
/// public async Task<IActionResult> CreateSearchFilterStay(CreateSearchFilterStayCommand model)
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
/// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.SearchFilterStay.Commands.CreateSearchFilterStayCommand.CreateSearchFilterStayCommand, CleanArc.Application.Features.SearchFilterStay.Commands.UpdateSearchFilterStayCommand.UpdateSearchFilterStayCommand, CleanArc.Application.Features.SearchFilterStay.Commands.DeleteSearchFilterStayCommand.DeleteSearchFilterStayCommand, System.ResponseEntity, CleanArc.Application.Features.SearchFilterStay.Queries.GetAllSearchFilterStay.GetAllSearchFilterStayQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.SearchFilterStay.Queries.GetAllSearchFilterStay.GetAllSearchFilterStayQueryResult&gt;, CleanArc.Application.Features.SearchFilterStay.Queries.GetSearchFilterStayById.GetSearchFilterStayByIdQuery, CleanArc.Application.Features.SearchFilterStay.Queries.GetSearchFilterStayById.GetSearchFilterStayByIdQueryResult&gt;" />
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/SearchFilterStayDetail")]
//[Authorize]
public class SearchFilterStayController : _BaseController<CreateSearchFilterStayCommand, UpdateSearchFilterStayCommand, DeleteSearchFilterStayCommand, ResponseEntity, GetAllSearchFilterStayQuery,
List<GetAllSearchFilterStayQueryResult>, GetSearchFilterStayByIdQuery, GetSearchFilterStayByIdQueryResult>
{

    /// <summary>
    /// Initializes a new instance of the <see cref="SearchFilterStayDetailController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public SearchFilterStayController(ISender sender, ILogger<_BaseController<CreateSearchFilterStayCommand, UpdateSearchFilterStayCommand, DeleteSearchFilterStayCommand, ResponseEntity, GetAllSearchFilterStayQuery,
List<GetAllSearchFilterStayQueryResult>, GetSearchFilterStayByIdQuery, GetSearchFilterStayByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }
}
