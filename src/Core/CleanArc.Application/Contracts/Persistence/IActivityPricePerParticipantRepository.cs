using CleanArc.Domain.Entities.ActivityPricePerParticipant;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IActivityPricePerParticipantRepository:IRepository<ActivityPricePerParticipant>
{
   // Task CreateAgeType(AgeType ageType);
}
