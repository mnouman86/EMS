using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.ActivityDisabilityMapping.Queries.GetActivityDisabilityMappingById
{
    public class GetActivityDisabilityMappingByIdQuery:IRequest<OperationResult<GetActivityDisabilityMappingByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
