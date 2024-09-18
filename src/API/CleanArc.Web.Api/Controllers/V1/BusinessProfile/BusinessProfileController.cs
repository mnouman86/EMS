using Asp.Versioning;
using CleanArc.Application.Features.BusinessProfile.Command.CreateBusinessProfileCommand;
using CleanArc.Application.Features.BusinessProfile.Command.DeleteBusinessProfileCommand;
using CleanArc.Application.Features.BusinessProfile.Command.UpdateBusinessProfileCommand;
using CleanArc.Application.Features.BusinessProfile.Query.GetAllBusinessProfile;
using CleanArc.Application.Features.BusinessProfile.Query.GetBusinessProfileById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.BusinessProfile;

/// <summary>
/// BusinessProfileController is responsible for handling HTTP requests related to BusinessProfile operations
/// such as creating, updating, deleting, and retrieving BusinessProfile information. It extends from a 
/// base controller which provides common functionality for CRUD operations.
/// 
/// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
/// endpoint definitions for the following actions:
/// 
/// 1. CreateBusinessProfile: Handles the creation of a new BusinessProfile.
/// 2. UpdateBusinessProfile: Handles the updating of an existing BusinessProfile.
/// 3. DeleteBusinessProfile: Handles the deletion of an existing BusinessProfile.
/// 4. GetAllBusinessProfile: Retrieves all BusinessProfilees.
/// 5. GetBusinessProfileById: Retrieves a specific BusinessProfile by its ID.
/// 
/// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
/// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
/// 
/// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
/// version, specified as "api/v{version:apiVersion}/BusinessProfile".
/// 
/// Note: The actual endpoint methods are intended to be implemented to utilize the base controller's 
/// operation result handling and user ID setting.
/// 
/// Example Usage:
/// 
/// [HttpPost("CreateBusinessProfile")]
/// public async Task<IActionResult> CreateBusinessProfile(CreateBusinessProfileCommand model)
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
/// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.BusinessProfile.Command.CreateBusinessProfileCommand.CreateBusinessProfileCommand, CleanArc.Application.Features.BusinessProfile.Command.UpdateBusinessProfileCommand.UpdateBusinessProfileCommand, CleanArc.Application.Features.BusinessProfile.Command.DeleteBusinessProfileCommand.DeleteBusinessProfileCommand, System.ResponseEntity, CleanArc.Application.Features.BusinessProfile.Query.GetAllBusinessProfile.GetAllBusinessProfileQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.BusinessProfile.Query.GetAllBusinessProfile.GetAllBusinessProfileQueryResult&gt;, CleanArc.Application.Features.BusinessProfile.Query.GetBusinessProfileById.GetBusinessProfileByIdQuery, CleanArc.Application.Features.BusinessProfile.Query.GetBusinessProfileById.GetBusinessProfileByIdQueryResult&gt;" />
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/BusinessProfile")]
public class BusinessProfileController : _BaseController<CreateBusinessProfileCommand, UpdateBusinessProfileCommand, DeleteBusinessProfileCommand, ResponseEntity, GetAllBusinessProfileQuery,
List<GetAllBusinessProfileQueryResult>, GetBusinessProfileByIdQuery, GetBusinessProfileByIdQueryResult>
{

    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessProfileController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public BusinessProfileController(ISender sender, ILogger<_BaseController<CreateBusinessProfileCommand, UpdateBusinessProfileCommand, DeleteBusinessProfileCommand, ResponseEntity, GetAllBusinessProfileQuery,
List<GetAllBusinessProfileQueryResult>, GetBusinessProfileByIdQuery, GetBusinessProfileByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}


