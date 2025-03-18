using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.ActivityAddressMapping.Queries.GetActivityAddressMappingById
{
    public record GetActivityAddressMappingByIdQuery(SearchRequestById searchRequestById):IRequest<OperationResult<GetActivityAddressMappingByIdQueryResult>>;
    
}
