using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.ActivityGroup.Queries.GetActivityGroupById
{
    public class GetActivityGroupByIdQuery:IRequest<OperationResult<GetActivityGroupByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
