using CleanArc.Application.Common;
using CleanArc.Application.Features.Activity.Queries.GetActivityCheckoutDetail;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.Activity;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IActivityRepository:IRepository<Activity>
{
	// Task CreateAgeType(AgeType ageType);
	Task<SingleResponseWrapper<Activity>> GetActivityCheckoutDetailAsync(GetActivityCheckoutDetailQuery searchRequestById);
}
