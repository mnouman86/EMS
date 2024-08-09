using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Currency.Queries.GetCurrencyById
{
    public class GetCurrencyByIdQuery:IRequest<OperationResult<GetCurrencyByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
