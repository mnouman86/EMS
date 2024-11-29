using CleanArc.Application.Features.Campaign.Queries.GetCampaignById;
using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.CampaignTargetItems.Queries.GetCampaignTargetItemsById;

public class GetCampaignTargetItemsByIdQuery: IRequest<OperationResult<GetCampaignTargetItemsByIdQueryResult>>
{
    public int Id { get; set; }

}

