using Asp.Versioning;
using CleanArc.Application.Features.Service.Command.CreateServiceCommand;
using CleanArc.Application.Features.Service.Command.DeleteServiceCommand;
using CleanArc.Application.Features.Service.Command.UpdateServiceCommand;
using CleanArc.Application.Features.Service.Queries.GetAllServices;
using CleanArc.Application.Features.Service.Queries.GetServiceById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.Service;
/// <summary>
/// ServiceController is responsible for handling HTTP requests related to service operations
/// such as creating, updating, deleting, and retrieving services. It extends from a base controller
/// which provides common functionality for CRUD operations.
/// 
/// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
/// endpoint definitions for the following actions:
/// 
/// 1. CreateService: Handles the creation of a new service.
/// 2. UpdateService: Handles the updating of an existing service.
/// 3. DeleteService: Handles the deletion of an existing service.
/// 4. GetAllServices: Retrieves all services.
/// 5. GetServiceById: Retrieves a specific service by its ID.
/// 
/// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
/// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
/// 
/// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
/// version, specified as "api/v{version:apiVersion}/Service".
/// 
/// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
/// operation result handling and user ID setting.
/// 
/// The controller is part of the CleanArc architecture, ensuring a clean separation of concerns and adherence to 
/// SOLID principles.
/// </summary>
/// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.Service.Command.CreateServiceCommand.CreateServiceCommand, CleanArc.Application.Features.Service.Command.UpdateServiceCommand.UpdateServiceCommand, CleanArc.Application.Features.Service.Command.DeleteServiceCommand.DeleteServiceCommand, System.ResponseEntity, CleanArc.Application.Features.Service.Queries.GetAllServices.GetAllServicesQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.Service.Queries.GetAllServices.GetAllServicesQueryResult&gt;, CleanArc.Application.Features.Service.Queries.GetServiceById.GetServiceByIdQuery, CleanArc.Application.Features.Service.Queries.GetServiceById.GetServiceByIdQueryResult&gt;" />
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/Service")]
//[Authorize]
public class ServiceController : _BaseController<CreateServiceCommand, UpdateServiceCommand, DeleteServiceCommand, ResponseEntity, GetAllServicesQuery,
    List<GetAllServicesQueryResult>, GetServiceByIdQuery, GetServiceByIdQueryResult>
{

    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public ServiceController(ISender sender, ILogger<_BaseController<CreateServiceCommand, UpdateServiceCommand, DeleteServiceCommand, ResponseEntity, GetAllServicesQuery,
List<GetAllServicesQueryResult>, GetServiceByIdQuery, GetServiceByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}
