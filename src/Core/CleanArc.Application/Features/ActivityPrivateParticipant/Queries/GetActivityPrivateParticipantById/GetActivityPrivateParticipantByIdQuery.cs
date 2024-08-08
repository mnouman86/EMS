using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.ActivityPrivateParticipant.Queries.GetActivityPrivateParticipantById
{
    public class GetActivityPrivateParticipantByIdQuery:IRequest<OperationResult<GetActivityPrivateParticipantByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
