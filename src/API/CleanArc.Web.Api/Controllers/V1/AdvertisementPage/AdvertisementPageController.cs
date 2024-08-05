using Asp.Versioning;
using CleanArc.Application.Features.AdvertisementPage.Commands.CreateAdvertisementPageCommand;
using CleanArc.Application.Features.AdvertisementPage.Commands.DeleteAdvertisementPageCommand;
using CleanArc.Application.Features.AdvertisementPage.Commands.UpdateAdvertisementPageCommand;
using CleanArc.Application.Features.AdvertisementPage.Queries.GetAdvertisementPageById;
using CleanArc.Application.Features.AdvertisementPage.Queries.GetAllAdvertisementPage;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.AdvertisementPage
{
    /// <summary>
    /// AdvertisementPageController is responsible for managing HTTP requests related to advertisement page operations
    /// such as creating, updating, deleting, and retrieving advertisement pages. It extends from a base controller
    /// which provides common functionality for CRUD operations.
    /// 
    /// The controller uses MediatR to send commands and queries to the corresponding handlers. It includes
    /// endpoint definitions for the following actions:
    /// 
    /// 1. CreateAdvertisementPageCommand: Handles the creation of new advertisement pages.
    /// 2. UpdateAdvertisementPageCommand: Handles the updating of existing advertisement pages.
    /// 3. DeleteAdvertisementPageCommand: Handles the deletion of advertisement pages.
    /// 4. GetAllAdvertisementPageQuery: Retrieves a list of all advertisement pages.
    /// 5. GetAdvertisementPageByIdQuery: Retrieves a specific advertisement page by its ID.
    /// 
    /// The controller uses dependency injection to receive instances of ISender (for sending commands and queries),
    /// ILogger (for logging purposes), and IHttpContextAccessor (for accessing HTTP context information).
    /// 
    /// The controller is versioned using the [ApiVersion] attribute and responds to routes prefixed with the API
    /// version, specified as "api/v{version:apiVersion}/AdvertisementPage".
    /// 
    /// Note: The actual endpoint methods are intended to be implemented to utilize the base controller’s 
    /// operation result handling and user ID setting.
    /// </summary>
    /// <seealso cref="CleanArc.WebFramework.BaseController._BaseController&lt;CleanArc.Application.Features.AdvertisementPage.Commands.CreateAdvertisementPageCommand.CreateAdvertisementPageCommand, CleanArc.Application.Features.AdvertisementPage.Commands.UpdateAdvertisementPageCommand.UpdateAdvertisementPageCommand, CleanArc.Application.Features.AdvertisementPage.Commands.DeleteAdvertisementPageCommand.DeleteAdvertisementPageCommand, System.Boolean, CleanArc.Application.Features.AdvertisementPage.Queries.GetAllAdvertisementPage.GetAllAdvertisementPageQuery, System.Collections.Generic.List&lt;CleanArc.Application.Features.AdvertisementPage.Queries.GetAllAdvertisementPage.GetAllAdvertisementPageQueryResult&gt;, CleanArc.Application.Features.AdvertisementPage.Queries.GetAdvertisementPageById.GetAdvertisementPageByIdQuery, CleanArc.Application.Features.AdvertisementPage.Queries.GetAdvertisementPageById.GetAdvertisementPageByIdQueryResult&gt;" />
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/AdvertisementPage")]
    //[Authorize]
    public class AdvertisementPageController : _BaseController<CreateAdvertisementPageCommand, UpdateAdvertisementPageCommand, DeleteAdvertisementPageCommand, bool, GetAllAdvertisementPageQuery,
    List<GetAllAdvertisementPageQueryResult>, GetAdvertisementPageByIdQuery, GetAdvertisementPageByIdQueryResult>
    {
        //private readonly ISender _sender;

        //public AdvertisementPageController(ISender sender)
        //{
        //    _sender = sender;
        //}

        //[HttpPost("CreateAdvertisementPage")]
        //public async Task<IActionResult> CreateAdvertisementPage(CreateAdvertisementPageCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("UpdateAdvertisementPage")]
        //public async Task<IActionResult> UpdateAdvertisementPage(UpdateAdvertisementPageCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpPost("DeleteAdvertisementPage")]
        //public async Task<IActionResult> DeleteAdvertisementPage(DeleteAdvertisementPageCommand model)
        //{
        //    model.UserId = base.UserId;
        //    var command = await _sender.Send(model);

        //    return base.OperationResult(command);
        //}
        //[HttpGet("GetAllAdvertisementPage")]
        //public async Task<IActionResult> GetAllAdvertisementPage( )
        //{
        //    var queryResult = await _sender.Send(new GetAllAdvertisementPageQuery());

        //    return base.OperationResult(queryResult);
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="AdvertisementPageController"/> class.
        /// </summary>
        /// <param name="sender">The mediator sender for handling requests and responses.</param>
        /// <param name="logger">The logger for logging controller-related information.</param>
        /// <param name="httpContextAccessor"></param>
        public AdvertisementPageController(ISender sender, ILogger<_BaseController<CreateAdvertisementPageCommand, UpdateAdvertisementPageCommand, DeleteAdvertisementPageCommand, bool, GetAllAdvertisementPageQuery,
   List<GetAllAdvertisementPageQueryResult>, GetAdvertisementPageByIdQuery, GetAdvertisementPageByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
        {

        }

    }
}
