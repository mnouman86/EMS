using Asp.Versioning;
using CleanArc.Application.Features.SearchHotelRoomDetail.Command.CreateSearchHotelRoomDetail;
using CleanArc.Application.Features.SearchHotelRoomDetail.Command.DeleteSearchHotelRoomDetail;
using CleanArc.Application.Features.SearchHotelRoomDetail.Command.UpdateSearchHotelRoomDetail;
using CleanArc.Application.Features.SearchHotelRoomDetail.Queries.GetAllSearchHotelRoomDetail;
using CleanArc.Application.Features.SearchHotelRoomDetail.Queries.GetSearchHotelRoomDetailById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.SearchHotelRoomDetail;
/// <summary>
/// SearchHotelRoomDetailController is responsible for handling HTTP requests related to hotel room detail search operations
/// such as creating, updating, deleting, and retrieving hotel room details. It extends from a base controller
/// which provides common functionality for CRUD operations.
/// 
/// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
/// endpoint definitions for the following actions:
/// 
/// 1. CreateSearchHotelRoomDetail: Handles the creation of a new hotel room detail.
/// 2. UpdateSearchHotelRoomDetail: Handles the updating of an existing hotel room detail.
/// 3. DeleteSearchHotelRoomDetail: Handles the deletion of an existing hotel room detail.
/// 4. GetAllSearchHotelRoomDetails: Retrieves all hotel room details.
/// 5. GetSearchHotelRoomDetailById: Retrieves a specific hotel room detail by its ID.
/// 
/// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
/// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
/// 
/// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
/// version, specified as "api/v{version:apiVersion}/SearchHotelRoomDetail".
/// 
/// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
/// operation result handling and user ID setting.
/// 
/// Example Usage:
/// 
/// [HttpPost("CreateSearchHotelRoomDetail")]
/// public async Task<IActionResult> CreateSearchHotelRoomDetail(CreateSearchHotelRoomDetailCommand model)
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
/// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.SearchHotelRoomDetail.Command.CreateSearchHotelRoomDetail.CreateSearchHotelRoomDetailCommand, CleanArc.Application.Features.SearchHotelRoomDetail.Command.UpdateSearchHotelRoomDetail.UpdateSearchHotelRoomDetailCommand, CleanArc.Application.Features.SearchHotelRoomDetail.Command.DeleteSearchHotelRoomDetail.DeleteSearchHotelRoomDetailCommand, System.Boolean, CleanArc.Application.Features.SearchHotelRoomDetail.Queries.GetAllSearchHotelRoomDetail.GetAllSearchHotelRoomDetailQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.SearchHotelRoomDetail.Queries.GetAllSearchHotelRoomDetail.GetAllSearchHotelRoomDetailQueryResult&gt;, CleanArc.Application.Features.SearchHotelRoomDetail.Queries.GetSearchHotelRoomDetailById.GetSearchHotelRoomDetailByIdQuery, CleanArc.Application.Features.SearchHotelRoomDetail.Queries.GetSearchHotelRoomDetailById.GetSearchHotelRoomDetailByIdQueryResult&gt;" />
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/SearchHotelRoomDetail")]
public class SearchHotelRoomDetailController : _BaseController<CreateSearchHotelRoomDetailCommand, UpdateSearchHotelRoomDetailCommand, DeleteSearchHotelRoomDetailCommand, bool, GetAllSearchHotelRoomDetailQuery,
    List<GetAllSearchHotelRoomDetailQueryResult>, GetSearchHotelRoomDetailByIdQuery, GetSearchHotelRoomDetailByIdQueryResult>
{

    /// <summary>
    /// Initializes a new instance of the <see cref="SearchHotelRoomDetailController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public SearchHotelRoomDetailController(ISender sender, ILogger<_BaseController<CreateSearchHotelRoomDetailCommand, UpdateSearchHotelRoomDetailCommand, DeleteSearchHotelRoomDetailCommand, bool, GetAllSearchHotelRoomDetailQuery,
List<GetAllSearchHotelRoomDetailQueryResult>, GetSearchHotelRoomDetailByIdQuery, GetSearchHotelRoomDetailByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}
