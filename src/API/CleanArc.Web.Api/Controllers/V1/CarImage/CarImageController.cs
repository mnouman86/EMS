using Asp.Versioning;
using CleanArc.Application.Features.CarImage.Command.CreateCarImageCommand;
using CleanArc.Application.Features.CarImage.Command.DeleteCarImageCommand;
using CleanArc.Application.Features.CarImage.Command.UpdateCarImageCommand;
using CleanArc.Application.Features.CarImage.Query.GetAllCarImage;
using CleanArc.Application.Features.CarImage.Query.GetCarImageById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.CarImage
{
    /// <summary>
    /// CarImageController is responsible for handling HTTP requests related to car image operations
    /// such as creating, updating, deleting, and retrieving car images. It extends from a base controller
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateCarImage: Handles the creation of a new car image.
    /// 2. UpdateCarImage: Handles the updating of an existing car image.
    /// 3. DeleteCarImage: Handles the deletion of an existing car image.
    /// 4. GetAllCarImage: Retrieves all car images.
    /// 5. GetCarImageById: Retrieves a specific car image by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/CarImage".
    /// 
    /// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
    /// operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateCarImage")]
    /// public async Task<IActionResult> CreateCarImage(CreateCarImageCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.CarImage.Command.CreateCarImageCommand.CreateCarImageCommand, CleanArc.Application.Features.CarImage.Command.UpdateCarImageCommand.UpdateCarImageCommand, CleanArc.Application.Features.CarImage.Command.DeleteCarImageCommand.DeleteCarImageCommand, System.ResponseEntity, CleanArc.Application.Features.CarImage.Query.GetAllCarImage.GetAllCarImageQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.CarImage.Query.GetAllCarImage.GetAllCarImageQueryResult&gt;, CleanArc.Application.Features.CarImage.Query.GetCarImageById.GetCarImageByIdQuery, CleanArc.Application.Features.CarImage.Query.GetCarImageById.GetCarImageByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/CarImage")]
    //[Authorize]

    public class CarImageController : _BaseController<CreateCarImageCommand, UpdateCarImageCommand, DeleteCarImageCommand, ResponseEntity, GetAllCarImageQuery,
    List<GetAllCarImageQueryResult>, GetCarImageByIdQuery, GetCarImageByIdQueryResult>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CarImageController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public CarImageController(ISender sender, ILogger<_BaseController<CreateCarImageCommand, UpdateCarImageCommand, DeleteCarImageCommand, ResponseEntity, GetAllCarImageQuery,
   List<GetAllCarImageQueryResult>, GetCarImageByIdQuery, GetCarImageByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) :
            base(sender, logger, httpContextAccessor)
        {

        }

    }
}

