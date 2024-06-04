using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById
{
    public class GetAgeTypeByIdQuery:IRequest<OperationResult<GetAgeTypeByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
