using CleanArc.Domain.Entities.ActivityIncludeOptionMapping;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IActivityIncludeOptionMappingRepository:IRepository<ActivityIncludeOptionMapping>
{
   // Task CreateAgeType(AgeType ageType);
}
