using Asp.Versioning;
using CleanArc.Application.Features.PackageType.Commands.CreatePackageTypeCommand;
using CleanArc.Application.Features.PackageType.Commands.DeletePackageTypeCommand;
using CleanArc.Application.Features.PackageType.Commands.UpdatePackageTypeCommand;
using CleanArc.Application.Features.PackageType.Queries.GetPackageTypeById;
using CleanArc.Application.Features.PackageType.Queries.GetAllPackageType;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.PackageType
{
    /// <summary>
    /// PackageTypeController is responsible for handling HTTP requests related to PackageType operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreatePackageType: Handles the creation of a new age type.
    /// 2. UpdatePackageType: Handles the updating of an existing age type.
    /// 3. DeletePackageType: Handles the deletion of an existing age type.
    /// 4. GetAllPackageType: Retrieves all age types.
    /// 5. GetPackageTypeById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/PackageType".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreatePackageType")]
    /// public async Task<IActionResult> CreatePackageType(CreatePackageTypeCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.PackageType.Commands.CreatePackageTypeCommand.CreatePackageTypeCommand, CleanArc.Application.Features.PackageType.Commands.UpdatePackageTypeCommand.UpdatePackageTypeCommand, CleanArc.Application.Features.PackageType.Commands.DeletePackageTypeCommand.DeletePackageTypeCommand, System.Boolean, CleanArc.Application.Features.PackageType.Queries.GetAllPackageType.GetAllPackageTypeQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.PackageType.Queries.GetAllPackageType.GetAllPackageTypeQueryResult&gt;, CleanArc.Application.Features.PackageType.Queries.GetPackageTypeById.GetPackageTypeByIdQuery, CleanArc.Application.Features.PackageType.Queries.GetPackageTypeById.GetPackageTypeByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/PackageType")]
    //[Authorize]
    public class PackageTypeController : _BaseController<CreatePackageTypeCommand, UpdatePackageTypeCommand, DeletePackageTypeCommand, bool, GetAllPackageTypeQuery,
    List<GetAllPackageTypeQueryResult>, GetPackageTypeByIdQuery, GetPackageTypeByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public PackageTypeController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreatePackageType")]
        //public async Task<IActionResult> CreatePackageType(CreatePackageTypeCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdatePackageType")]
        //public async Task<IActionResult> UpdatePackageType(UpdatePackageTypeCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeletePackageType")]
        //public async Task<IActionResult> DeletePackageType(DeletePackageTypeCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllPackageType")]
        //public async Task<IActionResult> GetAllPackageType( )
        //{
        //    var queryResult = await _sender.Send(new GetAllPackageTypeQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="PackageTypeController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public PackageTypeController(ISender sender, ILogger<_BaseController<CreatePackageTypeCommand, UpdatePackageTypeCommand, DeletePackageTypeCommand, bool, GetAllPackageTypeQuery,
   List<GetAllPackageTypeQueryResult>, GetPackageTypeByIdQuery, GetPackageTypeByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
