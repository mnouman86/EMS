using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.ActivityIDImageMapping.Queries.GetActivityIDImageMappingById
{
    public class GetActivityIDImageMappingByIdQuery:IRequest<OperationResult<GetActivityIDImageMappingByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
