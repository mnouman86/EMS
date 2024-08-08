using CleanArc.Domain.Entities.ActivityTransportation;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IActivityTransportationRepository:IRepository<ActivityTransportation>
{
   // Task CreateAgeType(AgeType ageType);
}
