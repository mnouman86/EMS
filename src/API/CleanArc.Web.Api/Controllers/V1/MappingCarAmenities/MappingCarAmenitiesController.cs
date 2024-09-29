using Asp.Versioning;
using CleanArc.Application.Features.MappingCarAmenity.Command.CreateMappingCarAmenityCommand;
using CleanArc.Application.Features.MappingCarAmenity.Command.UpdateMappingCarAmenityCommand;
using CleanArc.Application.Features.MappingCarAmenity.Queries.GetAllMappingCarAmenity;
using CleanArc.Application.Features.MappingCarAmenity.Queries.GetMappingCarAmenityByID;
using CleanArc.Application.FeaturesppingCarAmenity.Command.DeleteMappingCarAmenityCommand;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;
namespace CleanArc.Web.Api.Controllers.V1.MappingCarAmenities
{
	/// <summary>
	/// MappingCarAmenitiesController is responsible for handling HTTP requests related to Car amenity mapping operations
	/// such as creating, updating, deleting, and retrieving mappings between Cars and amenities. It extends from a base controller
	/// which provides common functionality for CRUD operations.
	/// 
	/// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
	/// endpoint definitions for the following actions:
	/// 
	/// 1. CreateMappingCarAmenityCommand: Handles the creation of new Car-amenity mappings.
	/// 2. UpdateMappingCarAmenityCommand: Handles the updating of existing Car-amenity mappings.
	/// 3. DeleteMappingCarAmenityCommand: Handles the deletion of Car-amenity mappings.
	/// 4. GetAllMappingCarAmenityQuery: Retrieves all Car-amenity mappings.
	/// 5. GetMappingCarAmenityByIDQuery: Retrieves a specific Car-amenity mapping by its ID.
	/// 
	/// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
	/// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
	/// 
	/// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
	/// version, specified as "api/v{version:apiVersion}/MappingCarAmenities".
	/// 
	/// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
	/// operation result handling and user ID setting.
	/// 
	/// The controller is part of the CleanArc architecture, ensuring a clean separation of concerns and adherence to 
	/// SOLID principles.
	/// </summary>
	/// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.MappingCarAmenity.Command.CreateMappingCarAmenityCommand.CreateMappingCarAmenityCommand, CleanArc.Application.Features.MappingCarAmenity.Command.UpdateMappingCarAmenityCommand.UpdateMappingCarAmenityCommand, CleanArc.Application.FeaturesppingCarAmenity.Command.DeleteMappingCarAmenityCommand.DeleteMappingCarAmenityCommand, System.ResponseEntity, CleanArc.Application.Features.MappingCarAmenity.Queries.GetAllMappingCarAmenity.GetAllMappingCarAmenityQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.MappingCarAmenity.Queries.GetAllMappingCarAmenity.GetAllMappingCarAmenityQueryResult&gt;, CleanArc.Application.Features.MappingCarAmenity.Queries.GetMappingCarAmenityByID.GetMappingCarAmenityByIDQuery, CleanArc.Application.Features.MappingCarAmenity.Queries.GetMappingCarAmenityByID.GetMappingCarAmenityByIDQueryResult&gt;" />
	[ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/MappingCarAmenities")]
    public class MappingCarAmenitiesController : _BaseController<CreateMappingCarAmenityCommand, UpdateMappingCarAmenityCommand, DeleteMappingCarAmenityCommand, ResponseEntity, GetAllMappingCarAmenityQuery,
    List<GetAllMappingCarAmenityQueryResult>, GetMappingCarAmenityByIDQuery, GetMappingCarAmenityByIDQueryResult>
    {

		/// <summary>
		/// Initializes a new instance of the <see cref="MappingCarAmenitiesController"/> class.
		/// </summary>
		/// <param name="sender">The mediator sender for handling requests and responses.</param>
		/// <param name="logger">The logger for logging controller-related information.</param>
		/// <param name="httpContextAccessor"></param>
		public MappingCarAmenitiesController(ISender sender, ILogger<_BaseController<CreateMappingCarAmenityCommand, UpdateMappingCarAmenityCommand, DeleteMappingCarAmenityCommand, ResponseEntity, GetAllMappingCarAmenityQuery,
		List<GetAllMappingCarAmenityQueryResult>, GetMappingCarAmenityByIDQuery, GetMappingCarAmenityByIDQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}


