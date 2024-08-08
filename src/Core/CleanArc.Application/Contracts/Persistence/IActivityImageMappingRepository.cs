using CleanArc.Domain.Entities.ActivityImageMapping;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IActivityImageMappingRepository:IRepository<ActivityImageMapping>
{
   // Task CreateAgeType(AgeType ageType);
}
