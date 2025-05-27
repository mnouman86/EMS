using Asp.Versioning;
using CleanArc.Application.Features.OutDoor.Command.CreateOutDoorCommand;
using CleanArc.Application.Features.OutDoor.Command.DeleteOutDoorCommand;
using CleanArc.Application.Features.OutDoor.Command.UpdateOutDoorCommand;
using CleanArc.Application.Features.OutDoor.Queries.GetAllOutDoor;
using CleanArc.Application.Features.OutDoor.Queries.GetOutDoorById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; 
using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.OutDoor;

[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/OutDoor")]
public class OutDoorController : _BaseController<CreateOutDoorCommand, UpdateOutDoorCommand, DeleteOutDoorCommand, ResponseEntity, GetAllOutDoorQuery,
    List<GetAllOutDoorQueryResult>, GetOutDoorByIdQuery, GetOutDoorByIdQueryResult>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OutDoorController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public OutDoorController(ISender sender, ILogger<_BaseController<CreateOutDoorCommand, UpdateOutDoorCommand, DeleteOutDoorCommand, ResponseEntity, GetAllOutDoorQuery,
List<GetAllOutDoorQueryResult>, GetOutDoorByIdQuery, GetOutDoorByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}

