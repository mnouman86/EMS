using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.ActivityIncludeOptionMapping.Queries.GetActivityIncludeOptionMappingById
{
    public record GetActivityIncludeOptionMappingByIdQuery(SearchRequestById searchRequestById):IRequest<OperationResult<GetActivityIncludeOptionMappingByIdQueryResult>>;
    
}
