using CleanArc.Domain.Entities.ActivityIDImageMapping;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IActivityIDImageMappingRepository:IRepository<ActivityIDImageMapping>
{
   // Task CreateAgeType(AgeType ageType);
}
