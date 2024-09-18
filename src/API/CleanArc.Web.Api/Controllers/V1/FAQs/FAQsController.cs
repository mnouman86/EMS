using Asp.Versioning;
using CleanArc.Application.Features.FAQs.Commands.CreateFAQsCommand;
using CleanArc.Application.Features.FAQs.Commands.DeleteFAQsCommand;
using CleanArc.Application.Features.FAQs.Commands.UpdateFAQsCommand;
using CleanArc.Application.Features.FAQs.Queries.GetFAQsById;
using CleanArc.Application.Features.FAQs.Queries.GetAllFAQs;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.FAQs
{
    /// <summary>
    /// FAQsController is responsible for handling HTTP requests related to FAQs operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateFAQs: Handles the creation of a new age type.
    /// 2. UpdateFAQs: Handles the updating of an existing age type.
    /// 3. DeleteFAQs: Handles the deletion of an existing age type.
    /// 4. GetAllFAQs: Retrieves all age types.
    /// 5. GetFAQsById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/FAQs".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateFAQs")]
    /// public async Task<IActionResult> CreateFAQs(CreateFAQsCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.FAQs.Commands.CreateFAQsCommand.CreateFAQsCommand, CleanArc.Application.Features.FAQs.Commands.UpdateFAQsCommand.UpdateFAQsCommand, CleanArc.Application.Features.FAQs.Commands.DeleteFAQsCommand.DeleteFAQsCommand, System.ResponseEntity, CleanArc.Application.Features.FAQs.Queries.GetAllFAQs.GetAllFAQsQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.FAQs.Queries.GetAllFAQs.GetAllFAQsQueryResult&gt;, CleanArc.Application.Features.FAQs.Queries.GetFAQsById.GetFAQsByIdQuery, CleanArc.Application.Features.FAQs.Queries.GetFAQsById.GetFAQsByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/FAQs")]
    //[Authorize]
    public class FAQsController : _BaseController<CreateFAQsCommand, UpdateFAQsCommand, DeleteFAQsCommand, ResponseEntity, GetAllFAQsQuery,
    List<GetAllFAQsQueryResult>, GetFAQsByIdQuery, GetFAQsByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public FAQsController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateFAQs")]
        //public async Task<IActionResult> CreateFAQs(CreateFAQsCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateFAQs")]
        //public async Task<IActionResult> UpdateFAQs(UpdateFAQsCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteFAQs")]
        //public async Task<IActionResult> DeleteFAQs(DeleteFAQsCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllFAQs")]
        //public async Task<IActionResult> GetAllFAQs( )
        //{
        //    var queryResult = await _sender.Send(new GetAllFAQsQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="FAQsController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public FAQsController(ISender sender, ILogger<_BaseController<CreateFAQsCommand, UpdateFAQsCommand, DeleteFAQsCommand, ResponseEntity, GetAllFAQsQuery,
   List<GetAllFAQsQueryResult>, GetFAQsByIdQuery, GetFAQsByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
