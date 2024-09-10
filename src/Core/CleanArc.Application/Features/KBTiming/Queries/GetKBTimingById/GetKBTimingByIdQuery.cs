using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.KBTiming.Queries.GetKBTimingById
{
    public class GetKBTimingByIdQuery:IRequest<OperationResult<GetKBTimingByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
