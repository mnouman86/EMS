using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.CoreArea.Queries.GetCoreAreaById
{
    public class GetCoreAreaByIdQuery:IRequest<OperationResult<GetCoreAreaByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
