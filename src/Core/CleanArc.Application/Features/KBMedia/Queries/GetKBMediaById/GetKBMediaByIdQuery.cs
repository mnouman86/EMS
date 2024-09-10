using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.KBMedia.Queries.GetKBMediaById
{
    public class GetKBMediaByIdQuery:IRequest<OperationResult<GetKBMediaByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
