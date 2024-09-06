using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.KBCRelatedUrlLink.Queries.GetKBCRelatedUrlLinkById
{
    public class GetKBCRelatedUrlLinkByIdQuery:IRequest<OperationResult<GetKBCRelatedUrlLinkByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
