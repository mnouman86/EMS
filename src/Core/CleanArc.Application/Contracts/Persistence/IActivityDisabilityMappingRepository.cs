using CleanArc.Domain.Entities.ActivityDisabilityMapping;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IActivityDisabilityMappingRepository:IRepository<ActivityDisabilityMapping>
{
   // Task CreateAgeType(AgeType ageType);
}
