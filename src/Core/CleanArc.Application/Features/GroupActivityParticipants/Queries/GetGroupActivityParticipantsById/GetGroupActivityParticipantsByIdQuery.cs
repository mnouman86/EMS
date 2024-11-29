using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.GroupActivityParticipants.Queries.GetGroupActivityParticipantsById
{
    public class GetGroupActivityParticipantsByIdQuery:IRequest<OperationResult<GetGroupActivityParticipantsByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
