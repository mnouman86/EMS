using CleanArc.Domain.Entities.Currency;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface ICurrencyRepository:IRepository<Currency>
{
   // Task CreateAgeType(AgeType ageType);
}
