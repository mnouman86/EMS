using CleanArc.Domain.Entities.CheckProfileStatus;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface ICheckProfileStatusRepository:IRepository<CheckProfileStatus>
{
   // Task CreateAgeType(AgeType ageType);
}
