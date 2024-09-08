using CleanArc.Domain.Entities.KBRelatedUrlLink;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IKBRelatedUrlLinkRepository:IRepository<KBRelatedUrlLink>
{
   // Task CreateAgeType(AgeType ageType);
}
