using CleanArc.Domain.Entities.AgeType;
using CleanArc.Domain.Entities.FeedbackStatus;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IFeedbackStatusRepository : IRepository<FeedbackStatus>
{
   // Task CreateAgeType(AgeType ageType);
}
