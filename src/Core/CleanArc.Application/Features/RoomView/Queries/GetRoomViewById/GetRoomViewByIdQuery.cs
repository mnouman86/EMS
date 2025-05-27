using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
using CleanArc.Application.Features.RoomView.Queries.GetRoomViewById;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.RoomView.Queries.GetRoomViewById;

public record GetRoomViewByIdQuery(SearchRequestById searchRequestById):IRequest<OperationResult<GetRoomViewByIdQueryResult>>;
