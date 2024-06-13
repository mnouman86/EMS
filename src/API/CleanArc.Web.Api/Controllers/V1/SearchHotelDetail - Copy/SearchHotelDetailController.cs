using Asp.Versioning;
using CleanArc.Application.Features.SearchBusinessDetail.Commands.CreateSearchBusinessDetailCommand;
using CleanArc.Application.Features.SearchBusinessDetail.Commands.DeleteSearchBusinessDetailCommand;
using CleanArc.Application.Features.SearchBusinessDetail.Commands.UpdateSearchBusinessDetailCommand;
using CleanArc.Application.Features.SearchBusinessDetail.Queries.GetAllSearchBusinessDetails;
using CleanArc.Application.Features.SearchBusinessDetail.Queries.GetSearchBusinessDetailById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.SearchBusinessDetailDetail;

[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/SearchBusinessDetailDetail")]
//[Authorize]
public class SearchBusinessDetailDetailController : _BaseController<CreateSearchBusinessDetailCommand, UpdateSearchBusinessDetailCommand, DeleteSearchBusinessDetailCommand, bool, GetAllSearchBusinessDetailsQuery,
List<GetAllSearchBusinessDetailsQueryResult>, GetSearchBusinessDetailByIdQuery, GetSearchBusinessDetailByIdQueryResult>
{

    public SearchBusinessDetailDetailController(ISender sender, ILogger<_BaseController<CreateSearchBusinessDetailCommand, UpdateSearchBusinessDetailCommand, DeleteSearchBusinessDetailCommand, bool, GetAllSearchBusinessDetailsQuery,
List<GetAllSearchBusinessDetailsQueryResult>, GetSearchBusinessDetailByIdQuery, GetSearchBusinessDetailByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }
}
