using Asp.Versioning;
using CleanArc.Application.Features.CarDetail.Command.CreateCarDetailCommand;
using CleanArc.Application.Features.CarDetail.Command.DeleteCarDetailCommand;
using CleanArc.Application.Features.CarDetail.Command.UpdateCarDetailCommand;
using CleanArc.Application.Features.CarDetail.Query.GetAllCarDetail;
using CleanArc.Application.Features.CarDetail.Query.GetCarDetailById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.CarDetail
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/CarDetail")]
    //[Authorize]

    public class CarDetailController : _BaseController<CreateCarDetailCommand, UpdateCarDetailCommand, DeleteCarDetailCommand, bool, GetAllCarDetailQuery,
    List<GetAllCarDetailQueryResult>, GetCarDetailByIdQuery, GetCarDetailByIdQueryResult>
    {
        
        public CarDetailController(ISender sender, ILogger<_BaseController<CreateCarDetailCommand, UpdateCarDetailCommand, DeleteCarDetailCommand, bool, GetAllCarDetailQuery,
   List<GetAllCarDetailQueryResult>, GetCarDetailByIdQuery, GetCarDetailByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : 
            base(sender, logger, httpContextAccessor)
        {

        }

    }
}

