using Asp.Versioning;
using CleanArc.Application.Features.FilterCategory.Command.CreateFilterCategoryCommand;
using CleanArc.Application.Features.FilterCategory.Command.DeleteFilterCategoryCommand;
using CleanArc.Application.Features.FilterCategory.Command.UpdateFilterCategoryCommand;
using CleanArc.Application.Features.FilterCategory.Queries.GetAllFilterCategory;
using CleanArc.Application.Features.FilterCategory.Queries.GetFilterCategoryById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; 
using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.FilterCategory;

[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/FilterCategory")]
public class FilterCategoryController : _BaseController<CreateFilterCategoryCommand, UpdateFilterCategoryCommand, DeleteFilterCategoryCommand, ResponseEntity, GetAllFilterCategoryQuery,
    List<GetAllFilterCategoryQueryResult>, GetFilterCategoryByIdQuery, GetFilterCategoryByIdQueryResult>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FilterCategoryController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public FilterCategoryController(ISender sender, ILogger<_BaseController<CreateFilterCategoryCommand, UpdateFilterCategoryCommand, DeleteFilterCategoryCommand, ResponseEntity, GetAllFilterCategoryQuery,
List<GetAllFilterCategoryQueryResult>, GetFilterCategoryByIdQuery, GetFilterCategoryByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}

