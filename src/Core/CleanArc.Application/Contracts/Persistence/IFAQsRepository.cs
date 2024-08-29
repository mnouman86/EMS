using CleanArc.Domain.Entities.FAQs;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IFAQsRepository:IRepository<FAQs>
{
   // Task CreateAgeType(AgeType ageType);
}
