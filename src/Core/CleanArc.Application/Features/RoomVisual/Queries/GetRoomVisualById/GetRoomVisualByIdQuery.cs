using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.RoomVisual.Queries.GetRoomVisualById;

public class GetRoomVisualByIdQuery : IRequest<OperationResult<GetRoomVisualByIdQueryResult>>
{
    public int Id { get; set; }

}
