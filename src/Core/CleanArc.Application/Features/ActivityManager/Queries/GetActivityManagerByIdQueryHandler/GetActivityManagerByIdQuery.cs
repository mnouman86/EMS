using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.ActivityManager.Queries.GetActivityManagerById
{
    public class GetActivityManagerByIdQuery:IRequest<OperationResult<GetActivityManagerByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
