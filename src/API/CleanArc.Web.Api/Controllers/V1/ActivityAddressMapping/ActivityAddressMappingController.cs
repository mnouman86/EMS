using Asp.Versioning;
using CleanArc.Application.Features.ActivityAddressMapping.Commands.CreateActivityAddressMappingCommand;
using CleanArc.Application.Features.ActivityAddressMapping.Commands.DeleteActivityAddressMappingCommand;
using CleanArc.Application.Features.ActivityAddressMapping.Commands.UpdateActivityAddressMappingCommand;
using CleanArc.Application.Features.ActivityAddressMapping.Queries.GetActivityAddressMappingById;
using CleanArc.Application.Features.ActivityAddressMapping.Queries.GetAllActivityAddressMapping;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.ActivityAddressMapping
{
    /// <summary>
    /// ActivityAddressMappingController is responsible for handling HTTP requests related to ActivityAddressMapping operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateActivityAddressMapping: Handles the creation of a new age type.
    /// 2. UpdateActivityAddressMapping: Handles the updating of an existing age type.
    /// 3. DeleteActivityAddressMapping: Handles the deletion of an existing age type.
    /// 4. GetAllActivityAddressMapping: Retrieves all age types.
    /// 5. GetActivityAddressMappingById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/ActivityAddressMapping".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateActivityAddressMapping")]
    /// public async Task<IActionResult> CreateActivityAddressMapping(CreateActivityAddressMappingCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.ActivityAddressMapping.Commands.CreateActivityAddressMappingCommand.CreateActivityAddressMappingCommand, CleanArc.Application.Features.ActivityAddressMapping.Commands.UpdateActivityAddressMappingCommand.UpdateActivityAddressMappingCommand, CleanArc.Application.Features.ActivityAddressMapping.Commands.DeleteActivityAddressMappingCommand.DeleteActivityAddressMappingCommand, System.Boolean, CleanArc.Application.Features.ActivityAddressMapping.Queries.GetAllActivityAddressMapping.GetAllActivityAddressMappingQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.ActivityAddressMapping.Queries.GetAllActivityAddressMapping.GetAllActivityAddressMappingQueryResult&gt;, CleanArc.Application.Features.ActivityAddressMapping.Queries.GetActivityAddressMappingById.GetActivityAddressMappingByIdQuery, CleanArc.Application.Features.ActivityAddressMapping.Queries.GetActivityAddressMappingById.GetActivityAddressMappingByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/ActivityAddressMapping")]
    //[Authorize]
    public class ActivityAddressMappingController : _BaseController<CreateActivityAddressMappingCommand, UpdateActivityAddressMappingCommand, DeleteActivityAddressMappingCommand, bool, GetAllActivityAddressMappingQuery,
    List<GetAllActivityAddressMappingQueryResult>, GetActivityAddressMappingByIdQuery, GetActivityAddressMappingByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public ActivityAddressMappingController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateActivityAddressMapping")]
        //public async Task<IActionResult> CreateActivityAddressMapping(CreateActivityAddressMappingCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateActivityAddressMapping")]
        //public async Task<IActionResult> UpdateActivityAddressMapping(UpdateActivityAddressMappingCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteActivityAddressMapping")]
        //public async Task<IActionResult> DeleteActivityAddressMapping(DeleteActivityAddressMappingCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllActivityAddressMapping")]
        //public async Task<IActionResult> GetAllActivityAddressMapping( )
        //{
        //    var queryResult = await _sender.Send(new GetAllActivityAddressMappingQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityAddressMappingController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public ActivityAddressMappingController(ISender sender, ILogger<_BaseController<CreateActivityAddressMappingCommand, UpdateActivityAddressMappingCommand, DeleteActivityAddressMappingCommand, bool, GetAllActivityAddressMappingQuery,
   List<GetAllActivityAddressMappingQueryResult>, GetActivityAddressMappingByIdQuery, GetActivityAddressMappingByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
