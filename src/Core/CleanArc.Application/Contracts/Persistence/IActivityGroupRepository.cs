using CleanArc.Domain.Entities.ActivityGroup;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IActivityGroupRepository:IRepository<ActivityGroup>
{
   // Task CreateAgeType(AgeType ageType);
}
