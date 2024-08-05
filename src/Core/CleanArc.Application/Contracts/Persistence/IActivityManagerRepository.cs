using CleanArc.Domain.Entities.ActivityManager;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IActivityManagerRepository:IRepository<ActivityManager>
{
   // Task CreateAgeType(AgeType ageType);
}
