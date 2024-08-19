using CleanArc.Domain.Entities.ProcessOrder;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IProcessOrderRepository:IRepository<ProcessOrder>
{
   // Task CreateAgeType(AgeType ageType);
}
