using CleanArc.Domain.Entities.ActivitySeasonMapping;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IActivitySeasonMappingRepository:IRepository<ActivitySeasonMapping>
{
   // Task CreateAgeType(AgeType ageType);
}
