using CleanArc.Application.Common;
using CleanArc.Application.Models.RatePlan;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.Activity;
using CleanArc.Domain.Entities.RatePlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence
{
    public interface IRatePlanRepository:IRepository<RatePlanRequestDto>
    {
        Task<SingleResponseWrapper<RatePlanResponseDto>> GetAllRatePlansAsync(RatePlanSearchRequest searchRequest);

    }
}
