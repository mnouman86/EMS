using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.ActivityIncludeOptionMapping.Queries.GetActivityIncludeOptionMappingById
{
    public class GetActivityIncludeOptionMappingByIdQuery:IRequest<OperationResult<GetActivityIncludeOptionMappingByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
