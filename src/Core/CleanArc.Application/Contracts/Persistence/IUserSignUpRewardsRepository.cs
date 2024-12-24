using CleanArc.Domain.Entities.UserSignUpRewards;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IUserSignUpRewardsRepository:IRepository<UserSignUpRewards>
{
   // Task CreateAgeType(AgeType ageType);
}
