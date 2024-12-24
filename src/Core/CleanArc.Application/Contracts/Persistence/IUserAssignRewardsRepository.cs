using CleanArc.Domain.Entities.UserAssignRewards;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IUserAssignRewardsRepository:IRepository<UserAssignRewards>
{
   // Task CreateAgeType(AgeType ageType);
}
