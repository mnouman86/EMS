using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Bank.Queries.GetBankById
{
    public class GetBankByIdQuery : IRequest<OperationResult<GetBankByIdQueryResult>>
    {
        public int Id { get; set; }

    }
}
