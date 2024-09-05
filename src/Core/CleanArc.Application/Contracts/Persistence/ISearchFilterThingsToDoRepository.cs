using CleanArc.Domain.Entities.SearchFilterThingsToDo;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.SearchHotelDetail;

namespace CleanArc.Application.Contracts.Persistence;

public  interface ISearchFilterThingsToDoRepository
{
    // Task CreateAgeType(AgeType ageType);
    Task<IReadOnlyList<SearchFilterThingsToDo>> GetAllWithParamAsync(ThingsToDoSearchFilterRequest request);

}
