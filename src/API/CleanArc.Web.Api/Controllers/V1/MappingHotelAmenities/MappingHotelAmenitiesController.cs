using Asp.Versioning;
using CleanArc.Application.Features.MappingHotelAmenity.Command.CreateMappingHotelAmenityCommand;
using CleanArc.Application.Features.MappingHotelAmenity.Command.UpdateMappingHotelAmenityCommand;
using CleanArc.Application.Features.MappingHotelAmenity.Queries.GetAllMappingHotelAmenity;
using CleanArc.Application.Features.MappingHotelAmenity.Queries.GetMappingHotelAmenityByID;
using CleanArc.Application.FeaturesppingHotelAmenity.Command.DeleteMappingHotelAmenityCommand;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.MappingHotelAmenities
{
    /// <summary>
    /// MappingHotelAmenitiesController is responsible for handling HTTP requests related to hotel amenity mapping operations
    /// such as creating, updating, deleting, and retrieving mappings between hotels and amenities. It extends from a base controller
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateMappingHotelAmenityCommand: Handles the creation of new hotel-amenity mappings.
    /// 2. UpdateMappingHotelAmenityCommand: Handles the updating of existing hotel-amenity mappings.
    /// 3. DeleteMappingHotelAmenityCommand: Handles the deletion of hotel-amenity mappings.
    /// 4. GetAllMappingHotelAmenityQuery: Retrieves all hotel-amenity mappings.
    /// 5. GetMappingHotelAmenityByIDQuery: Retrieves a specific hotel-amenity mapping by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/MappingHotelAmenities".
    /// 
    /// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
    /// operation result handling and user ID setting.
    /// 
    /// The controller is part of the CleanArc architecture, ensuring a clean separation of concerns and adherence to 
    /// SOLID principles.
    /// </summary>
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.MappingHotelAmenity.Command.CreateMappingHotelAmenityCommand.CreateMappingHotelAmenityCommand, CleanArc.Application.Features.MappingHotelAmenity.Command.UpdateMappingHotelAmenityCommand.UpdateMappingHotelAmenityCommand, CleanArc.Application.FeaturesppingHotelAmenity.Command.DeleteMappingHotelAmenityCommand.DeleteMappingHotelAmenityCommand, System.Boolean, CleanArc.Application.Features.MappingHotelAmenity.Queries.GetAllMappingHotelAmenity.GetAllMappingHotelAmenityQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.MappingHotelAmenity.Queries.GetAllMappingHotelAmenity.GetAllMappingHotelAmenityQueryResult&gt;, CleanArc.Application.Features.MappingHotelAmenity.Queries.GetMappingHotelAmenityByID.GetMappingHotelAmenityByIDQuery, CleanArc.Application.Features.MappingHotelAmenity.Queries.GetMappingHotelAmenityByID.GetMappingHotelAmenityByIDQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/MappingHotelAmenities")]
    public class MappingHotelAmenitiesController : _BaseController<CreateMappingHotelAmenityCommand, UpdateMappingHotelAmenityCommand, DeleteMappingHotelAmenityCommand, bool, GetAllMappingHotelAmenityQuery,
    List<GetAllMappingHotelAmenityQueryResult>, GetMappingHotelAmenityByIDQuery, GetMappingHotelAmenityByIDQueryResult>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingHotelAmenitiesController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public MappingHotelAmenitiesController(ISender sender, ILogger<_BaseController<CreateMappingHotelAmenityCommand, UpdateMappingHotelAmenityCommand, DeleteMappingHotelAmenityCommand, bool, GetAllMappingHotelAmenityQuery,
   List<GetAllMappingHotelAmenityQueryResult>, GetMappingHotelAmenityByIDQuery, GetMappingHotelAmenityByIDQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}


