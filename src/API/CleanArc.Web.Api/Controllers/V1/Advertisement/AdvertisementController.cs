using Asp.Versioning;
using CleanArc.Application.Features.Advertisement.Commands.CreateAdvertisementCommand;
using CleanArc.Application.Features.Advertisement.Commands.DeleteAdvertisementCommand;
using CleanArc.Application.Features.Advertisement.Commands.UpdateAdvertisementCommand;
using CleanArc.Application.Features.Advertisement.Queries.GetAdvertisementById;
using CleanArc.Application.Features.Advertisement.Queries.GetAllAdvertisement;
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

namespace CleanArc.Web.Api.Controllers.V1.Advertisement
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/Advertisement")]
    //[Authorize]
    public class AdvertisementController : _BaseController<CreateAdvertisementCommand, UpdateAdvertisementCommand, DeleteAdvertisementCommand, bool, GetAllAdvertisementQuery,
    List<GetAllAdvertisementQueryResult>, GetAdvertisementByIdQuery, GetAdvertisementByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public AdvertisementController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateAdvertisement")]
        //public async Task<IActionResult> CreateAdvertisement(CreateAdvertisementCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateAdvertisement")]
        //public async Task<IActionResult> UpdateAdvertisement(UpdateAdvertisementCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteAdvertisement")]
        //public async Task<IActionResult> DeleteAdvertisement(DeleteAdvertisementCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllAdvertisement")]
        //public async Task<IActionResult> GetAllAdvertisement( )
        //{
        //    var queryResult = await _sender.Send(new GetAllAdvertisementQuery());

        //    return base.OperationResult(queryResult);
        //}
        public AdvertisementController(ISender sender, ILogger<_BaseController<CreateAdvertisementCommand, UpdateAdvertisementCommand, DeleteAdvertisementCommand, bool, GetAllAdvertisementQuery,
   List<GetAllAdvertisementQueryResult>, GetAdvertisementByIdQuery, GetAdvertisementByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
