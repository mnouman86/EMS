using Asp.Versioning;
using CleanArc.Application.Features.Language.Command.CreateLanguageCommand;
using CleanArc.Application.Features.Language.Command.DeleteLanguageCommand;
using CleanArc.Application.Features.Language.Command.UpdateLanguageCommand;
using CleanArc.Application.Features.Language.Queries.GetAllLanguages;
using CleanArc.Application.Features.Language.Queries.GetLanguageById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.Language;
/// <summary>
/// LanguageController is responsible for handling HTTP requests related to language operations
/// such as creating, updating, deleting, and retrieving languages. It extends from a base controller
/// which provides common functionality for CRUD operations.
/// 
/// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
/// endpoint definitions for the following actions:
/// 
/// 1. CreateLanguage: Handles the creation of a new language.
/// 2. UpdateLanguage: Handles the updating of an existing language.
/// 3. DeleteLanguage: Handles the deletion of an existing language.
/// 4. GetAllLanguages: Retrieves all languages.
/// 5. GetLanguageById: Retrieves a specific language by its ID.
/// 
/// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
/// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
/// 
/// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
/// version, specified as "api/v{version:apiVersion}/Language".
/// 
/// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
/// operation result handling and user ID setting.
/// 
/// Example Usage:
/// 
/// [HttpPost("CreateLanguage")]
/// public async Task<IActionResult> CreateLanguage(CreateLanguageCommand model)
/// {
///     model.UserId = base.UserId;
///     var command 
///      = await _sender.Send(model);
///     return base.OperationResult(command);
/// }
/// 
/// This ensures that the UserId is set from the base controller before sending the command and that the operation
/// result is properly formatted for the response.
/// 
/// The controller is part of the CleanArc architecture, ensuring a clean separation of concerns and adherence to 
/// SOLID principles.
/// </summary>
/// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.Language.Command.CreateLanguageCommand.CreateLanguageCommand, CleanArc.Application.Features.Language.Command.UpdateLanguageCommand.UpdateLanguageCommand, CleanArc.Application.Features.Language.Command.DeleteLanguageCommand.DeleteLanguageCommand, System.Boolean, CleanArc.Application.Features.Language.Queries.GetAllLanguages.GetAllLanguagesQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.Language.Queries.GetAllLanguages.GetAllLanguagesQueryResult&gt;, CleanArc.Application.Features.Language.Queries.GetLanguageById.GetLanguageByIdQuery, CleanArc.Application.Features.Language.Queries.GetLanguageById.GetLanguageByIdQueryResult&gt;" />
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/Language")]
public class LanguageController : _BaseController<CreateLanguageCommand, UpdateLanguageCommand, DeleteLanguageCommand, bool, GetAllLanguagesQuery,
    List<GetAllLanguagesQueryResult>, GetLanguageByIdQuery, GetLanguageByIdQueryResult>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LanguageController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public LanguageController(ISender sender, ILogger<_BaseController<CreateLanguageCommand, UpdateLanguageCommand, DeleteLanguageCommand, bool, GetAllLanguagesQuery,
List<GetAllLanguagesQueryResult>, GetLanguageByIdQuery, GetLanguageByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}

