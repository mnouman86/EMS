using CleanArc.Domain.Entities.SubService;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface ISubServiceRepository:IRepository<SubService>
{
   // Task CreateAgeType(AgeType ageType);
}
