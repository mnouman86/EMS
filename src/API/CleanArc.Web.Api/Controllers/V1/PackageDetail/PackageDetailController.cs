using Asp.Versioning;
using CleanArc.Application.Features.PackageDetail.Commands.CreatePackageDetailCommand;
using CleanArc.Application.Features.PackageDetail.Commands.DeletePackageDetailCommand;
using CleanArc.Application.Features.PackageDetail.Commands.UpdatePackageDetailCommand;
using CleanArc.Application.Features.PackageDetail.Queries.GetPackageDetailById;
using CleanArc.Application.Features.PackageDetail.Queries.GetAllPackageDetail;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.PackageDetail
{
    /// <summary>
    /// PackageDetailController is responsible for handling HTTP requests related to PackageDetail operations
    /// such as creating, updating, deleting, and retrieving age types. It extends from a base controller 
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreatePackageDetail: Handles the creation of a new age type.
    /// 2. UpdatePackageDetail: Handles the updating of an existing age type.
    /// 3. DeletePackageDetail: Handles the deletion of an existing age type.
    /// 4. GetAllPackageDetail: Retrieves all age types.
    /// 5. GetPackageDetailById: Retrieves a specific age type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/PackageDetail".
    /// 
    /// Note: The actual endpoint methods are commented out but are intended to be implemented as shown to utilize
    /// the base controller's operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreatePackageDetail")]
    /// public async Task<IActionResult> CreatePackageDetail(CreatePackageDetailCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.PackageDetail.Commands.CreatePackageDetailCommand.CreatePackageDetailCommand, CleanArc.Application.Features.PackageDetail.Commands.UpdatePackageDetailCommand.UpdatePackageDetailCommand, CleanArc.Application.Features.PackageDetail.Commands.DeletePackageDetailCommand.DeletePackageDetailCommand, System.Boolean, CleanArc.Application.Features.PackageDetail.Queries.GetAllPackageDetail.GetAllPackageDetailQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.PackageDetail.Queries.GetAllPackageDetail.GetAllPackageDetailQueryResult&gt;, CleanArc.Application.Features.PackageDetail.Queries.GetPackageDetailById.GetPackageDetailByIdQuery, CleanArc.Application.Features.PackageDetail.Queries.GetPackageDetailById.GetPackageDetailByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/PackageDetail")]
    //[Authorize]
    public class PackageDetailController : _BaseController<CreatePackageDetailCommand, UpdatePackageDetailCommand, DeletePackageDetailCommand, bool, GetAllPackageDetailQuery,
    List<GetAllPackageDetailQueryResult>, GetPackageDetailByIdQuery, GetPackageDetailByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public PackageDetailController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreatePackageDetail")]
        //public async Task<IActionResult> CreatePackageDetail(CreatePackageDetailCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdatePackageDetail")]
        //public async Task<IActionResult> UpdatePackageDetail(UpdatePackageDetailCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeletePackageDetail")]
        //public async Task<IActionResult> DeletePackageDetail(DeletePackageDetailCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllPackageDetail")]
        //public async Task<IActionResult> GetAllPackageDetail( )
        //{
        //    var queryResult = await _sender.Send(new GetAllPackageDetailQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="PackageDetailController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public PackageDetailController(ISender sender, ILogger<_BaseController<CreatePackageDetailCommand, UpdatePackageDetailCommand, DeletePackageDetailCommand, bool, GetAllPackageDetailQuery,
   List<GetAllPackageDetailQueryResult>, GetPackageDetailByIdQuery, GetPackageDetailByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
