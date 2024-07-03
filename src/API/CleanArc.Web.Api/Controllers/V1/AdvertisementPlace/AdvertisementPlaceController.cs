using Asp.Versioning;
using CleanArc.Application.Features.AdvertisementPlace.Commands.CreateAdvertisementPlaceCommand;
using CleanArc.Application.Features.AdvertisementPlace.Commands.DeleteAdvertisementPlaceCommand;
using CleanArc.Application.Features.AdvertisementPlace.Commands.UpdateAdvertisementPlaceCommand;
using CleanArc.Application.Features.AdvertisementPlace.Queries.GetAdvertisementPlaceById;
using CleanArc.Application.Features.AdvertisementPlace.Queries.GetAllAdvertisementPlace;
using CleanArc.Application.Features.Order.Commands;
using CleanArc.Application.Features.Order.Queries.GetAllOrders;
using CleanArc.Application.Features.URL.Commands.AddURLCommand;
using CleanArc.Application.Features.URL.Commands.DeleteURLCommand;
using CleanArc.Application.Features.URL.Commands.UpdateURLCommand;
using CleanArc.Application.Features.URL.Queries.GetAllURLs;
using CleanArc.Application.Features.URL.Queries.GetURLById;
using CleanArc.Application.Models.Request;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.AdvertisementPlace
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/AdvertisementPlace")]
    //[Authorize]
    public class AdvertisementPlaceController : _BaseController<CreateAdvertisementPlaceCommand, UpdateAdvertisementPlaceCommand, DeleteAdvertisementPlaceCommand, bool, GetAllAdvertisementPlaceQuery,
    List<GetAllAdvertisementPlaceQueryResult>, GetAdvertisementPlaceByIdQuery, GetAdvertisementPlaceByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public AdvertisementPlaceController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateAdvertisementPlace")]
        //public async Task<IActionResult> CreateAdvertisementPlace(CreateAdvertisementPlaceCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateAdvertisementPlace")]
        //public async Task<IActionResult> UpdateAdvertisementPlace(UpdateAdvertisementPlaceCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteAdvertisementPlace")]
        //public async Task<IActionResult> DeleteAdvertisementPlace(DeleteAdvertisementPlaceCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllAdvertisementPlace")]
        //public async Task<IActionResult> GetAllAdvertisementPlace( )
        //{
        //    var queryResult = await _sender.Send(new GetAllAdvertisementPlaceQuery());

        //    return base.OperationResult(queryResult);
        //}
        public AdvertisementPlaceController(ISender sender, ILogger<_BaseController<CreateAdvertisementPlaceCommand, UpdateAdvertisementPlaceCommand, DeleteAdvertisementPlaceCommand, bool, GetAllAdvertisementPlaceQuery,
   List<GetAllAdvertisementPlaceQueryResult>, GetAdvertisementPlaceByIdQuery, GetAdvertisementPlaceByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
