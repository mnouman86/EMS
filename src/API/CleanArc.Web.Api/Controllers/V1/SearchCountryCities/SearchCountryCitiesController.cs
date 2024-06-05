using Asp.Versioning;
using CleanArc.Application.Features.SearchCountryCities.Command.CreateSearchCountryCities;
using CleanArc.Application.Features.SearchCountryCities.Command.DeleteSearchCountryCities;
using CleanArc.Application.Features.SearchCountryCities.Command.UpdateSearchCountryCities;
using CleanArc.Application.Features.SearchCountryCities.Queries.GetAllSearchCountryCities;
using CleanArc.Application.Features.SearchCountryCities.Queries.GetSearchCountryCitiesById;
using CleanArc.Application.Features.SearchHotel.Commands.CreateSearchHotelCommand;
using CleanArc.Application.Features.SearchHotel.Commands.DeleteSearchHotelCommand;
using CleanArc.Application.Features.SearchHotel.Commands.UpdateSearchHotelCommand;
using CleanArc.Application.Features.SearchHotel.Queries.GetAllSearchHotels;
using CleanArc.Application.Features.SearchHotel.Queries.GetSearchHotelById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.SearchCountryCities;

[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/SearchCountryCities")]
[Authorize]
public class SearchCountryCitiesController : _BaseController<CreateSearchCountryCitiesCommand, UpdateSearchCountryCitiesCommand, DeleteSearchCountryCitiesCommand, bool, GetAllSearchCountryCitiesQueries,
List<GetAllSearchCountryCitiesQueriesResult>, GetSearchCountryCitiesByIdQuery, GetSearchCountryCitiesByIdQueryResult>
{

    public SearchCountryCitiesController(ISender sender, ILogger<_BaseController<CreateSearchCountryCitiesCommand, UpdateSearchCountryCitiesCommand, DeleteSearchCountryCitiesCommand, bool, GetAllSearchCountryCitiesQueries,
List<GetAllSearchCountryCitiesQueriesResult>, GetSearchCountryCitiesByIdQuery, GetSearchCountryCitiesByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}

