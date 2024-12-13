using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.FAQs.Queries.GetFAQsById
{
    public class GetFAQsByIdQuery:IRequest<OperationResult<GetFAQsByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
