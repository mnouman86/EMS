using Asp.Versioning;
using CleanArc.Application.Features.HomeSlider.Command.CreateHomeSliderCommand;
using CleanArc.Application.Features.HomeSlider.Command.DeleteHomeSliderCommand;
using CleanArc.Application.Features.HomeSlider.Command.UpdateHomeSliderCommand;
using CleanArc.Application.Features.HomeSlider.Queries.GetAllHomeSliders;
using CleanArc.Application.Features.HomeSlider.Queries.GetHomeSliderById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.HomeSlider
{
    /// <summary>
    /// HomeSliderController is responsible for handling HTTP requests related to room type operations
    /// such as creating, updating, deleting, and retrieving room types. It extends from a base controller
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateHomeSlider: Handles the creation of a new room type.
    /// 2. UpdateHomeSlider: Handles the updating of an existing room type.
    /// 3. DeleteHomeSlider: Handles the deletion of an existing room type.
    /// 4. GetAllHomeSliders: Retrieves all room types.
    /// 5. GetHomeSliderById: Retrieves a specific room type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/HomeSlider".
    /// 
    /// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
    /// operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateHomeSlider")]
    /// public async Task<IActionResult> CreateHomeSlider(CreateHomeSliderCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.HomeSlider.Command.CreateHomeSliderCommand.CreateHomeSliderCommand, CleanArc.Application.Features.HomeSlider.Command.UpdateHomeSliderCommand.UpdateHomeSliderCommand, CleanArc.Application.Features.HomeSlider.Command.DeleteHomeSliderCommand.DeleteHomeSliderCommand, System.ResponseEntity, CleanArc.Application.Features.HomeSlider.Queries.GetAllHomeSliders.GetAllHomeSlidersQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.HomeSlider.Queries.GetAllHomeSliders.GetAllHomeSlidersQueryResult&gt;, CleanArc.Application.Features.HomeSlider.Queries.GetHomeSliderById.GetHomeSliderByIdQuery, CleanArc.Application.Features.HomeSlider.Queries.GetHomeSliderById.GetHomeSliderByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/HomeSlider")]
    //[Authorize]
    public class HomeSliderController : _BaseController<CreateHomeSliderCommand, UpdateHomeSliderCommand, DeleteHomeSliderCommand, ResponseEntity, GetAllHomeSlidersQuery,
    List<GetAllHomeSlidersQueryResult>, GetHomeSliderByIdQuery, GetHomeSliderByIdQueryResult>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeSliderController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public HomeSliderController(ISender sender, ILogger<_BaseController<CreateHomeSliderCommand, UpdateHomeSliderCommand, DeleteHomeSliderCommand, ResponseEntity, GetAllHomeSlidersQuery,
   List<GetAllHomeSlidersQueryResult>, GetHomeSliderByIdQuery, GetHomeSliderByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
