using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.DisabilityOption.Queries.GetDisabilityOptionById
{
    public class GetDisabilityOptionByIdQuery:IRequest<OperationResult<GetDisabilityOptionByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
