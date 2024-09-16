using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.PopularItemsVisit.Queries.GetPopularItemsVisitById
{
    public class GetPopularItemsVisitByIdQuery:IRequest<OperationResult<GetPopularItemsVisitByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
