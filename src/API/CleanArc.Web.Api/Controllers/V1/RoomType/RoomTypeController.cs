using Asp.Versioning;
using CleanArc.Application.Features.RoomType.Command.CreateRoomTypeCommand;
using CleanArc.Application.Features.RoomType.Command.DeleteRoomTypeCommand;
using CleanArc.Application.Features.RoomType.Command.UpdateRoomTypeCommand;
using CleanArc.Application.Features.RoomType.Queries.GetAllRoomTypes;
using CleanArc.Application.Features.RoomType.Queries.GetRoomTypeById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; 
using CleanArc.Domain.Common;
using CleanArc.Application.Features.RoomType.Queries.GetRoomTypeByHotelId;
namespace CleanArc.Web.Api.Controllers.V1.RoomType
{
	/// <summary>
	/// RoomTypeController is responsible for handling HTTP requests related to room type operations
	/// such as creating, updating, deleting, and retrieving room types. It extends from a base controller
	/// which provides common functionality for CRUD operations.
	/// 
	/// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
	/// endpoint definitions for the following actions:
	/// 
	/// 1. CreateRoomType: Handles the creation of a new room type.
	/// 2. UpdateRoomType: Handles the updating of an existing room type.
	/// 3. DeleteRoomType: Handles the deletion of an existing room type.
	/// 4. GetAllRoomTypes: Retrieves all room types.
	/// 5. GetRoomTypeById: Retrieves a specific room type by its ID.
	/// 
	/// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
	/// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
	/// 
	/// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
	/// version, specified as "api/v{version:apiVersion}/RoomType".
	/// 
	/// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
	/// operation result handling and user ID setting.
	/// 
	/// Example Usage:
	/// 
	/// [HttpPost("CreateRoomType")]
	/// public async Task<IActionResult> CreateRoomType(CreateRoomTypeCommand model)
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
	/// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.RoomType.Command.CreateRoomTypeCommand.CreateRoomTypeCommand, CleanArc.Application.Features.RoomType.Command.UpdateRoomTypeCommand.UpdateRoomTypeCommand, CleanArc.Application.Features.RoomType.Command.DeleteRoomTypeCommand.DeleteRoomTypeCommand, System.ResponseEntity, CleanArc.Application.Features.RoomType.Queries.GetAllRoomTypes.GetAllRoomTypesQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.RoomType.Queries.GetAllRoomTypes.GetAllRoomTypesQueryResult&gt;, CleanArc.Application.Features.RoomType.Queries.GetRoomTypeById.GetRoomTypeByIdQuery, CleanArc.Application.Features.RoomType.Queries.GetRoomTypeById.GetRoomTypeByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/RoomType")]
    //[Authorize]
    public class RoomTypeController : _BaseController<CreateRoomTypeCommand, UpdateRoomTypeCommand, DeleteRoomTypeCommand, ResponseEntity, GetAllRoomTypesQuery,
    List<GetAllRoomTypesQueryResult>, GetRoomTypeByIdQuery, GetRoomTypeByIdQueryResult>
    {
		private readonly ISender _sender;

		/// <summary>
		/// Initializes a new instance of the <see cref="RoomTypeController"/> class.
		/// </summary>
		/// <param name="sender">The mediator sender for handling requests and responses.</param>
		/// <param name="logger">The logger for logging controller-related information.</param>
		/// <param name="httpContextAccessor"></param>
		public RoomTypeController(ISender sender, ILogger<_BaseController<CreateRoomTypeCommand, UpdateRoomTypeCommand, DeleteRoomTypeCommand, ResponseEntity, GetAllRoomTypesQuery,
   List<GetAllRoomTypesQueryResult>, GetRoomTypeByIdQuery, GetRoomTypeByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {
			_sender = sender;
		}
		[HttpGet("GetRoomTypeDetailByHotel/{id}")]
		public async Task<IActionResult> GetRoomTypeDetailByHotel(GetRoomTypeByHotelIdQuery query)
		{
			//GetActivityCheckoutDetailQuery query = new GetActivityCheckoutDetailQuery { searchRequestById = searchRequestById,UserId=userid };
			var result = await _sender.Send(query);

			return base.OperationResult(result);
		}

	}
}
