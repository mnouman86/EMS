using Asp.Versioning;
using CleanArc.Application.Features.AgeType.Commands.CreateAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Commands.DeleteAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Commands.UpdateAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
using CleanArc.Application.Features.AgeType.Queries.GetAllAgeType;
using CleanArc.Application.Features.Country.Command.CreateCountryCommand;
using CleanArc.Application.Features.Country.Command.DeleteCountryCommand;
using CleanArc.Application.Features.Country.Command.UpdateCountryCommand;
using CleanArc.Application.Features.Country.Queries.GetAllCountries;
using CleanArc.Application.Features.Country.Queries.GetCountryById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.Country;

[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/Country")]
public class CountryController : _BaseController<CreateCountryCommand, UpdateCountryCommand, DeleteCountryCommand, bool, GetAllCountryQuery,
    List<GetAllCountryQueryResult>, GetCountryByIdQuery, GetCountryByIdQueryResult>
{
   
    public CountryController(ISender sender, ILogger<_BaseController<CreateCountryCommand, UpdateCountryCommand, DeleteCountryCommand, bool, GetAllCountryQuery,
List<GetAllCountryQueryResult>, GetCountryByIdQuery, GetCountryByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}


