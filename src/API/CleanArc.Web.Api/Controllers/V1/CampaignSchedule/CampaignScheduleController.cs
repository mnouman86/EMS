using Asp.Versioning;
using CleanArc.Application.Features.CampaignSchedule.Command.CreateCampaignScheduleCommand;
using CleanArc.Application.Features.CampaignSchedule.Command.DeleteCampaignScheduleCommand;
using CleanArc.Application.Features.CampaignSchedule.Command.UpdateCampaignScheduleCommand;
using CleanArc.Application.Features.CampaignSchedule.Queries.GetAllCampaignSchedules;
using CleanArc.Application.Features.CampaignSchedule.Queries.GetCampaignScheduleById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.CampaignSchedule
{
    /// <summary>
    /// CampaignScheduleController is responsible for handling HTTP requests related to room type operations
    /// such as creating, updating, deleting, and retrieving room types. It extends from a base controller
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateCampaignSchedule: Handles the creation of a new room type.
    /// 2. UpdateCampaignSchedule: Handles the updating of an existing room type.
    /// 3. DeleteCampaignSchedule: Handles the deletion of an existing room type.
    /// 4. GetAllCampaignSchedules: Retrieves all room types.
    /// 5. GetCampaignScheduleById: Retrieves a specific room type by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/CampaignSchedule".
    /// 
    /// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
    /// operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateCampaignSchedule")]
    /// public async Task<IActionResult> CreateCampaignSchedule(CreateCampaignScheduleCommand model)
    /// {
    ///     model.UserId = base.UserId;
    ///     var command = await _sender.Send(model);
    ///     return base.OperationResult(command);
    /// }
    /// 
    /// This ensures that the UserId is set from the base controller before sending the command and that the operation
    /// result is properly formatted for the response.
    /// 
    /// The controller is part of the CleanArc architecture, ensuring a clean separation of concerns and adherence to 
    /// SOLID principles. 
    /// </summary>
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.CampaignSchedule.Command.CreateCampaignScheduleCommand.CreateCampaignScheduleCommand, CleanArc.Application.Features.CampaignSchedule.Command.UpdateCampaignScheduleCommand.UpdateCampaignScheduleCommand, CleanArc.Application.Features.CampaignSchedule.Command.DeleteCampaignScheduleCommand.DeleteCampaignScheduleCommand, System.ResponseEntity, CleanArc.Application.Features.CampaignSchedule.Queries.GetAllCampaignSchedules.GetAllCampaignSchedulesQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.CampaignSchedule.Queries.GetAllCampaignSchedules.GetAllCampaignSchedulesQueryResult&gt;, CleanArc.Application.Features.CampaignSchedule.Queries.GetCampaignScheduleById.GetCampaignScheduleByIdQuery, CleanArc.Application.Features.CampaignSchedule.Queries.GetCampaignScheduleById.GetCampaignScheduleByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/CampaignSchedule")]
    //[Authorize]
    public class CampaignScheduleController : _BaseController<CreateCampaignScheduleCommand, UpdateCampaignScheduleCommand, DeleteCampaignScheduleCommand, ResponseEntity, GetAllCampaignSchedulesQuery,
    List<GetAllCampaignSchedulesQueryResult>, GetCampaignScheduleByIdQuery, GetCampaignScheduleByIdQueryResult>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CampaignScheduleController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public CampaignScheduleController(ISender sender, ILogger<_BaseController<CreateCampaignScheduleCommand, UpdateCampaignScheduleCommand, DeleteCampaignScheduleCommand, ResponseEntity, GetAllCampaignSchedulesQuery,
   List<GetAllCampaignSchedulesQueryResult>, GetCampaignScheduleByIdQuery, GetCampaignScheduleByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
