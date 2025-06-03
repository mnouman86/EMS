using CleanArc.Domain.Entities.PopularItemsVisit;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArc.Application.Common;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.Hotel;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IPopularItemsVisitRepository:IRepository<PopularItemsVisit>
{
    Task<ListResponseWrapper<PopularItemsCityWise>> GetAllPopularItemsCityWiseCityWiseAsync(SearchRequest searchRequest);

    // Task CreateAgeType(AgeType ageType);
}
