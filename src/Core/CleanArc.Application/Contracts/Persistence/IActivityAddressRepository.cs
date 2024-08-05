using CleanArc.Domain.Entities.ActivityAddress;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IActivityAddressRepository:IRepository<ActivityAddress>
{
   // Task CreateAgeType(AgeType ageType);
}
