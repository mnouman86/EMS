using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.ActivitySeasonMapping.Queries.GetActivitySeasonMappingById
{
    public class GetActivitySeasonMappingByIdQuery:IRequest<OperationResult<GetActivitySeasonMappingByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
