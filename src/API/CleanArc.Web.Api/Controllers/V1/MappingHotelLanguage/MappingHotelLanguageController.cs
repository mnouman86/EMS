using Asp.Versioning;
using CleanArc.Application.Features.AgeType.Commands.CreateAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Commands.DeleteAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Commands.UpdateAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
using CleanArc.Application.Features.AgeType.Queries.GetAllAgeType;
using CleanArc.Application.Features.MappingHotelLanguage.Command.CreateMappingHotelLanguageCommand;
using CleanArc.Application.Features.MappingHotelLanguage.Command.DeleteMappingHotelLanguageCommand;
using CleanArc.Application.Features.MappingHotelLanguage.Command.UpdateMappingHotelLanguageCommand;
using CleanArc.Application.Features.MappingHotelLanguage.Queries.GetAllMappingHotelLanguage;
using CleanArc.Application.Features.MappingHotelLanguage.Queries.GetMappingHotelLanguageById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.MappingHotelLanguage;
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/MappingHotelLanguage")]
public class MappingHotelLanguageController : _BaseController<CreateMappingHotelLanguageCommand, UpdateMappingHotelLanguageCommand, DeleteMappingHotelLanguageCommand, bool, GetAllMappingHotelLanguageQuery,
    List<GetAllMappingHotelLanguageQueryResult>, GetMappingHotelLanguageByIdQuery, GetMappingHotelLanguageByIdQueryResult>
{
  
    public MappingHotelLanguageController(ISender sender, ILogger<_BaseController<CreateMappingHotelLanguageCommand, UpdateMappingHotelLanguageCommand, DeleteMappingHotelLanguageCommand, bool, GetAllMappingHotelLanguageQuery,
List<GetAllMappingHotelLanguageQueryResult>, GetMappingHotelLanguageByIdQuery, GetMappingHotelLanguageByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}

