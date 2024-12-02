using Asp.Versioning;
using CleanArc.Application.Features.CampaignTarget.Command.CreateCampaignTargetCommand;
using CleanArc.Application.Features.CampaignTarget.Command.DeleteCampaignTargetCommand;
using CleanArc.Application.Features.CampaignTarget.Command.UpdateCampaignTargetCommand;
using CleanArc.Application.Features.CampaignTarget.Queries.GetAllCampaignTarget;
using CleanArc.Application.Features.CampaignTarget.Queries.GetCampaignTargetById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using CleanArc.Domain.Common;
using CleanArc.Web.Api.Controllers.V1.CampaignTarget;
using CleanArc.Application.Features.CampaignTarget.Queries.GetAllCampaignTarget;

namespace CleanArc.Web.Api.Controllers.V1.CampaignTarget
{
    /// <summary>
    /// CampaignTargetController is responsible for handling HTTP requests related to campaign operations
    /// such as creating, updating, deleting, and retrieving campaigns. It extends from a base controller
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateCampaignTarget: Handles the creation of a new campaign.
    /// 2. UpdateCampaignTarget: Handles the updating of an existing campaign.
    /// 3. DeleteCampaignTarget: Handles the deletion of an existing campaign.
    /// 4. GetAllCampaignTargets: Retrieves all campaigns.
    /// 5. GetCampaignTargetById: Retrieves a specific campaign by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/Campaign".
    /// 
    /// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
    /// operation result handling and user ID setting.
    /// 
    /// Example Usage:
    /// 
    /// [HttpPost("CreateCampaignTarget")]
    /// public async Task<IActionResult> CreateCampaignTarget(CreateCampaignTargetCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.Campaign.Command.CreateCampaignCommand.CreateCampaignTargetCommand, CleanArc.Application.Features.Campaign.Command.UpdateCampaignCommand.UpdateCampaignTargetCommand, CleanArc.Application.Features.Campaign.Command.DeleteCampaignCommand.DeleteCampaignTargetCommand, System.ResponseEntity, CleanArc.Application.Features.Campaign.Queries.GetAllCampaigns.GetAllCampaignTargetQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.Campaign.Queries.GetAllCampaigns.GetAllCampaignsQueryResult&gt;, CleanArc.Application.Features.Campaign.Queries.GetCampaignById.GetCampaignTargetByIdQuery, CleanArc.Application.Features.Campaign.Queries.GetCampaignById.GetCampaignTargetByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/CampaignTarget")]
    //[Authorize]
    public class CampaignTargetController : _BaseController<CreateCampaignTargetCommand, UpdateCampaignTargetCommand, DeleteCampaignTargetCommand, ResponseEntity, GetAllCampaignTargetQuery,
    List<GetAllCampaignTargetQueryResult>, GetCampaignTargetByIdQuery, GetCampaignTargetByIdQueryResult>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CampaignTargetController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public CampaignTargetController(ISender sender, ILogger<_BaseController<CreateCampaignTargetCommand, UpdateCampaignTargetCommand, DeleteCampaignTargetCommand, ResponseEntity, GetAllCampaignTargetQuery,
   List<GetAllCampaignTargetQueryResult>, GetCampaignTargetByIdQuery, GetCampaignTargetByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
