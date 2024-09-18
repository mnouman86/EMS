using Asp.Versioning;
using CleanArc.Application.Features.URL.Commands.AddURLCommand;
using CleanArc.Application.Features.URL.Commands.DeleteURLCommand;
using CleanArc.Application.Features.URL.Commands.UpdateURLCommand;
using CleanArc.Application.Features.URL.Queries.GetAllURLs;
using CleanArc.Application.Features.URL.Queries.GetURLById;
using CleanArc.Domain.Common;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.Order;

/// <summary>l
/// API controller for managing URLs.
/// </summary>
/// <seealso cref="CleanArc.WebFramework.BaseController._BaseController{AddURLCommand, UpdateURLCommand, DeleteURLCommand, ResponseEntity, GetAllURLsQuery,GetAllURLsQueryResult, GetURLByIdQuery, GetURLByIdQueryResult}" />
/// <remarks>
/// This controller inherits from the generic base controller, providing CRUD operations for URLs.
/// It handles requests related to adding, updating, and deleting URLs, as well as retrieving a list
/// of all URLs and getting a specific URL by its identifier.
/// </remarks>
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
//[Authorize]
public class UrlController : _BaseController<AddURLCommand, UpdateURLCommand, DeleteURLCommand, ResponseEntity, GetAllURLsQuery,
    List<GetAllURLsQueryResult>, GetURLByIdQuery, GetURLByIdQueryResult>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UrlController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    public UrlController(ISender sender, ILogger<_BaseController<AddURLCommand, UpdateURLCommand, DeleteURLCommand, ResponseEntity, GetAllURLsQuery,
    List<GetAllURLsQueryResult>, GetURLByIdQuery, GetURLByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }


}