using CleanArc.Domain.Entities.PostPaymentStatus;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IPostPaymentStatusRepository:IRepository<PostPaymentStatus>
{
   // Task CreateAgeType(AgeType ageType);
}
