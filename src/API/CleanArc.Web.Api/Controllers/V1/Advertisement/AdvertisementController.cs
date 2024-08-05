using Asp.Versioning;
using CleanArc.Application.Features.Advertisement.Commands.CreateAdvertisementCommand;
using CleanArc.Application.Features.Advertisement.Commands.DeleteAdvertisementCommand;
using CleanArc.Application.Features.Advertisement.Commands.UpdateAdvertisementCommand;
using CleanArc.Application.Features.Advertisement.Queries.GetAdvertisementById;
using CleanArc.Application.Features.Advertisement.Queries.GetAllAdvertisement;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.Advertisement
{
    /// <summary>
    /// AdvertisementController is responsible for managing HTTP requests related to advertisement operations
    /// such as creating, updating, deleting, and retrieving advertisements. It extends from a base controller
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateAdvertisementCommand: Handles the creation of new advertisements.
    /// 2. UpdateAdvertisementCommand: Handles the updating of existing advertisements.
    /// 3. DeleteAdvertisementCommand: Handles the deletion of advertisements.
    /// 4. GetAllAdvertisementQuery: Retrieves a list of all advertisements.
    /// 5. GetAdvertisementByIdQuery: Retrieves a specific advertisement by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/Advertisement".
    /// 
    /// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
    /// operation result handling and user ID setting.
    /// </summary>
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.Advertisement.Commands.CreateAdvertisementCommand.CreateAdvertisementCommand, CleanArc.Application.Features.Advertisement.Commands.UpdateAdvertisementCommand.UpdateAdvertisementCommand, CleanArc.Application.Features.Advertisement.Commands.DeleteAdvertisementCommand.DeleteAdvertisementCommand, System.Boolean, CleanArc.Application.Features.Advertisement.Queries.GetAllAdvertisement.GetAllAdvertisementQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.Advertisement.Queries.GetAllAdvertisement.GetAllAdvertisementQueryResult&gt;, CleanArc.Application.Features.Advertisement.Queries.GetAdvertisementById.GetAdvertisementByIdQuery, CleanArc.Application.Features.Advertisement.Queries.GetAdvertisementById.GetAdvertisementByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/Advertisement")]
    //[Authorize]
    public class AdvertisementController : _BaseController<CreateAdvertisementCommand, UpdateAdvertisementCommand, DeleteAdvertisementCommand, bool, GetAllAdvertisementQuery,
    List<GetAllAdvertisementQueryResult>, GetAdvertisementByIdQuery, GetAdvertisementByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public AdvertisementController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateAdvertisement")]
        //public async Task<IActionResult> CreateAdvertisement(CreateAdvertisementCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateAdvertisement")]
        //public async Task<IActionResult> UpdateAdvertisement(UpdateAdvertisementCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteAdvertisement")]
        //public async Task<IActionResult> DeleteAdvertisement(DeleteAdvertisementCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllAdvertisement")]
        //public async Task<IActionResult> GetAllAdvertisement( )
        //{
        //    var queryResult = await _sender.Send(new GetAllAdvertisementQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="AdvertisementController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public AdvertisementController(ISender sender, ILogger<_BaseController<CreateAdvertisementCommand, UpdateAdvertisementCommand, DeleteAdvertisementCommand, bool, GetAllAdvertisementQuery,
   List<GetAllAdvertisementQueryResult>, GetAdvertisementByIdQuery, GetAdvertisementByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
