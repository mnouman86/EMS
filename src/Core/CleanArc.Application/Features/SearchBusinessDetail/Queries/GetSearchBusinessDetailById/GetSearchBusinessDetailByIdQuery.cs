using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.SearchBusinessDetail.Queries.GetSearchBusinessDetailById
{
    public class GetSearchBusinessDetailByIdQuery : IRequest<OperationResult<GetSearchBusinessDetailByIdQueryResult>>
    {
        public int Id { get; set; }

    }
}
