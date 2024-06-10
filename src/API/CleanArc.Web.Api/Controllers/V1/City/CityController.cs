using Asp.Versioning;

using CleanArc.Application.Features.City.Command.CreateCityCommand;
using CleanArc.Application.Features.City.Command.DeleteCityCommand;
using CleanArc.Application.Features.City.Command.UpdateCityCommand;
using CleanArc.Application.Features.City.Queries.GetAllCities;
using CleanArc.Application.Features.City.Queries.GetCityById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.City
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/City")]
    public class CityController : _BaseController<CreateCityCommand, UpdateCityCommand, DeleteCityCommand, bool, GetAllCitiesQuery,
    List<GetAllCitiesQueryResult>, GetCityByIdQuery, GetCityByIdQueryResult>
    {
       
        public CityController(ISender sender, ILogger<_BaseController<CreateCityCommand, UpdateCityCommand, DeleteCityCommand, bool, GetAllCitiesQuery,
   List<GetAllCitiesQueryResult>, GetCityByIdQuery, GetCityByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
