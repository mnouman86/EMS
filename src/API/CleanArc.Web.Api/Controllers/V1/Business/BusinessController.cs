using Asp.Versioning;
using CleanArc.Application.Features.Business.Command.CreateBusinessCommand;
using CleanArc.Application.Features.Business.Command.DeleteBusinessCommand;
using CleanArc.Application.Features.Business.Command.UpdateBusinessCommand;
using CleanArc.Application.Features.Business.Query.GetAllBusiness;
using CleanArc.Application.Features.Business.Query.GetBusinessById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.Business;

/// <summary>
/// BusinessController is responsible for handling HTTP requests related to business operations
/// such as creating, updating, deleting, and retrieving business information. It extends from a 
/// base controller which provides common functionality for CRUD operations.
/// 
/// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
/// endpoint definitions for the following actions:
/// 
/// 1. CreateBusiness: Handles the creation of a new business.
/// 2. UpdateBusiness: Handles the updating of an existing business.
/// 3. DeleteBusiness: Handles the deletion of an existing business.
/// 4. GetAllBusiness: Retrieves all businesses.
/// 5. GetBusinessById: Retrieves a specific business by its ID.
/// 
/// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
/// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
/// 
/// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
/// version, specified as "api/v{version:apiVersion}/Business".
/// 
/// Note: The actual endpoint methods are intended to be implemented to utilize the base controller's 
/// operation result handling and user ID setting.
/// 
/// Example Usage:
/// 
/// [HttpPost("CreateBusiness")]
/// public async Task<IActionResult> CreateBusiness(CreateBusinessCommand model)
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
/// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.Business.Command.CreateBusinessCommand.CreateBusinessCommand, CleanArc.Application.Features.Business.Command.UpdateBusinessCommand.UpdateBusinessCommand, CleanArc.Application.Features.Business.Command.DeleteBusinessCommand.DeleteBusinessCommand, System.ResponseEntity, CleanArc.Application.Features.Business.Query.GetAllBusiness.GetAllBusinessQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.Business.Query.GetAllBusiness.GetAllBusinessQueryResult&gt;, CleanArc.Application.Features.Business.Query.GetBusinessById.GetBusinessByIdQuery, CleanArc.Application.Features.Business.Query.GetBusinessById.GetBusinessByIdQueryResult&gt;" />
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/Business")]
public class BusinessController : _BaseController<CreateBusinessCommand, UpdateBusinessCommand, DeleteBusinessCommand, ResponseEntity, GetAllBusinessQuery,
List<GetAllBusinessQueryResult>, GetBusinessByIdQuery, GetBusinessByIdQueryResult>
{

    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public BusinessController(ISender sender, ILogger<_BaseController<CreateBusinessCommand, UpdateBusinessCommand, DeleteBusinessCommand, ResponseEntity, GetAllBusinessQuery,
List<GetAllBusinessQueryResult>, GetBusinessByIdQuery, GetBusinessByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}


