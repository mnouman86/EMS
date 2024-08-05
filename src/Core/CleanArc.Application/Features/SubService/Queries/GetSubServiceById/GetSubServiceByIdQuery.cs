using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.SubService.Queries.GetSubServiceById
{
    public class GetSubServiceByIdQuery:IRequest<OperationResult<GetSubServiceByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
