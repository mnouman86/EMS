using Asp.Versioning;
using CleanArc.Application.Features.SearchBusinessCarDetail.Command.CreateSearchBusinessCarDetail;
using CleanArc.Application.Features.SearchBusinessCarDetail.Command.DeleteSearchBusinessCarDetail;
using CleanArc.Application.Features.SearchBusinessCarDetail.Command.UpdateSearchBusinessCarDetail;
using CleanArc.Application.Features.SearchBusinessCarDetail.Queries.GetAllSearchBusinessCarDetail;
using CleanArc.Application.Features.SearchBusinessCarDetail.Queries.GetSearchBusinessCarDetailById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.SearchBusinessCarDetail;
/// <summary>
/// SearchBusinessCarDetailController is responsible for handling HTTP requests related to hotel room detail search operations
/// such as creating, updating, deleting, and retrieving hotel room details. It extends from a base controller
/// which provides common functionality for CRUD operations.
/// 
/// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
/// endpoint definitions for the following actions:
/// 
/// 1. CreateSearchBusinessCarDetail: Handles the creation of a new hotel room detail.
/// 2. UpdateSearchBusinessCarDetail: Handles the updating of an existing hotel room detail.
/// 3. DeleteSearchBusinessCarDetail: Handles the deletion of an existing hotel room detail.
/// 4. GetAllSearchBusinessCarDetails: Retrieves all hotel room details.
/// 5. GetSearchBusinessCarDetailById: Retrieves a specific hotel room detail by its ID.
/// 
/// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
/// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
/// 
/// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
/// version, specified as "api/v{version:apiVersion}/SearchBusinessCarDetail".
/// 
/// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
/// operation result handling and user ID setting.
/// 
/// Example Usage:
/// 
/// [HttpPost("CreateSearchBusinessCarDetail")]
/// public async Task<IActionResult> CreateSearchBusinessCarDetail(CreateSearchBusinessCarDetailCommand model)
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
/// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.SearchBusinessCarDetail.Command.CreateSearchBusinessCarDetail.CreateSearchBusinessCarDetailCommand, CleanArc.Application.Features.SearchBusinessCarDetail.Command.UpdateSearchBusinessCarDetail.UpdateSearchBusinessCarDetailCommand, CleanArc.Application.Features.SearchBusinessCarDetail.Command.DeleteSearchBusinessCarDetail.DeleteSearchBusinessCarDetailCommand, System.Boolean, CleanArc.Application.Features.SearchBusinessCarDetail.Queries.GetAllSearchBusinessCarDetail.GetAllSearchBusinessCarDetailQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.SearchBusinessCarDetail.Queries.GetAllSearchBusinessCarDetail.GetAllSearchBusinessCarDetailQueryResult&gt;, CleanArc.Application.Features.SearchBusinessCarDetail.Queries.GetSearchBusinessCarDetailById.GetSearchBusinessCarDetailByIdQuery, CleanArc.Application.Features.SearchBusinessCarDetail.Queries.GetSearchBusinessCarDetailById.GetSearchBusinessCarDetailByIdQueryResult&gt;" />
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/SearchBusinessCarDetail")]
public class SearchBusinessCarDetailController : _BaseController<CreateSearchBusinessCarDetailCommand, UpdateSearchBusinessCarDetailCommand, DeleteSearchBusinessCarDetailCommand, bool, GetAllSearchBusinessCarDetailQuery,
    List<GetAllSearchBusinessCarDetailQueryResult>, GetSearchBusinessCarDetailByIdQuery, GetSearchBusinessCarDetailByIdQueryResult>
{

    /// <summary>
    /// Initializes a new instance of the <see cref="SearchBusinessCarDetailController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public SearchBusinessCarDetailController(ISender sender, ILogger<_BaseController<CreateSearchBusinessCarDetailCommand, UpdateSearchBusinessCarDetailCommand, DeleteSearchBusinessCarDetailCommand, bool, GetAllSearchBusinessCarDetailQuery,
List<GetAllSearchBusinessCarDetailQueryResult>, GetSearchBusinessCarDetailByIdQuery, GetSearchBusinessCarDetailByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}
