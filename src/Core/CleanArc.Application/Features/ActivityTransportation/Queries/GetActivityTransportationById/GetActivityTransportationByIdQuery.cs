using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.ActivityTransportation.Queries.GetActivityTransportationById
{
    public class GetActivityTransportationByIdQuery:IRequest<OperationResult<GetActivityTransportationByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
