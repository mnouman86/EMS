using CleanArc.Domain.Entities.ActivityDisabilityOption;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IActivityDisabilityOptionRepository:IRepository<ActivityDisabilityOption>
{
   // Task CreateAgeType(AgeType ageType);
}
