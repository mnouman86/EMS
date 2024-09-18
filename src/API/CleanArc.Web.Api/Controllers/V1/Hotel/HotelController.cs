using Asp.Versioning;
using CleanArc.Application.Features.Hotel.Command.CreateHotelCommand;
using CleanArc.Application.Features.Hotel.Command.DeleteHotelCommand;
using CleanArc.Application.Features.Hotel.Command.UpdateHotelCommand;
using CleanArc.Application.Features.Hotel.Queries.GetAllHotels;
using CleanArc.Application.Features.Hotel.Queries.GetHotelById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.Hotel
{
    /// <summary>
    /// HotelController is responsible for handling HTTP requests related to hotel operations
    /// such as creating, updating, deleting, and retrieving hotels. It extends from a base controller
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateHotel: Handles the creation of a new hotel.
    /// 2. UpdateHotel: Handles the updating of an existing hotel.
    /// 3. DeleteHotel: Handles the deletion of an existing hotel.
    /// 4. GetAllHotels: Retrieves all hotels.
    /// 5. GetHotelById: Retrieves a specific hotel by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/Hotel".
    /// 
    /// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
    /// operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateHotel")]
    /// public async Task<IActionResult> CreateHotel(CreateHotelCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.Hotel.Command.CreateHotelCommand.CreateHotelCommand, CleanArc.Application.Features.Hotel.Command.UpdateHotelCommand.UpdateHotelCommand, CleanArc.Application.Features.Hotel.Command.DeleteHotelCommand.DeleteHotelCommand, System.ResponseEntity, CleanArc.Application.Features.Hotel.Queries.GetAllHotels.GetAllHotelsQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.Hotel.Queries.GetAllHotels.GetAllHotelsQueryResult&gt;, CleanArc.Application.Features.Hotel.Queries.GetHotelById.GetHotelByIdQuery, CleanArc.Application.Features.Hotel.Queries.GetHotelById.GetHotelByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/Hotel")]
    //[Authorize]

    public class HotelController : _BaseController<CreateHotelCommand, UpdateHotelCommand, DeleteHotelCommand, ResponseEntity, GetAllHotelsQuery,
    List<GetAllHotelsQueryResult>, GetHotelByIdQuery, GetHotelByIdQueryResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HotelController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public HotelController(ISender sender, ILogger<_BaseController<CreateHotelCommand, UpdateHotelCommand, DeleteHotelCommand, ResponseEntity, GetAllHotelsQuery,
   List<GetAllHotelsQueryResult>, GetHotelByIdQuery, GetHotelByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)


        {

        }

    }

}
