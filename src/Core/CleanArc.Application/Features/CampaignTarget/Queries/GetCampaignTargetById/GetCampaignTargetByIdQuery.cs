using CleanArc.Application.Features.Campaign.Queries.GetCampaignById;
using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.CampaignTarget.Queries.GetCampaignTargetById;

public class GetCampaignTargetByIdQuery : IRequest<OperationResult<GetCampaignTargetByIdQueryResult>>
{
    public int Id { get; set; }

}

