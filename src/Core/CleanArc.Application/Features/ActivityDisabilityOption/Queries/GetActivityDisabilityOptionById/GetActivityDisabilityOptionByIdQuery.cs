using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.ActivityDisabilityOption.Queries.GetActivityDisabilityOptionById
{
    public class GetActivityDisabilityOptionByIdQuery:IRequest<OperationResult<GetActivityDisabilityOptionByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
