using CleanArc.Domain.Entities.KBDetail;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IKBDetailRepository:IRepository<KBDetail>
{
   // Task CreateAgeType(AgeType ageType);
}
