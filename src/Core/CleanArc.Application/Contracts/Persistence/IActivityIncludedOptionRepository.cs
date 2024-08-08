using CleanArc.Domain.Entities.ActivityIncludedOption;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IActivityIncludedOptionRepository:IRepository<ActivityIncludedOption>
{
   // Task CreateAgeType(AgeType ageType);
}
