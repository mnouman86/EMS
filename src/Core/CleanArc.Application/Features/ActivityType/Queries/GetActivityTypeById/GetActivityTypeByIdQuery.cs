using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.ActivityType.Queries.GetActivityTypeById
{
    public class GetActivityTypeByIdQuery:IRequest<OperationResult<GetActivityTypeByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
