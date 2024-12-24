using Asp.Versioning;
using CleanArc.Application.Features.CarDetail.Command.CreateCarDetailCommand;
using CleanArc.Application.Features.CarDetail.Command.DeleteCarDetailCommand;
using CleanArc.Application.Features.CarDetail.Command.UpdateCarDetailCommand;
using CleanArc.Application.Features.CarDetail.Queries.GetAllCarDetail;
using CleanArc.Application.Features.CarDetail.Queries.GetCarDetailById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.CarDetail
{
    /// <summary>
    /// CarDetailController is responsible for handling HTTP requests related to car detail operations
    /// such as creating, updating, deleting, and retrieving car details. It extends from a base controller
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateCarDetail: Handles the creation of a new car detail.
    /// 2. UpdateCarDetail: Handles the updating of an existing car detail.
    /// 3. DeleteCarDetail: Handles the deletion of an existing car detail.
    /// 4. GetAllCarDetail: Retrieves all car details.
    /// 5. GetCarDetailById: Retrieves a specific car detail by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/CarDetail".
    /// 
    /// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
    /// operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateCarDetail")]
    /// public async Task<IActionResult> CreateCarDetail(CreateCarDetailCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.CarDetail.Command.CreateCarDetailCommand.CreateCarDetailCommand, CleanArc.Application.Features.CarDetail.Command.UpdateCarDetailCommand.UpdateCarDetailCommand, CleanArc.Application.Features.CarDetail.Command.DeleteCarDetailCommand.DeleteCarDetailCommand, System.ResponseEntity, CleanArc.Application.Features.CarDetail.Queries.GetAllCarDetail.GetAllCarDetailQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.CarDetail.Queries.GetAllCarDetail.GetAllCarDetailQueryResult&gt;, CleanArc.Application.Features.CarDetail.Queries.GetCarDetailById.GetCarDetailByIdQuery, CleanArc.Application.Features.CarDetail.Queries.GetCarDetailById.GetCarDetailByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/CarDetail")]
    //[Authorize]

    public class CarDetailController : _BaseController<CreateCarDetailCommand, UpdateCarDetailCommand, DeleteCarDetailCommand, ResponseEntity, GetAllCarDetailQuery,
    List<GetAllCarDetailQueryResult>, GetCarDetailByIdQuery, GetCarDetailByIdQueryResult>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDetailController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public CarDetailController(ISender sender, ILogger<_BaseController<CreateCarDetailCommand, UpdateCarDetailCommand, DeleteCarDetailCommand, ResponseEntity, GetAllCarDetailQuery,
   List<GetAllCarDetailQueryResult>, GetCarDetailByIdQuery, GetCarDetailByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) :
            base(sender, logger, httpContextAccessor)
        {

        }

    }
}

