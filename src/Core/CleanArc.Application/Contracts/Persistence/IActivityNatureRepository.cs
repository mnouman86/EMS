using CleanArc.Domain.Entities.ActivityNature;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IActivityNatureRepository:IRepository<ActivityNature>
{
   // Task CreateAgeType(AgeType ageType);
}
