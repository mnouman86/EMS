using Asp.Versioning;
using CleanArc.Application.Features.Campaign.Command.CreateCampaignCommand;
using CleanArc.Application.Features.Campaign.Command.DeleteCampaignCommand;
using CleanArc.Application.Features.Campaign.Command.UpdateCampaignCommand;
using CleanArc.Application.Features.Campaign.Queries.GetAllCampaigns;
using CleanArc.Application.Features.Campaign.Queries.GetCampaignById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.Campaign
{
    /// <summary>
    /// CampaignController is responsible for handling HTTP requests related to room type operations
    /// such as creating, updating, deleting, and retrieving room types. It extends from a base controller
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateCampaign: Handles the creation of a new room type.
    /// 2. UpdateCampaign: Handles the updating of an existing room type.
    /// 3. DeleteCampaign: Handles the deletion of an existing room type.
    /// 4. GetAllCampaigns: Retrieves all room types.
    /// 5. GetCampaignById: Retrieves a specific room type by its ID.
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
    /// [HttpPost("CreateCampaign")]
    /// public async Task<IActionResult> CreateCampaign(CreateCampaignCommand model)
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
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.Campaign.Command.CreateCampaignCommand.CreateCampaignCommand, CleanArc.Application.Features.Campaign.Command.UpdateCampaignCommand.UpdateCampaignCommand, CleanArc.Application.Features.Campaign.Command.DeleteCampaignCommand.DeleteCampaignCommand, System.ResponseEntity, CleanArc.Application.Features.Campaign.Queries.GetAllCampaigns.GetAllCampaignsQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.Campaign.Queries.GetAllCampaigns.GetAllCampaignsQueryResult&gt;, CleanArc.Application.Features.Campaign.Queries.GetCampaignById.GetCampaignByIdQuery, CleanArc.Application.Features.Campaign.Queries.GetCampaignById.GetCampaignByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/Campaign")]
    //[Authorize]
    public class CampaignController : _BaseController<CreateCampaignCommand, UpdateCampaignCommand, DeleteCampaignCommand, ResponseEntity, GetAllCampaignsQuery,
    List<GetAllCampaignsQueryResult>, GetCampaignByIdQuery, GetCampaignByIdQueryResult>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CampaignController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public CampaignController(ISender sender, ILogger<_BaseController<CreateCampaignCommand, UpdateCampaignCommand, DeleteCampaignCommand, ResponseEntity, GetAllCampaignsQuery,
   List<GetAllCampaignsQueryResult>, GetCampaignByIdQuery, GetCampaignByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
