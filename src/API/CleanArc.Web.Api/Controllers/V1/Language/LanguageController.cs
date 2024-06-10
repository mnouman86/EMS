using Asp.Versioning;
using CleanArc.Application.Features.AgeType.Commands.CreateAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Commands.DeleteAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Commands.UpdateAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
using CleanArc.Application.Features.AgeType.Queries.GetAllAgeType;
using CleanArc.Application.Features.Language.Command.CreateLanguageCommand;
using CleanArc.Application.Features.Language.Command.DeleteLanguageCommand;
using CleanArc.Application.Features.Language.Command.UpdateLanguageCommand;
using CleanArc.Application.Features.Language.Queries.GetAllLanguages;
using CleanArc.Application.Features.Language.Queries.GetLanguageById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.Language;
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/Language")]
public class LanguageController : _BaseController<CreateLanguageCommand, UpdateLanguageCommand, DeleteLanguageCommand, bool, GetAllLanguagesQuery,
    List<GetAllLanguagesQueryResult>, GetLanguageByIdQuery, GetLanguageByIdQueryResult>
{
   
    public LanguageController(ISender sender, ILogger<_BaseController<CreateLanguageCommand, UpdateLanguageCommand, DeleteLanguageCommand, bool, GetAllLanguagesQuery,
List<GetAllLanguagesQueryResult>, GetLanguageByIdQuery, GetLanguageByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}

