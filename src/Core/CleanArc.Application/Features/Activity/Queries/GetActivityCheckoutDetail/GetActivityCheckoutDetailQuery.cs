using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Activity.Queries.GetActivityCheckoutDetail
{
    public class GetActivityCheckoutDetailQuery:IRequest<OperationResult<GetActivityCheckoutDetailQueryResult>>
    {
                public int Id { get; set; }
		        public int UserId { get; set; }

	}
}
