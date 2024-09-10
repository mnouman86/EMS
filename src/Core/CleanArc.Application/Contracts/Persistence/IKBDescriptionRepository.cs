using CleanArc.Domain.Entities.KBDescription;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IKBDescriptionRepository:IRepository<KBDescription>
{
   // Task CreateAgeType(AgeType ageType);
}
