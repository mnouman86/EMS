using Asp.Versioning;
using CleanArc.Application.Features.AmenityMapping.Command.CreateAmenityMappingCommand;
using CleanArc.Application.Features.AmenityMapping.Command.UpdateAmenityMappingCommand;
using CleanArc.Application.Features.AmenityMapping.Queries.GetAllAmenityMapping;
using CleanArc.Application.Features.AmenityMapping.Queries.GetAmenityMappingByID;
using CleanArc.Application.AmenityMapping.Command.DeleteAmenityMappingCommand;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.AmenityMapping
{
    /// <summary>
    /// MappingHotelAmenitiesController is responsible for handling HTTP requests related to hotel amenity mapping operations
    /// such as creating, updating, deleting, and retrieving mappings between hotels and amenities. It extends from a base controller
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateAmenityMappingCommand: Handles the creation of new hotel-amenity mappings.
    /// 2. UpdateAmenityMappingCommand: Handles the updating of existing hotel-amenity mappings.
    /// 3. DeleteAmenityMappingCommand: Handles the deletion of hotel-amenity mappings.
    /// 4. GetAllAmenityMappingQuery: Retrieves all hotel-amenity mappings.
    /// 5. GetAmenityMappingByIDQuery: Retrieves a specific hotel-amenity mapping by its ID.
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.AmenityMapping.Command.CreateAmenityMappingCommand.CreateAmenityMappingCommand, CleanArc.Application.Features.AmenityMapping.Command.UpdateAmenityMappingCommand.UpdateAmenityMappingCommand, CleanArc.Application.AmenityMapping.Command.DeleteAmenityMappingCommand.DeleteAmenityMappingCommand, System.ResponseEntity, CleanArc.Application.Features.AmenityMapping.Queries.GetAllAmenityMapping.GetAllAmenityMappingQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.AmenityMapping.Queries.GetAllAmenityMapping.GetAllAmenityMappingQueryResult&gt;, CleanArc.Application.Features.AmenityMapping.Queries.GetAmenityMappingByID.GetAmenityMappingByIDQuery, CleanArc.Application.Features.AmenityMapping.Queries.GetAmenityMappingByID.GetAmenityMappingByIDQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/AmenityMapping")]
    public class AmenityMappingController : _BaseController<CreateAmenityMappingCommand, UpdateAmenityMappingCommand, DeleteAmenityMappingCommand, ResponseEntity, GetAllAmenityMappingQuery,
    List<GetAllAmenityMappingQueryResult>, GetAmenityMappingByIDQuery, GetAmenityMappingByIDQueryResult>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="AmenityMappingController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public AmenityMappingController(ISender sender, ILogger<_BaseController<CreateAmenityMappingCommand, UpdateAmenityMappingCommand, DeleteAmenityMappingCommand, ResponseEntity, GetAllAmenityMappingQuery,
   List<GetAllAmenityMappingQueryResult>, GetAmenityMappingByIDQuery, GetAmenityMappingByIDQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}


