using Asp.Versioning;
using CleanArc.Application.Features.KBDetail.Commands.CreateKBDetailCommand;
using CleanArc.Application.Features.KBDetail.Commands.DeleteKBDetailCommand;
using CleanArc.Application.Features.KBDetail.Commands.UpdateKBDetailCommand;
using CleanArc.Application.Features.KBDetail.Queries.GetKBDetailById;
using CleanArc.Application.Features.KBDetail.Queries.GetAllKBDetail;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; 
using CleanArc.Domain.Common;
using CleanArc.Application.Features.KBDetail.Queries.GetKBMinimalView;
using CleanArc.Application.Features.KBDetail.Queries.GetKBCoreAreasWiseMinimalView;

namespace CleanArc.Web.Api.Controllers.V1.KBDetail
{
    /// <summary>
    /// KBDetailController is responsible for handling HTTP requests related to KBDetail operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateKBDetail: Handles the creation of a new age type.
    /// 2. UpdateKBDetail: Handles the updating of an existing age type.
    /// 3. DeleteKBDetail: Handles the deletion of an existing age type.
    /// 4. GetAllKBDetail: Retrieves all age types.
    /// 5. GetKBDetailById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/KBDetail".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateKBDetail")]
    /// public async Task<IActionResult> CreateKBDetail(CreateKBDetailCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.KBDetail.Commands.CreateKBDetailCommand.CreateKBDetailCommand, CleanArc.Application.Features.KBDetail.Commands.UpdateKBDetailCommand.UpdateKBDetailCommand, CleanArc.Application.Features.KBDetail.Commands.DeleteKBDetailCommand.DeleteKBDetailCommand, System.ResponseEntity, CleanArc.Application.Features.KBDetail.Queries.GetAllKBDetail.GetAllKBDetailQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.KBDetail.Queries.GetAllKBDetail.GetAllKBDetailQueryResult&gt;, CleanArc.Application.Features.KBDetail.Queries.GetKBDetailById.GetKBDetailByIdQuery, CleanArc.Application.Features.KBDetail.Queries.GetKBDetailById.GetKBDetailByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/KBDetail")]
    //[Authorize]
    public class KBDetailController : _BaseController<CreateKBDetailCommand, UpdateKBDetailCommand, DeleteKBDetailCommand, ResponseEntity, GetAllKBDetailQuery,
    List<GetAllKBDetailQueryResult>, GetKBDetailByIdQuery, GetKBDetailByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public KBDetailController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateKBDetail")]
        //public async Task<IActionResult> CreateKBDetail(CreateKBDetailCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateKBDetail")]
        //public async Task<IActionResult> UpdateKBDetail(UpdateKBDetailCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteKBDetail")]
        //public async Task<IActionResult> DeleteKBDetail(DeleteKBDetailCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllKBDetail")]
        //public async Task<IActionResult> GetAllKBDetail( )
        //{
        //    var queryResult = await _sender.Send(new GetAllKBDetailQuery());

        //    return base.OperationResult(queryResult);
        //}

        private readonly ISender _sender;

        /// <summary>
        /// Initializes a new instance of the <see cref="KBDetailController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public KBDetailController(ISender sender, ILogger<_BaseController<CreateKBDetailCommand, UpdateKBDetailCommand, DeleteKBDetailCommand, ResponseEntity, GetAllKBDetailQuery,
   List<GetAllKBDetailQueryResult>, GetKBDetailByIdQuery, GetKBDetailByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {
            _sender = sender;
        }

        /// <summary>
        /// Get the Details for Landing Page of Knowledgebase
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("GetKBMinimalView")]
        public async Task<IActionResult> GetKBMinimalView(GetKBMinimalViewQuery query)
        {
            var result = await _sender.Send(query);

            return base.OperationResult(result);
        }

        /// <summary>
        /// Get Core Area Wise Details for Landing Page of Knowledgebase
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("GetKBCoreAreasMinimalView")]
        public async Task<IActionResult> GetKBCoreAreasMinimalView(GetKBCoreAreasWiseMinimalViewQuery query)
        {
            var result = await _sender.Send(query);

            return base.OperationResult(result);
        }
    }
}
