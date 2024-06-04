using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.RoomDetails.Queries.GetRoomDetailById;

public class GetRoomDetailByIdQuery : IRequest<OperationResult<GetRoomDetailByIdQueryResult>>
{
    public int Id { get; set; }

}
