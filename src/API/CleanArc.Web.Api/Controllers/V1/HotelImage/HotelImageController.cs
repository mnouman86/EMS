using Asp.Versioning;
using CleanArc.Application.Features.HotelImage.Command.CreateHotelImageCommand;
using CleanArc.Application.Features.HotelImage.Command.DeleteHotelImageCommand;
using CleanArc.Application.Features.HotelImage.Command.UpdateHotelImageCommand;
using CleanArc.Application.Features.HotelImage.Query.GetAllHotelImage;
using CleanArc.Application.Features.HotelImage.Query.GetHotelImageById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.HotelImage
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/HotelImage")]
    //[Authorize]

    public class HotelImageController : _BaseController<CreateHotelImageCommand, UpdateHotelImageCommand, DeleteHotelImageCommand, bool, GetAllHotelImageQuery,
    List<GetAllHotelImageQueryResult>, GetHotelImageByIdQuery, GetHotelImageByIdQueryResult>
    {
        
        public HotelImageController(ISender sender, ILogger<_BaseController<CreateHotelImageCommand, UpdateHotelImageCommand, DeleteHotelImageCommand, bool, GetAllHotelImageQuery,
   List<GetAllHotelImageQueryResult>, GetHotelImageByIdQuery, GetHotelImageByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : 
            base(sender, logger, httpContextAccessor)
        {

        }

    }
}

