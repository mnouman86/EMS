using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.BusinessType.Queries.GetBusinessTypeById
{
    public record GetBusinessTypeByIdQuery(SearchRequestById searchRequestById):
        IRequest<OperationResult<GetBusinessTypeByIdQueryResult>>;
}
