using Asp.Versioning;
using CleanArc.Application.Features.RoomView.Command.CreateRoomViewCommand;
using CleanArc.Application.Features.RoomView.Command.DeleteRoomViewCommand;
using CleanArc.Application.Features.RoomView.Command.UpdateRoomViewCommand;
using CleanArc.Application.Features.RoomView.Queries.GetAllRoomView;
using CleanArc.Application.Features.RoomView.Queries.GetRoomViewById;
using CleanArc.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Mvc; 
using CleanArc.Domain.Common;

namespace CleanArc.Web.Api.Controllers.V1.RoomView;

[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/RoomView")]
public class RoomViewController : _BaseController<CreateRoomViewCommand, UpdateRoomViewCommand, DeleteRoomViewCommand, ResponseEntity, GetAllRoomViewQuery,
    List<GetAllRoomViewQueryResult>, GetRoomViewByIdQuery, GetRoomViewByIdQueryResult>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoomViewController"/> class.
    /// </summary>
    /// <param name="sender">The mediator sender for handling requests and responses.</param>
    /// <param name="logger">The logger for logging controller-related information.</param>
    /// <param name="httpContextAccessor"></param>
    public RoomViewController(ISender sender, ILogger<_BaseController<CreateRoomViewCommand, UpdateRoomViewCommand, DeleteRoomViewCommand, ResponseEntity, GetAllRoomViewQuery,
List<GetAllRoomViewQueryResult>, GetRoomViewByIdQuery, GetRoomViewByIdQueryResult>> logger, IHttpContextAccessor httpContextAccessor) : base(sender, logger, httpContextAccessor)
    {

    }

}

