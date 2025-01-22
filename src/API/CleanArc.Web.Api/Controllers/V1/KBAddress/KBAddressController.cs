using Asp.Versioning;
using CleanArc.Application.Features.KBAddress.Commands.CreateKBAddressCommand;
using CleanArc.Application.Features.KBAddress.Commands.DeleteKBAddressCommand;
using CleanArc.Application.Features.KBAddress.Commands.UpdateKBAddressCommand;
using CleanArc.Application.Features.KBAddress.Queries.GetKBAddressById;
using CleanArc.Application.Features.KBAddress.Queries.GetAllKBAddress;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; 
using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.KBAddress
{
    /// <summary>
    /// KBAddressController is responsible for handling HTTP requests related to KBAddress operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateKBAddress: Handles the creation of a new age type.
    /// 2. UpdateKBAddress: Handles the updating of an existing age type.
    /// 3. DeleteKBAddress: Handles the deletion of an existing age type.
    /// 4. GetAllKBAddress: Retrieves all age types.
    /// 5. GetKBAddressById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/KBAddress".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateKBAddress")]
    /// public async Task<IActionResult> CreateKBAddress(CreateKBAddressCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.KBAddress.Commands.CreateKBAddressCommand.CreateKBAddressCommand, CleanArc.Application.Features.KBAddress.Commands.UpdateKBAddressCommand.UpdateKBAddressCommand, CleanArc.Application.Features.KBAddress.Commands.DeleteKBAddressCommand.DeleteKBAddressCommand, System.ResponseEntity, CleanArc.Application.Features.KBAddress.Queries.GetAllKBAddress.GetAllKBAddressQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.KBAddress.Queries.GetAllKBAddress.GetAllKBAddressQueryResult&gt;, CleanArc.Application.Features.KBAddress.Queries.GetKBAddressById.GetKBAddressByIdQuery, CleanArc.Application.Features.KBAddress.Queries.GetKBAddressById.GetKBAddressByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/KBAddress")]
    //[Authorize]
    public class KBAddressController : _BaseController<CreateKBAddressCommand, UpdateKBAddressCommand, DeleteKBAddressCommand, ResponseEntity, GetAllKBAddressQuery,
    List<GetAllKBAddressQueryResult>, GetKBAddressByIdQuery, GetKBAddressByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public KBAddressController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateKBAddress")]
        //public async Task<IActionResult> CreateKBAddress(CreateKBAddressCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateKBAddress")]
        //public async Task<IActionResult> UpdateKBAddress(UpdateKBAddressCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteKBAddress")]
        //public async Task<IActionResult> DeleteKBAddress(DeleteKBAddressCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllKBAddress")]
        //public async Task<IActionResult> GetAllKBAddress( )
        //{
        //    var queryResult = await _sender.Send(new GetAllKBAddressQuery());

        //    return base.OperationResult(queryResult);
        //}

        private readonly ISender _sender;

        /// <summary>
        /// Initializes a new instance of the <see cref="KBAddressController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public KBAddressController(ISender sender, ILogger<_BaseController<CreateKBAddressCommand, UpdateKBAddressCommand, DeleteKBAddressCommand, ResponseEntity, GetAllKBAddressQuery,
   List<GetAllKBAddressQueryResult>, GetKBAddressByIdQuery, GetKBAddressByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {
            _sender = sender;
        }

        
    }
}
