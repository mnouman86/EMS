using Asp.Versioning;
using CleanArc.Application.Features.SubService.Commands.CreateSubServiceCommand;
using CleanArc.Application.Features.SubService.Commands.DeleteSubServiceCommand;
using CleanArc.Application.Features.SubService.Commands.UpdateSubServiceCommand;
using CleanArc.Application.Features.SubService.Queries.GetSubServiceById;
using CleanArc.Application.Features.SubService.Queries.GetAllSubService;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.SubService
{
    /// <summary>
    /// SubServiceController is responsible for handling HTTP requests related to SubService operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateSubService: Handles the creation of a new age type.
    /// 2. UpdateSubService: Handles the updating of an existing age type.
    /// 3. DeleteSubService: Handles the deletion of an existing age type.
    /// 4. GetAllSubService: Retrieves all age types.
    /// 5. GetSubServiceById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/SubService".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateSubService")]
    /// public async Task<IActionResult> CreateSubService(CreateSubServiceCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.SubService.Commands.CreateSubServiceCommand.CreateSubServiceCommand, CleanArc.Application.Features.SubService.Commands.UpdateSubServiceCommand.UpdateSubServiceCommand, CleanArc.Application.Features.SubService.Commands.DeleteSubServiceCommand.DeleteSubServiceCommand, System.Boolean, CleanArc.Application.Features.SubService.Queries.GetAllSubService.GetAllSubServiceQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.SubService.Queries.GetAllSubService.GetAllSubServiceQueryResult&gt;, CleanArc.Application.Features.SubService.Queries.GetSubServiceById.GetSubServiceByIdQuery, CleanArc.Application.Features.SubService.Queries.GetSubServiceById.GetSubServiceByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/SubService")]
    //[Authorize]
    public class SubServiceController : _BaseController<CreateSubServiceCommand, UpdateSubServiceCommand, DeleteSubServiceCommand, bool, GetAllSubServiceQuery,
    List<GetAllSubServiceQueryResult>, GetSubServiceByIdQuery, GetSubServiceByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public SubServiceController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateSubService")]
        //public async Task<IActionResult> CreateSubService(CreateSubServiceCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateSubService")]
        //public async Task<IActionResult> UpdateSubService(UpdateSubServiceCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteSubService")]
        //public async Task<IActionResult> DeleteSubService(DeleteSubServiceCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllSubService")]
        //public async Task<IActionResult> GetAllSubService( )
        //{
        //    var queryResult = await _sender.Send(new GetAllSubServiceQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="SubServiceController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public SubServiceController(ISender sender, ILogger<_BaseController<CreateSubServiceCommand, UpdateSubServiceCommand, DeleteSubServiceCommand, bool, GetAllSubServiceQuery,
   List<GetAllSubServiceQueryResult>, GetSubServiceByIdQuery, GetSubServiceByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
