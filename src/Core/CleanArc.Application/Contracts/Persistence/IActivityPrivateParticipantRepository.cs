using CleanArc.Domain.Entities.ActivityPrivateParticipant;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IActivityPrivateParticipantRepository:IRepository<ActivityPrivateParticipant>
{
   // Task CreateAgeType(AgeType ageType);
}
