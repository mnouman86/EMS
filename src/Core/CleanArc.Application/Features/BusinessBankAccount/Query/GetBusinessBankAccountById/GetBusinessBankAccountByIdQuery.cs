using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.BusinessBankAccount.Query.GetBusinessBankAccountById;

public class GetBusinessBankAccountByIdQuery : IRequest<OperationResult<GetBusinessBankAccountByIdQueryResult>>
{
    public int Id { get; set; }
}