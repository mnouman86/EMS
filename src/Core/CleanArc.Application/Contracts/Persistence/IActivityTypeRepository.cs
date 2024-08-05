using CleanArc.Domain.Entities.ActivityType;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IActivityTypeRepository:IRepository<ActivityType>
{
   // Task CreateAgeType(AgeType ageType);
}
