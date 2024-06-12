using CleanArc.Application.Features.AgeType.Queries.GetAllAgeType;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.BusinessBankAccount.Query.GetAllBusinessBankAccount
{
    public record GetAllBusinessBankAccountQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllBusinessBankAccountQueryResult>>>;
    
}
