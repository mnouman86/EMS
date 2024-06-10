using Asp.Versioning;
using CleanArc.Application.Features.AgeType.Commands.CreateAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Commands.DeleteAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Commands.UpdateAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
using CleanArc.Application.Features.AgeType.Queries.GetAllAgeType;
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
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/ServiceCategory")]
    //[Authorize]
    public class ServiceCategoryController : _BaseController<CreateServiceCategoryCommand, UpdateServiceCategoryCommand, DeleteServiceCategoryCommand, bool, GetAllServiceCategoriesQuery,
    List<GetAllServiceCategoriesQueryResult>, GetServiceCategoryByIdQuery, GetServiceCategoryByIdQueryResult>
    {
      
        public ServiceCategoryController(ISender sender, ILogger<_BaseController<CreateServiceCategoryCommand, UpdateServiceCategoryCommand, DeleteServiceCategoryCommand, bool, GetAllServiceCategoriesQuery,
   List<GetAllServiceCategoriesQueryResult>, GetServiceCategoryByIdQuery, GetServiceCategoryByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}

