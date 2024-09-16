using Asp.Versioning;
using CleanArc.Application.Features.PopularItemsVisit.Commands.CreatePopularItemsVisitCommand;
using CleanArc.Application.Features.PopularItemsVisit.Commands.DeletePopularItemsVisitCommand;
using CleanArc.Application.Features.PopularItemsVisit.Commands.UpdatePopularItemsVisitCommand;
using CleanArc.Application.Features.PopularItemsVisit.Queries.GetPopularItemsVisitById;
using CleanArc.Application.Features.PopularItemsVisit.Queries.GetAllPopularItemsVisit;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.PopularItemsVisit
{
    /// <summary>
    /// PopularItemsVisitController is responsible for handling HTTP requests related to PopularItemsVisit operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreatePopularItemsVisit: Handles the creation of a new age type.
    /// 2. UpdatePopularItemsVisit: Handles the updating of an existing age type.
    /// 3. DeletePopularItemsVisit: Handles the deletion of an existing age type.
    /// 4. GetAllPopularItemsVisit: Retrieves all age types.
    /// 5. GetPopularItemsVisitById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/PopularItemsVisit".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreatePopularItemsVisit")]
    /// public async Task<IActionResult> CreatePopularItemsVisit(CreatePopularItemsVisitCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.PopularItemsVisit.Commands.CreatePopularItemsVisitCommand.CreatePopularItemsVisitCommand, CleanArc.Application.Features.PopularItemsVisit.Commands.UpdatePopularItemsVisitCommand.UpdatePopularItemsVisitCommand, CleanArc.Application.Features.PopularItemsVisit.Commands.DeletePopularItemsVisitCommand.DeletePopularItemsVisitCommand, System.Boolean, CleanArc.Application.Features.PopularItemsVisit.Queries.GetAllPopularItemsVisit.GetAllPopularItemsVisitQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.PopularItemsVisit.Queries.GetAllPopularItemsVisit.GetAllPopularItemsVisitQueryResult&gt;, CleanArc.Application.Features.PopularItemsVisit.Queries.GetPopularItemsVisitById.GetPopularItemsVisitByIdQuery, CleanArc.Application.Features.PopularItemsVisit.Queries.GetPopularItemsVisitById.GetPopularItemsVisitByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/PopularItemsVisit")]
    //[Authorize]
    public class PopularItemsVisitController : _BaseController<CreatePopularItemsVisitCommand, UpdatePopularItemsVisitCommand, DeletePopularItemsVisitCommand, bool, GetAllPopularItemsVisitQuery,
    List<GetAllPopularItemsVisitQueryResult>, GetPopularItemsVisitByIdQuery, GetPopularItemsVisitByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public PopularItemsVisitController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreatePopularItemsVisit")]
        //public async Task<IActionResult> CreatePopularItemsVisit(CreatePopularItemsVisitCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdatePopularItemsVisit")]
        //public async Task<IActionResult> UpdatePopularItemsVisit(UpdatePopularItemsVisitCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeletePopularItemsVisit")]
        //public async Task<IActionResult> DeletePopularItemsVisit(DeletePopularItemsVisitCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllPopularItemsVisit")]
        //public async Task<IActionResult> GetAllPopularItemsVisit( )
        //{
        //    var queryResult = await _sender.Send(new GetAllPopularItemsVisitQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="PopularItemsVisitController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public PopularItemsVisitController(ISender sender, ILogger<_BaseController<CreatePopularItemsVisitCommand, UpdatePopularItemsVisitCommand, DeletePopularItemsVisitCommand, bool, GetAllPopularItemsVisitQuery,
   List<GetAllPopularItemsVisitQueryResult>, GetPopularItemsVisitByIdQuery, GetPopularItemsVisitByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
