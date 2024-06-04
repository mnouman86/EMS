using Asp.Versioning;
//using CleanArc.Application.Features.AgeType.Commands.CreateAgeTypeCommand;
//using CleanArc.Application.Features.AgeType.Commands.DeleteAgeTypeCommand;
//using CleanArc.Application.Features.AgeType.Commands.UpdateAgeTypeCommand;
//using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
//using CleanArc.Application.Features.AgeType.Queries.GetAllAgeType;
using CleanArc.Application.Features.Hotel.Command.CreateHotelCommand;
using CleanArc.Application.Features.Hotel.Command.DeleteHotelCommand;
using CleanArc.Application.Features.Hotel.Command.UpdateHotelCommand;
using CleanArc.Application.Features.Hotel.Queries.GetAllHotels;
using CleanArc.Application.Features.Hotel.Queries.GetHotelById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CleanArc.Application.Models.Request;

namespace CleanArc.Web.Api.Controllers.V1.Hotel
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/Hotel")]
    [Authorize]

    public class HotelController : _BaseController<CreateHotelCommand, UpdateHotelCommand, DeleteHotelCommand, bool, GetAllHotelsQuery,
    List<GetAllHotelsQueryResult>, GetHotelByIdQuery, GetHotelByIdQueryResult>
    {

        public HotelController(ISender sender, ILogger<_BaseController<CreateHotelCommand, UpdateHotelCommand, DeleteHotelCommand, bool, GetAllHotelsQuery,
   List<GetAllHotelsQueryResult>, GetHotelByIdQuery, GetHotelByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }

}
