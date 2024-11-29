using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.CustomerAwareness.Queries.GetCustomerAwarenessById;

public class GetCustomerAwarenessByIdQuery: IRequest<OperationResult<GetCustomerAwarenessByIdQueryResult>>
{
    public int Id { get; set; }

}

