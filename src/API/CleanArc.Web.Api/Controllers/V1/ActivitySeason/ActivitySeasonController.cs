using Asp.Versioning;
using CleanArc.Application.Features.ActivitySeason.Commands.CreateActivitySeasonCommand;
using CleanArc.Application.Features.ActivitySeason.Commands.DeleteActivitySeasonCommand;
using CleanArc.Application.Features.ActivitySeason.Commands.UpdateActivitySeasonCommand;
using CleanArc.Application.Features.ActivitySeason.Queries.GetActivitySeasonById;
using CleanArc.Application.Features.ActivitySeason.Queries.GetAllActivitySeason;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.ActivitySeason
{
    /// <summary>
    /// ActivitySeasonController is responsible for handling HTTP requests related to ActivitySeason operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateActivitySeason: Handles the creation of a new age type.
    /// 2. UpdateActivitySeason: Handles the updating of an existing age type.
    /// 3. DeleteActivitySeason: Handles the deletion of an existing age type.
    /// 4. GetAllActivitySeason: Retrieves all age types.
    /// 5. GetActivitySeasonById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/ActivitySeason".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateActivitySeason")]
    /// public async Task<IActionResult> CreateActivitySeason(CreateActivitySeasonCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.ActivitySeason.Commands.CreateActivitySeasonCommand.CreateActivitySeasonCommand, CleanArc.Application.Features.ActivitySeason.Commands.UpdateActivitySeasonCommand.UpdateActivitySeasonCommand, CleanArc.Application.Features.ActivitySeason.Commands.DeleteActivitySeasonCommand.DeleteActivitySeasonCommand, System.ResponseEntity, CleanArc.Application.Features.ActivitySeason.Queries.GetAllActivitySeason.GetAllActivitySeasonQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.ActivitySeason.Queries.GetAllActivitySeason.GetAllActivitySeasonQueryResult&gt;, CleanArc.Application.Features.ActivitySeason.Queries.GetActivitySeasonById.GetActivitySeasonByIdQuery, CleanArc.Application.Features.ActivitySeason.Queries.GetActivitySeasonById.GetActivitySeasonByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/ActivitySeason")]
    //[Authorize]
    public class ActivitySeasonController : _BaseController<CreateActivitySeasonCommand, UpdateActivitySeasonCommand, DeleteActivitySeasonCommand, ResponseEntity, GetAllActivitySeasonQuery,
    List<GetAllActivitySeasonQueryResult>, GetActivitySeasonByIdQuery, GetActivitySeasonByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public ActivitySeasonController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateActivitySeason")]
        //public async Task<IActionResult> CreateActivitySeason(CreateActivitySeasonCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateActivitySeason")]
        //public async Task<IActionResult> UpdateActivitySeason(UpdateActivitySeasonCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteActivitySeason")]
        //public async Task<IActionResult> DeleteActivitySeason(DeleteActivitySeasonCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllActivitySeason")]
        //public async Task<IActionResult> GetAllActivitySeason( )
        //{
        //    var queryResult = await _sender.Send(new GetAllActivitySeasonQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivitySeasonController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public ActivitySeasonController(ISender sender, ILogger<_BaseController<CreateActivitySeasonCommand, UpdateActivitySeasonCommand, DeleteActivitySeasonCommand, ResponseEntity, GetAllActivitySeasonQuery,
   List<GetAllActivitySeasonQueryResult>, GetActivitySeasonByIdQuery, GetActivitySeasonByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
