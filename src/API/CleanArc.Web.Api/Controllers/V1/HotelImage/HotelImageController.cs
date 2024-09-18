using Asp.Versioning;
using CleanArc.Application.Features.HotelImage.Command.CreateHotelImageCommand;
using CleanArc.Application.Features.HotelImage.Command.DeleteHotelImageCommand;
using CleanArc.Application.Features.HotelImage.Command.UpdateHotelImageCommand;
using CleanArc.Application.Features.HotelImage.Query.GetAllHotelImage;
using CleanArc.Application.Features.HotelImage.Query.GetHotelImageById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.HotelImage
{
    /// <summary>
    /// HotelImageController is responsible for handling HTTP requests related to hotel image operations
    /// such as creating, updating, deleting, and retrieving hotel images. It extends from a base controller
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateHotelImage: Handles the creation of a new hotel image.
    /// 2. UpdateHotelImage: Handles the updating of an existing hotel image.
    /// 3. DeleteHotelImage: Handles the deletion of an existing hotel image.
    /// 4. GetAllHotelImages: Retrieves all hotel images.
    /// 5. GetHotelImageById: Retrieves a specific hotel image by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/HotelImage".
    /// 
    /// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
    /// operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateHotelImage")]
    /// public async Task<IActionResult> CreateHotelImage(CreateHotelImageCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.HotelImage.Command.CreateHotelImageCommand.CreateHotelImageCommand, CleanArc.Application.Features.HotelImage.Command.UpdateHotelImageCommand.UpdateHotelImageCommand, CleanArc.Application.Features.HotelImage.Command.DeleteHotelImageCommand.DeleteHotelImageCommand, System.ResponseEntity, CleanArc.Application.Features.HotelImage.Query.GetAllHotelImage.GetAllHotelImageQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.HotelImage.Query.GetAllHotelImage.GetAllHotelImageQueryResult&gt;, CleanArc.Application.Features.HotelImage.Query.GetHotelImageById.GetHotelImageByIdQuery, CleanArc.Application.Features.HotelImage.Query.GetHotelImageById.GetHotelImageByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/HotelImage")]
    //[Authorize]

    public class HotelImageController : _BaseController<CreateHotelImageCommand, UpdateHotelImageCommand, DeleteHotelImageCommand, ResponseEntity, GetAllHotelImageQuery,
    List<GetAllHotelImageQueryResult>, GetHotelImageByIdQuery, GetHotelImageByIdQueryResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HotelImageController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public HotelImageController(ISender sender, ILogger<_BaseController<CreateHotelImageCommand, UpdateHotelImageCommand, DeleteHotelImageCommand, ResponseEntity, GetAllHotelImageQuery,
   List<GetAllHotelImageQueryResult>, GetHotelImageByIdQuery, GetHotelImageByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) :
            base(sender, logger, httpContextAccessor)
        {

        }

    }
}

