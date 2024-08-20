using CleanArc.Domain.Entities.PackageDetail;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IPackageDetailRepository:IRepository<PackageDetail>
{
   // Task CreateAgeType(AgeType ageType);
}
