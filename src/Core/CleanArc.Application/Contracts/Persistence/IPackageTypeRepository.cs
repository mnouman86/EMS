using CleanArc.Domain.Entities.PackageType;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IPackageTypeRepository:IRepository<PackageType>
{
   // Task CreateAgeType(AgeType ageType);
}
