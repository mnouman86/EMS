using Asp.Versioning;
using CleanArc.Application.Features.AdvertisementPage.Commands.CreateAdvertisementPageCommand;
using CleanArc.Application.Features.AdvertisementPage.Commands.DeleteAdvertisementPageCommand;
using CleanArc.Application.Features.AdvertisementPage.Commands.UpdateAdvertisementPageCommand;
using CleanArc.Application.Features.AdvertisementPage.Queries.GetAdvertisementPageById;
using CleanArc.Application.Features.AdvertisementPage.Queries.GetAllAdvertisementPage;
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

namespace CleanArc.Web.Api.Controllers.V1.AdvertisementPage
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/AdvertisementPage")]
    //[Authorize]
    public class AdvertisementPageController : _BaseController<CreateAdvertisementPageCommand, UpdateAdvertisementPageCommand, DeleteAdvertisementPageCommand, bool, GetAllAdvertisementPageQuery,
    List<GetAllAdvertisementPageQueryResult>, GetAdvertisementPageByIdQuery, GetAdvertisementPageByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public AdvertisementPageController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateAdvertisementPage")]
        //public async Task<IActionResult> CreateAdvertisementPage(CreateAdvertisementPageCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateAdvertisementPage")]
        //public async Task<IActionResult> UpdateAdvertisementPage(UpdateAdvertisementPageCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteAdvertisementPage")]
        //public async Task<IActionResult> DeleteAdvertisementPage(DeleteAdvertisementPageCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllAdvertisementPage")]
        //public async Task<IActionResult> GetAllAdvertisementPage( )
        //{
        //    var queryResult = await _sender.Send(new GetAllAdvertisementPageQuery());

        //    return base.OperationResult(queryResult);
        //}
        public AdvertisementPageController(ISender sender, ILogger<_BaseController<CreateAdvertisementPageCommand, UpdateAdvertisementPageCommand, DeleteAdvertisementPageCommand, bool, GetAllAdvertisementPageQuery,
   List<GetAllAdvertisementPageQueryResult>, GetAdvertisementPageByIdQuery, GetAdvertisementPageByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
