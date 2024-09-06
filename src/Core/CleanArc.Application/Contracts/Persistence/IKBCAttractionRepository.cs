using CleanArc.Domain.Entities.KBCAttraction;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IKBCAttractionRepository:IRepository<KBCAttraction>
{
   // Task CreateAgeType(AgeType ageType);
}
