using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.SearchFilterThingsToDo.Queries.GetSearchFilterThingsToDoById
{
    public class GetSearchFilterThingsToDoByIdQuery:IRequest<OperationResult<GetSearchFilterThingsToDoByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
