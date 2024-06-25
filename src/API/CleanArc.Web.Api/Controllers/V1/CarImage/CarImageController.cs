using Asp.Versioning;
using CleanArc.Application.Features.CarImage.Command.CreateCarImageCommand;
using CleanArc.Application.Features.CarImage.Command.DeleteCarImageCommand;
using CleanArc.Application.Features.CarImage.Command.UpdateCarImageCommand;
using CleanArc.Application.Features.CarImage.Query.GetAllCarImage;
using CleanArc.Application.Features.CarImage.Query.GetCarImageById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.CarImage
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/CarImage")]
    //[Authorize]

    public class CarImageController : _BaseController<CreateCarImageCommand, UpdateCarImageCommand, DeleteCarImageCommand, bool, GetAllCarImageQuery,
    List<GetAllCarImageQueryResult>, GetCarImageByIdQuery, GetCarImageByIdQueryResult>
    {
        
        public CarImageController(ISender sender, ILogger<_BaseController<CreateCarImageCommand, UpdateCarImageCommand, DeleteCarImageCommand, bool, GetAllCarImageQuery,
   List<GetAllCarImageQueryResult>, GetCarImageByIdQuery, GetCarImageByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : 
            base(sender, logger, httpContextAccessor)
        {

        }

    }
}

