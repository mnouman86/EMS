using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.CampaignSchedule.Queries.GetCampaignScheduleById;

public class GetCampaignScheduleByIdQuery: IRequest<OperationResult<GetCampaignScheduleByIdQueryResult>>
{
    public int Id { get; set; }

}

