using CleanArc.Domain.Entities.ActivitySchedule;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IActivityScheduleRepository:IRepository<ActivitySchedule>
{
   // Task CreateAgeType(AgeType ageType);
}
