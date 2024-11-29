using CleanArc.Domain.Entities.Campaign;
using CleanArc.Domain.Entities.CampaignTarget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public interface ICampaignTargetRepository:IRepository<CampaignTargetItems>
{
}
