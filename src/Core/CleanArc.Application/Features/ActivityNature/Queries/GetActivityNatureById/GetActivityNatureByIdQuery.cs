using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.ActivityNature.Queries.GetActivityNatureById
{
    public class GetActivityNatureByIdQuery:IRequest<OperationResult<GetActivityNatureByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
