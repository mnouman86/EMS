using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.KBWhenToVisit.Queries.GetKBWhenToVisitById
{
    public class GetKBWhenToVisitByIdQuery:IRequest<OperationResult<GetKBWhenToVisitByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
