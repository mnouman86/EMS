using Asp.Versioning;
using CleanArc.Application.Features.AgeType.Commands.CreateAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Commands.DeleteAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Commands.UpdateAgeTypeCommand;
using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
using CleanArc.Application.Features.AgeType.Queries.GetAllAgeType;
using CleanArc.Application.Features.State.Command.CreateStateCommand;
using CleanArc.Application.Features.State.Command.DeleteStateCommand;
using CleanArc.Application.Features.State.Command.UpdateStateCommand;
using CleanArc.Application.Features.State.Queries.GetAllStates;
using CleanArc.Application.Features.State.Queries.GetStateById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Web.Api.Controllers.V1.State
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/State")]
    //[Authorize]
    public class StateController : _BaseController<CreateStateCommand, UpdateStateCommand, DeleteStateCommand, bool, GetAllStatesQuery,
    List<GetAllStatesQueryResult>, GetStateByIdQuery, GetStateByIdQueryResult>
    {
        
        public StateController(ISender sender, ILogger<_BaseController<CreateStateCommand, UpdateStateCommand, DeleteStateCommand, bool, GetAllStatesQuery,
   List<GetAllStatesQueryResult>, GetStateByIdQuery, GetStateByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}