using Asp.Versioning;
using CleanArc.Application.Features.MappingHotelLanguage.Command.CreateMappingHotelLanguageCommand;
using CleanArc.Application.Features.MappingHotelLanguage.Command.DeleteMappingHotelLanguageCommand;
using CleanArc.Application.Features.MappingHotelLanguage.Command.UpdateMappingHotelLanguageCommand;
using CleanArc.Application.Features.MappingHotelLanguage.Queries.GetAllMappingHotelLanguage;
using CleanArc.Application.Features.MappingHotelLanguage.Queries.GetMappingHotelLanguageById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.MappingHotelLanguage;
/// <summary>
/// MappingHotelLanguageController is responsible for managing HTTP requests related to hotel-language mapping operations
/// such as creating, updating, deleting, and retrieving mappings between hotels and languages. It extends from a base controller
/// which provides common functionality for CRUD operations.
/// 
/// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
/// endpoint definitions for the following actions:
/// 
/// 1. CreateMappingHotelLanguageCommand: Handles the creation of new mappings between hotels and languages.
/// 2. UpdateMappingHotelLanguageCommand: Handles the updating of existing mappings between hotels and languages.
/// 3. DeleteMappingHotelLanguageCommand: Handles the deletion of mappings between hotels and languages.
/// 4. GetAllMappingHotelLanguageQuery: Retrieves a list of all mappings between hotels and languages.
/// 5. GetMappingHotelLanguageByIdQuery: Retrieves a specific mapping between a hotel and language by its ID.
/// 
/// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
/// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
/// 
/// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
/// version, specified as "api/v{version:apiVersion}/MappingHotelLanguage".
/// 
/// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
/// operation result handling and user ID setting.
/// 
/// The controller adheres to Clean Architecture principles, ensuring a clean separation of concerns and adherence to 
/// SOLID principles.
/// </summary>
/// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.MappingHotelLanguage.Command.CreateMappingHotelLanguageCommand.CreateMappingHotelLanguageCommand, CleanArc.Application.Features.MappingHotelLanguage.Command.UpdateMappingHotelLanguageCommand.UpdateMappingHotelLanguageCommand, CleanArc.Application.Features.MappingHotelLanguage.Command.DeleteMappingHotelLanguageCommand.DeleteMappingHotelLanguageCommand, System.Boolean, CleanArc.Application.Features.MappingHotelLanguage.Queries.GetAllMappingHotelLanguage.GetAllMappingHotelLanguageQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.MappingHotelLanguage.Queries.GetAllMappingHotelLanguage.GetAllMappingHotelLanguageQueryResult&gt;, CleanArc.Application.Features.MappingHotelLanguage.Queries.GetMappingHotelLanguageById.GetMappingHotelLanguageByIdQuery, CleanArc.Application.Features.MappingHotelLanguage.Queries.GetMappingHotelLanguageById.GetMappingHotelLanguageByIdQueryResult&gt;" />
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/MappingHotelLanguage")]
public class MappingHotelLanguageController : _BaseController<CreateMappingHotelLanguageCommand, UpdateMappingHotelLanguageCommand, DeleteMappingHotelLanguageCommand, bool, GetAllMappingHotelLanguageQuery,
    List<GetAllMappingHotelLanguageQueryResult>, GetMappingHotelLanguageByIdQuery, GetMappingHotelLanguageByIdQueryResult>
{

    /// <summary>
    /// Initializes a new instance of the <see cref="MappingHotelLanguageController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public MappingHotelLanguageController(ISender sender, ILogger<_BaseController<CreateMappingHotelLanguageCommand, UpdateMappingHotelLanguageCommand, DeleteMappingHotelLanguageCommand, bool, GetAllMappingHotelLanguageQuery,
List<GetAllMappingHotelLanguageQueryResult>, GetMappingHotelLanguageByIdQuery, GetMappingHotelLanguageByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}

