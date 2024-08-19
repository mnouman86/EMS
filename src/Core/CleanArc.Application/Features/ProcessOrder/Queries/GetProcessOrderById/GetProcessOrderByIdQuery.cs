using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.ProcessOrder.Queries.GetProcessOrderById
{
    public class GetProcessOrderByIdQuery:IRequest<OperationResult<GetProcessOrderByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
