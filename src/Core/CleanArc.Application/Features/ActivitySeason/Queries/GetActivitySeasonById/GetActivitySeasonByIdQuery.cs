using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.ActivitySeason.Queries.GetActivitySeasonById
{
    public class GetActivitySeasonByIdQuery:IRequest<OperationResult<GetActivitySeasonByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
