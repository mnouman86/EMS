using CleanArc.Domain.Entities.KBMedia;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IKBMediaRepository:IRepository<KBMedia>
{
   // Task CreateAgeType(AgeType ageType);
}
