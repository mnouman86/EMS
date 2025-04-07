using CleanArc.Domain.Entities.ActivitySupervisor;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IActivitySupervisorRepository:IRepository<ActivitySupervisor>
{
   // Task CreateAgeType(AgeType ageType);
}
