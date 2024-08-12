using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.ActivityPricePerParticipant.Queries.GetActivityPricePerParticipantById
{
    public class GetActivityPricePerParticipantByIdQuery:IRequest<OperationResult<GetActivityPricePerParticipantByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
