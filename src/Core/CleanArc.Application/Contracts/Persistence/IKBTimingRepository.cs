using CleanArc.Domain.Entities.KBTiming;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IKBTimingRepository:IRepository<KBTiming>
{
   // Task CreateAgeType(AgeType ageType);
}
