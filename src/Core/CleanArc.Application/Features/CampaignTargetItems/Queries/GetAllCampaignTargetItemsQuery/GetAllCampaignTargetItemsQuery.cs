using CleanArc.Application.Features.Campaign.Queries.GetAllCampaigns;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.CampaignTargetItems.Queries.GetAllCampaignTargetItems;

public record GetAllCampaignTargetItemsQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllCampaignTargetItemsQueryResult>>>;

