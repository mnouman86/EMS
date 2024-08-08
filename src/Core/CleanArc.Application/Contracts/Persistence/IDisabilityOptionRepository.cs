using CleanArc.Domain.Entities.DisabilityOption;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IDisabilityOptionRepository:IRepository<DisabilityOption>
{
   // Task CreateAgeType(AgeType ageType);
}
