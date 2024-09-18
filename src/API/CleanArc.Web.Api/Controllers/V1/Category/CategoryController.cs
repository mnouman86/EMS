using Asp.Versioning;
using CleanArc.Application.Features.Category.Command.CreateCategoryCommand;
using CleanArc.Application.Features.Category.Command.DeleteCategoryCommand;
using CleanArc.Application.Features.Category.Command.UpdateCategoryCommand;
using CleanArc.Application.Features.Category.Queries.GetAllCategories;
using CleanArc.Application.Features.Category.Queries.GetCategoryById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.Category
{
    /// <summary>
    /// CategoryController is responsible for handling HTTP requests related to category operations
    /// such as creating, updating, deleting, and retrieving categories. It extends from a base controller
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateCategory: Handles the creation of a new category.
    /// 2. UpdateCategory: Handles the updating of an existing category.
    /// 3. DeleteCategory: Handles the deletion of an existing category.
    /// 4. GetAllCategories: Retrieves all categories.
    /// 5. GetCategoryById: Retrieves a specific category by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/Category".
    /// 
    /// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
    /// operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateCategory")]
    /// public async Task<IActionResult> CreateCategory(CreateCategoryCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.Category.Command.CreateCategoryCommand.CreateCategoryCommand, CleanArc.Application.Features.Category.Command.UpdateCategoryCommand.UpdateCategoryCommand, CleanArc.Application.Features.Category.Command.DeleteCategoryCommand.DeleteCategoryCommand, System.ResponseEntity, CleanArc.Application.Features.Category.Queries.GetAllCategories.GetAllCategoriesQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.Category.Queries.GetAllCategories.GetAllCategoriesQueryResult&gt;, CleanArc.Application.Features.Category.Queries.GetCategoryById.GetCategoryByIdQuery, CleanArc.Application.Features.Category.Queries.GetCategoryById.GetCategoryByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/Category")]
    //[Authorize]
    public class CategoryController : _BaseController<CreateCategoryCommand, UpdateCategoryCommand, DeleteCategoryCommand, ResponseEntity, GetAllCategoriesQuery,
    List<GetAllCategoriesQueryResult>, GetCategoryByIdQuery, GetCategoryByIdQueryResult>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public CategoryController(ISender sender, ILogger<_BaseController<CreateCategoryCommand, UpdateCategoryCommand, DeleteCategoryCommand, ResponseEntity, GetAllCategoriesQuery,
   List<GetAllCategoriesQueryResult>, GetCategoryByIdQuery, GetCategoryByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}

