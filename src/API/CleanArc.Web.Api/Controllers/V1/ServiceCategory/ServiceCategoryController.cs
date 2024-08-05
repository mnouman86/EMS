using Asp.Versioning;
using CleanArc.Application.Features.ServiceCategory.Command.CreateServiceCategoryCommand;
using CleanArc.Application.Features.ServiceCategory.Command.DeleteServiceCategoryCommand;
using CleanArc.Application.Features.ServiceCategory.Command.UpdateServiceCategoryCommand;
using CleanArc.Application.Features.ServiceCategory.Queries.GetAllServiceCategories;
using CleanArc.Application.Features.ServiceCategory.Queries.GetServiceCategoryById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.ServiceCategory
{
    /// <summary>
    /// ServiceCategoryController is responsible for handling HTTP requests related to service categories operations
    /// such as creating, updating, deleting, and retrieving service categories. It extends from a base controller
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateServiceCategory: Handles the creation of a new service category.
    /// 2. UpdateServiceCategory: Handles the updating of an existing service category.
    /// 3. DeleteServiceCategory: Handles the deletion of an existing service category.
    /// 4. GetAllServiceCategories: Retrieves all service categories.
    /// 5. GetServiceCategoryById: Retrieves a specific service category by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/ServiceCategory".
    /// 
    /// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
    /// operation result handling and user ID setting.
    /// 
    /// The controller is part of the CleanArc architecture, ensuring a clean separation of concerns and adherence to 
    /// SOLID principles.
    /// </summary>
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.ServiceCategory.Command.CreateServiceCategoryCommand.CreateServiceCategoryCommand, CleanArc.Application.Features.ServiceCategory.Command.UpdateServiceCategoryCommand.UpdateServiceCategoryCommand, CleanArc.Application.Features.ServiceCategory.Command.DeleteServiceCategoryCommand.DeleteServiceCategoryCommand, System.Boolean, CleanArc.Application.Features.ServiceCategory.Queries.GetAllServiceCategories.GetAllServiceCategoriesQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.ServiceCategory.Queries.GetAllServiceCategories.GetAllServiceCategoriesQueryResult&gt;, CleanArc.Application.Features.ServiceCategory.Queries.GetServiceCategoryById.GetServiceCategoryByIdQuery, CleanArc.Application.Features.ServiceCategory.Queries.GetServiceCategoryById.GetServiceCategoryByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/ServiceCategory")]
    //[Authorize]
    public class ServiceCategoryController : _BaseController<CreateServiceCategoryCommand, UpdateServiceCategoryCommand, DeleteServiceCategoryCommand, bool, GetAllServiceCategoriesQuery,
    List<GetAllServiceCategoriesQueryResult>, GetServiceCategoryByIdQuery, GetServiceCategoryByIdQueryResult>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceCategoryController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public ServiceCategoryController(ISender sender, ILogger<_BaseController<CreateServiceCategoryCommand, UpdateServiceCategoryCommand, DeleteServiceCategoryCommand, bool, GetAllServiceCategoriesQuery,
   List<GetAllServiceCategoriesQueryResult>, GetServiceCategoryByIdQuery, GetServiceCategoryByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}

