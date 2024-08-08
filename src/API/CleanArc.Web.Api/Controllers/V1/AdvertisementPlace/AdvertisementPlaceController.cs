using Asp.Versioning;
using CleanArc.Application.Features.AdvertisementPlace.Commands.CreateAdvertisementPlaceCommand;
using CleanArc.Application.Features.AdvertisementPlace.Commands.DeleteAdvertisementPlaceCommand;
using CleanArc.Application.Features.AdvertisementPlace.Commands.UpdateAdvertisementPlaceCommand;
using CleanArc.Application.Features.AdvertisementPlace.Queries.GetAdvertisementPlaceById;
using CleanArc.Application.Features.AdvertisementPlace.Queries.GetAllAdvertisementPlace;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.AdvertisementPlace
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.AdvertisementPlace.Commands.CreateAdvertisementPlaceCommand.CreateAdvertisementPlaceCommand, CleanArc.Application.Features.AdvertisementPlace.Commands.UpdateAdvertisementPlaceCommand.UpdateAdvertisementPlaceCommand, CleanArc.Application.Features.AdvertisementPlace.Commands.DeleteAdvertisementPlaceCommand.DeleteAdvertisementPlaceCommand, System.Boolean, CleanArc.Application.Features.AdvertisementPlace.Queries.GetAllAdvertisementPlace.GetAllAdvertisementPlaceQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.AdvertisementPlace.Queries.GetAllAdvertisementPlace.GetAllAdvertisementPlaceQueryResult&gt;, CleanArc.Application.Features.AdvertisementPlace.Queries.GetAdvertisementPlaceById.GetAdvertisementPlaceByIdQuery, CleanArc.Application.Features.AdvertisementPlace.Queries.GetAdvertisementPlaceById.GetAdvertisementPlaceByIdQueryResult&gt;" />
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
        /// <summary>
        /// Initializes a new instance of the <see cref="AdvertisementPlaceController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public AdvertisementPlaceController(ISender sender, ILogger<_BaseController<CreateAdvertisementPlaceCommand, UpdateAdvertisementPlaceCommand, DeleteAdvertisementPlaceCommand, bool, GetAllAdvertisementPlaceQuery,
   List<GetAllAdvertisementPlaceQueryResult>, GetAdvertisementPlaceByIdQuery, GetAdvertisementPlaceByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
