using CleanArc.Domain.Entities.GroupActivityParticipants;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IGroupActivityParticipantsRepository:IRepository<GroupActivityParticipants>
{
   // Task CreateAgeType(AgeType ageType);
}
