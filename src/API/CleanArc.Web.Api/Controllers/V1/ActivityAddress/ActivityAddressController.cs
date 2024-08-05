using Asp.Versioning;
using CleanArc.Application.Features.ActivityAddress.Commands.CreateActivityAddressCommand;
using CleanArc.Application.Features.ActivityAddress.Commands.DeleteActivityAddressCommand;
using CleanArc.Application.Features.ActivityAddress.Commands.UpdateActivityAddressCommand;
using CleanArc.Application.Features.ActivityAddress.Queries.GetActivityAddressById;
using CleanArc.Application.Features.ActivityAddress.Queries.GetAllActivityAddress;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.ActivityAddress
{
    /// <summary>
    /// ActivityAddressController is responsible for handling HTTP requests related to ActivityAddress operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateActivityAddress: Handles the creation of a new age type.
    /// 2. UpdateActivityAddress: Handles the updating of an existing age type.
    /// 3. DeleteActivityAddress: Handles the deletion of an existing age type.
    /// 4. GetAllActivityAddress: Retrieves all age types.
    /// 5. GetActivityAddressById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/ActivityAddress".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateActivityAddress")]
    /// public async Task<IActionResult> CreateActivityAddress(CreateActivityAddressCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.ActivityAddress.Commands.CreateActivityAddressCommand.CreateActivityAddressCommand, CleanArc.Application.Features.ActivityAddress.Commands.UpdateActivityAddressCommand.UpdateActivityAddressCommand, CleanArc.Application.Features.ActivityAddress.Commands.DeleteActivityAddressCommand.DeleteActivityAddressCommand, System.Boolean, CleanArc.Application.Features.ActivityAddress.Queries.GetAllActivityAddress.GetAllActivityAddressQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.ActivityAddress.Queries.GetAllActivityAddress.GetAllActivityAddressQueryResult&gt;, CleanArc.Application.Features.ActivityAddress.Queries.GetActivityAddressById.GetActivityAddressByIdQuery, CleanArc.Application.Features.ActivityAddress.Queries.GetActivityAddressById.GetActivityAddressByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/ActivityAddress")]
    //[Authorize]
    public class ActivityAddressController : _BaseController<CreateActivityAddressCommand, UpdateActivityAddressCommand, DeleteActivityAddressCommand, bool, GetAllActivityAddressQuery,
    List<GetAllActivityAddressQueryResult>, GetActivityAddressByIdQuery, GetActivityAddressByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public ActivityAddressController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateActivityAddress")]
        //public async Task<IActionResult> CreateActivityAddress(CreateActivityAddressCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateActivityAddress")]
        //public async Task<IActionResult> UpdateActivityAddress(UpdateActivityAddressCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteActivityAddress")]
        //public async Task<IActionResult> DeleteActivityAddress(DeleteActivityAddressCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllActivityAddress")]
        //public async Task<IActionResult> GetAllActivityAddress( )
        //{
        //    var queryResult = await _sender.Send(new GetAllActivityAddressQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityAddressController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public ActivityAddressController(ISender sender, ILogger<_BaseController<CreateActivityAddressCommand, UpdateActivityAddressCommand, DeleteActivityAddressCommand, bool, GetAllActivityAddressQuery,
   List<GetAllActivityAddressQueryResult>, GetActivityAddressByIdQuery, GetActivityAddressByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
