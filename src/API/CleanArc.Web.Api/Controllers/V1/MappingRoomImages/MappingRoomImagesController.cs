using Asp.Versioning;
using CleanArc.Application.Features.AgeType.Commands.CreateAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Commands.DeleteAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Commands.UpdateAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
using CleanArc.Application.Features.AgeType.Queries.GetAllAgeType;
using CleanArc.Application.Features.MappingRoomImage.Command.CreateMappingRoomImageCommand;
using CleanArc.Application.Features.MappingRoomImage.Command.DeleteMappingRoomImageCommand;
using CleanArc.Application.Features.MappingRoomImage.Command.UpdateMappingRoomImageCommand;
using CleanArc.Application.Features.MappingRoomImage.Query.GetAllMappingRoomImage;
using CleanArc.Application.Features.MappingRoomImage.Query.GetMappingRoomImageById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.MappingRoomImages;

[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/MappingRoomImages")]
public class MappingRoomImagesController : _BaseController<CreateMappingRoomImageCommand, UpdateMappingRoomImageCommand, DeleteMappingRoomImageCommand, bool, GetAllMappingRoomImageQuery,
List<GetAllMappingRoomImageQueryResult>, GetMappingRoomImageByIdQuery, GetMappingRoomImageByIdQueryResult>
{
   
    public MappingRoomImagesController(ISender sender, ILogger<_BaseController<CreateMappingRoomImageCommand, UpdateMappingRoomImageCommand, DeleteMappingRoomImageCommand, bool, GetAllMappingRoomImageQuery,
List<GetAllMappingRoomImageQueryResult>, GetMappingRoomImageByIdQuery, GetMappingRoomImageByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}

