using Asp.Versioning;
using CleanArc.Application.Features.State.Command.CreateStateCommand;
using CleanArc.Application.Features.State.Command.DeleteStateCommand;
using CleanArc.Application.Features.State.Command.UpdateStateCommand;
using CleanArc.Application.Features.State.Queries.GetAllStates;
using CleanArc.Application.Features.State.Queries.GetStateById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.State
{
   [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/State")]
    //[Authorize]
    public class StateController : _BaseController<CreateStateCommand, UpdateStateCommand, DeleteStateCommand, ResponseEntity, GetAllStatesQuery,
    List<GetAllStatesQueryResult>, GetStateByIdQuery, GetStateByIdQueryResult>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="StateController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public StateController(ISender sender, ILogger<_BaseController<CreateStateCommand, UpdateStateCommand, DeleteStateCommand, ResponseEntity, GetAllStatesQuery,
   List<GetAllStatesQueryResult>, GetStateByIdQuery, GetStateByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}