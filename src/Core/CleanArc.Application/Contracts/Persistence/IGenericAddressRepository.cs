using CleanArc.Domain.Entities.GenericAddress;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IGenericAddressRepository:IRepository<GenericAddress>
{
   // Task CreateAgeType(AgeType ageType);
}
