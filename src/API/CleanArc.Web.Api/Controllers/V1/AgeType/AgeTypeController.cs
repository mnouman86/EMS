using Asp.Versioning;
using CleanArc.Application.Features.AgeType.Commands.CreateAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Commands.DeleteAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Commands.UpdateAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
using CleanArc.Application.Features.AgeType.Queries.GetAllAgeType;
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

namespace CleanArc.Web.Api.Controllers.V1.AgeType
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/AgeType")]
    //[Authorize]
    public class AgeTypeController : _BaseController<CreateAgeTypeCommand, UpdateAgeTypeCommand, DeleteAgeTypeCommand, bool, GetAllAgeTypeQuery,
    List<GetAllAgeTypeQueryResult>, GetAgeTypeByIdQuery, GetAgeTypeByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public AgeTypeController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateAgeType")]
        //public async Task<IActionResult> CreateAgeType(CreateAgeTypeCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateAgeType")]
        //public async Task<IActionResult> UpdateAgeType(UpdateAgeTypeCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteAgeType")]
        //public async Task<IActionResult> DeleteAgeType(DeleteAgeTypeCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllAgeType")]
        //public async Task<IActionResult> GetAllAgeType( )
        //{
        //    var queryResult = await _sender.Send(new GetAllAgeTypeQuery());

        //    return base.OperationResult(queryResult);
        //}
        public AgeTypeController(ISender sender, ILogger<_BaseController<CreateAgeTypeCommand, UpdateAgeTypeCommand, DeleteAgeTypeCommand, bool, GetAllAgeTypeQuery,
   List<GetAllAgeTypeQueryResult>, GetAgeTypeByIdQuery, GetAgeTypeByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
