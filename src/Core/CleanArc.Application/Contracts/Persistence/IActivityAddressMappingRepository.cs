using CleanArc.Domain.Entities.ActivityAddressMapping;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IActivityAddressMappingRepository:IRepository<ActivityAddressMapping>
{
   // Task CreateAgeType(AgeType ageType);
}
