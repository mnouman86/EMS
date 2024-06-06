using Asp.Versioning;
using CleanArc.Application.Features.AgeType.Commands.CreateAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Commands.DeleteAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Commands.UpdateAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
using CleanArc.Application.Features.AgeType.Queries.GetAllAgeType;
using CleanArc.Application.Features.Category.Command.CreateCategoryCommand;
using CleanArc.Application.Features.Category.Command.DeleteCategoryCommand;
using CleanArc.Application.Features.Category.Command.UpdateCategoryCommand;
using CleanArc.Application.Features.Category.Queries.GetAllCategories;
using CleanArc.Application.Features.Category.Queries.GetCategoryById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.Category
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/Category")]
    //[Authorize]
    public class CategoryController : _BaseController<CreateCategoryCommand, UpdateCategoryCommand, DeleteCategoryCommand, bool, GetAllCategoriesQuery,
    List<GetAllCategoriesQueryResult>, GetCategoryByIdQuery, GetCategoryByIdQueryResult>
    {
       
        public CategoryController(ISender sender, ILogger<_BaseController<CreateCategoryCommand, UpdateCategoryCommand, DeleteCategoryCommand, bool, GetAllCategoriesQuery,
   List<GetAllCategoriesQueryResult>, GetCategoryByIdQuery, GetCategoryByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}

