using Asp.Versioning;
using CleanArc.Application.Features.ActivityPerGroupPrice.Commands.CreateActivityPerGroupPriceCommand;
using CleanArc.Application.Features.ActivityPerGroupPrice.Commands.DeleteActivityPerGroupPriceCommand;
using CleanArc.Application.Features.ActivityPerGroupPrice.Commands.UpdateActivityPerGroupPriceCommand;
using CleanArc.Application.Features.ActivityPerGroupPrice.Queries.GetActivityPerGroupPriceById;
using CleanArc.Application.Features.ActivityPerGroupPrice.Queries.GetAllActivityPerGroupPrice;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.ActivityPerGroupPrice
{
    /// <summary>
    /// ActivityPerGroupPriceController is responsible for handling HTTP requests related to ActivityPerGroupPrice operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateActivityPerGroupPrice: Handles the creation of a new age type.
    /// 2. UpdateActivityPerGroupPrice: Handles the updating of an existing age type.
    /// 3. DeleteActivityPerGroupPrice: Handles the deletion of an existing age type.
    /// 4. GetAllActivityPerGroupPrice: Retrieves all age types.
    /// 5. GetActivityPerGroupPriceById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/ActivityPerGroupPrice".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateActivityPerGroupPrice")]
    /// public async Task<IActionResult> CreateActivityPerGroupPrice(CreateActivityPerGroupPriceCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.ActivityPerGroupPrice.Commands.CreateActivityPerGroupPriceCommand.CreateActivityPerGroupPriceCommand, CleanArc.Application.Features.ActivityPerGroupPrice.Commands.UpdateActivityPerGroupPriceCommand.UpdateActivityPerGroupPriceCommand, CleanArc.Application.Features.ActivityPerGroupPrice.Commands.DeleteActivityPerGroupPriceCommand.DeleteActivityPerGroupPriceCommand, System.ResponseEntity, CleanArc.Application.Features.ActivityPerGroupPrice.Queries.GetAllActivityPerGroupPrice.GetAllActivityPerGroupPriceQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.ActivityPerGroupPrice.Queries.GetAllActivityPerGroupPrice.GetAllActivityPerGroupPriceQueryResult&gt;, CleanArc.Application.Features.ActivityPerGroupPrice.Queries.GetActivityPerGroupPriceById.GetActivityPerGroupPriceByIdQuery, CleanArc.Application.Features.ActivityPerGroupPrice.Queries.GetActivityPerGroupPriceById.GetActivityPerGroupPriceByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/ActivityPerGroupPrice")]
    //[Authorize]
    public class ActivityPerGroupPriceController : _BaseController<CreateActivityPerGroupPriceCommand, UpdateActivityPerGroupPriceCommand, DeleteActivityPerGroupPriceCommand, ResponseEntity, GetAllActivityPerGroupPriceQuery,
    List<GetAllActivityPerGroupPriceQueryResult>, GetActivityPerGroupPriceByIdQuery, GetActivityPerGroupPriceByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public ActivityPerGroupPriceController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateActivityPerGroupPrice")]
        //public async Task<IActionResult> CreateActivityPerGroupPrice(CreateActivityPerGroupPriceCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateActivityPerGroupPrice")]
        //public async Task<IActionResult> UpdateActivityPerGroupPrice(UpdateActivityPerGroupPriceCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteActivityPerGroupPrice")]
        //public async Task<IActionResult> DeleteActivityPerGroupPrice(DeleteActivityPerGroupPriceCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllActivityPerGroupPrice")]
        //public async Task<IActionResult> GetAllActivityPerGroupPrice( )
        //{
        //    var queryResult = await _sender.Send(new GetAllActivityPerGroupPriceQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityPerGroupPriceController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public ActivityPerGroupPriceController(ISender sender, ILogger<_BaseController<CreateActivityPerGroupPriceCommand, UpdateActivityPerGroupPriceCommand, DeleteActivityPerGroupPriceCommand, ResponseEntity, GetAllActivityPerGroupPriceQuery,
   List<GetAllActivityPerGroupPriceQueryResult>, GetActivityPerGroupPriceByIdQuery, GetActivityPerGroupPriceByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
