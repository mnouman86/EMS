using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.ActivityType.Queries.GetActivityTypeById
{
    public record GetActivityTypeByIdQuery(SearchRequestById searchRequestById):IRequest<OperationResult<GetActivityTypeByIdQueryResult>>;
    
}
