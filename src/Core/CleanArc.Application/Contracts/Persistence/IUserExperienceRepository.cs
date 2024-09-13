using CleanArc.Domain.Entities.UserExperience;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IUserExperienceRepository:IRepository<UserExperience>
{
   // Task CreateAgeType(AgeType ageType);
}
