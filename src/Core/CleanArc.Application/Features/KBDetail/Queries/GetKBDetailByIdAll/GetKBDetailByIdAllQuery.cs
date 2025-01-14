using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.KBDetail.Queries.GetKBDetailByIdAll
{
    public class GetKBDetailByIdAllQuery:IRequest<OperationResult<GetKBDetailByIdAllQueryResult>>
    {
                public int Id { get; set; }

}
}
