using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.ActivityIncludedOption.Queries.GetActivityIncludedOptionById
{
    public class GetActivityIncludedOptionByIdQuery:IRequest<OperationResult<GetActivityIncludedOptionByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
