using Asp.Versioning;
using CleanArc.Application.Features.AgeType.Commands.CreateAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Commands.DeleteAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Commands.UpdateAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
using CleanArc.Application.Features.AgeType.Queries.GetAllAgeType;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.AgeType
{
    /// <summary>
    /// AgeTypeController is responsible for handling HTTP requests related to AgeType operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateAgeType: Handles the creation of a new age type.
    /// 2. UpdateAgeType: Handles the updating of an existing age type.
    /// 3. DeleteAgeType: Handles the deletion of an existing age type.
    /// 4. GetAllAgeType: Retrieves all age types.
    /// 5. GetAgeTypeById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/AgeType".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateAgeType")]
    /// public async Task<IActionResult> CreateAgeType(CreateAgeTypeCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.AgeType.Commands.CreateAgeTypeCommand.CreateAgeTypeCommand, CleanArc.Application.Features.AgeType.Commands.UpdateAgeTypeCommand.UpdateAgeTypeCommand, CleanArc.Application.Features.AgeType.Commands.DeleteAgeTypeCommand.DeleteAgeTypeCommand, System.Boolean, CleanArc.Application.Features.AgeType.Queries.GetAllAgeType.GetAllAgeTypeQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.AgeType.Queries.GetAllAgeType.GetAllAgeTypeQueryResult&gt;, CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById.GetAgeTypeByIdQuery, CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById.GetAgeTypeByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/AgeType")]
    //[Authorize]
    public class AgeTypeController : _BaseController<CreateAgeTypeCommand, UpdateAgeTypeCommand, DeleteAgeTypeCommand, bool, GetAllAgeTypeQuery,
    List<GetAllAgeTypeQueryResult>, GetAgeTypeByIdQuery, GetAgeTypeByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public AgeTypeController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateAgeType")]
        //public async Task<IActionResult> CreateAgeType(CreateAgeTypeCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateAgeType")]
        //public async Task<IActionResult> UpdateAgeType(UpdateAgeTypeCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteAgeType")]
        //public async Task<IActionResult> DeleteAgeType(DeleteAgeTypeCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllAgeType")]
        //public async Task<IActionResult> GetAllAgeType( )
        //{
        //    var queryResult = await _sender.Send(new GetAllAgeTypeQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="AgeTypeController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public AgeTypeController(ISender sender, ILogger<_BaseController<CreateAgeTypeCommand, UpdateAgeTypeCommand, DeleteAgeTypeCommand, bool, GetAllAgeTypeQuery,
   List<GetAllAgeTypeQueryResult>, GetAgeTypeByIdQuery, GetAgeTypeByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
