using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.KBCAttraction.Queries.GetKBCAttractionById
{
    public class GetKBCAttractionByIdQuery:IRequest<OperationResult<GetKBCAttractionByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
