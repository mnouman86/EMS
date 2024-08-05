using Asp.Versioning;
using CleanArc.Application.Features.State.Command.CreateStateCommand;
using CleanArc.Application.Features.State.Command.DeleteStateCommand;
using CleanArc.Application.Features.State.Command.UpdateStateCommand;
using CleanArc.Application.Features.State.Queries.GetAllStates;
using CleanArc.Application.Features.State.Queries.GetStateById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.State
{
    /// <summary>
    /// StateController is responsible for handling HTTP requests related to state operations
    /// such as creating, updating, deleting, and retrieving states. It extends from a base controller
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateState: Handles the creation of a new state.
    /// 2. UpdateState: Handles the updating of an existing state.
    /// 3. DeleteState: Handles the deletion of an existing state.
    /// 4. GetAllStates: Retrieves all states.
    /// 5. GetStateById: Retrieves a specific state by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/State".
    /// 
    /// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
    /// operation result handling and user ID setting.
    /// 
    /// The controller is part of the CleanArc architecture, ensuring a clean separation of concerns and adherence to 
    /// SOLID principles. 
    /// </summary>
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.State.Command.CreateStateCommand.CreateStateCommand, CleanArc.Application.Features.State.Command.UpdateStateCommand.UpdateStateCommand, CleanArc.Application.Features.State.Command.DeleteStateCommand.DeleteStateCommand, System.Boolean, CleanArc.Application.Features.State.Queries.GetAllStates.GetAllStatesQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.State.Queries.GetAllStates.GetAllStatesQueryResult&gt;, CleanArc.Application.Features.State.Queries.GetStateById.GetStateByIdQuery, CleanArc.Application.Features.State.Queries.GetStateById.GetStateByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/State")]
    //[Authorize]
    public class StateController : _BaseController<CreateStateCommand, UpdateStateCommand, DeleteStateCommand, bool, GetAllStatesQuery,
    List<GetAllStatesQueryResult>, GetStateByIdQuery, GetStateByIdQueryResult>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="StateController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public StateController(ISender sender, ILogger<_BaseController<CreateStateCommand, UpdateStateCommand, DeleteStateCommand, bool, GetAllStatesQuery,
   List<GetAllStatesQueryResult>, GetStateByIdQuery, GetStateByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}