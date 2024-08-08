using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.ActivitySchedule.Queries.GetActivityScheduleById
{
    public class GetActivityScheduleByIdQuery:IRequest<OperationResult<GetActivityScheduleByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
