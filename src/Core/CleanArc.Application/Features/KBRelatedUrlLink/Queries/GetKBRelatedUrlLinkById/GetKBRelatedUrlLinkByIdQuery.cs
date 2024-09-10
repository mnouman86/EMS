using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.KBRelatedUrlLink.Queries.GetKBRelatedUrlLinkById
{
    public class GetKBRelatedUrlLinkByIdQuery:IRequest<OperationResult<GetKBRelatedUrlLinkByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
