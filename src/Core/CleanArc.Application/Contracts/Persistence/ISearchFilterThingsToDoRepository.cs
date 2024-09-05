using CleanArc.Domain.Entities.SearchFilterThingsToDo;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface ISearchFilterThingsToDoRepository:IRepository<SearchFilterThingsToDo>
{
   // Task CreateAgeType(AgeType ageType);
}
