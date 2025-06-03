using Asp.Versioning;
using CleanArc.Application.Features.LastMinuteDeal.Command.CreateLastMinuteDealCommand;
using CleanArc.Application.Features.LastMinuteDeal.Command.DeleteLastMinuteDealCommand;
using CleanArc.Application.Features.LastMinuteDeal.Command.UpdateLastMinuteDealCommand;
using CleanArc.Application.Features.LastMinuteDeal.Queries.GetAllLastMinuteDeal;
using CleanArc.Application.Features.LastMinuteDeal.Queries.GetLastMinuteDealById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; 
using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.LastMinuteDeal;

[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/LastMinuteDeal")]
public class LastMinuteDealController : _BaseController<CreateLastMinuteDealCommand, UpdateLastMinuteDealCommand, DeleteLastMinuteDealCommand, ResponseEntity, GetAllLastMinuteDealQuery,
    List<GetAllLastMinuteDealQueryResult>, GetLastMinuteDealByIdQuery, GetLastMinuteDealByIdQueryResult>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LastMinuteDealController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public LastMinuteDealController(ISender sender, ILogger<_BaseController<CreateLastMinuteDealCommand, UpdateLastMinuteDealCommand, DeleteLastMinuteDealCommand, ResponseEntity, GetAllLastMinuteDealQuery,
List<GetAllLastMinuteDealQueryResult>, GetLastMinuteDealByIdQuery, GetLastMinuteDealByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}

