using CleanArc.Application.Features.Campaign.Queries.GetCampaignById;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.CampaignTarget.Queries.GetCampaignTargetById;

public record GetCampaignTargetByIdQuery(SearchRequestById searchRequestById):
    IRequest<OperationResult<GetCampaignTargetByIdQueryResult>>;

