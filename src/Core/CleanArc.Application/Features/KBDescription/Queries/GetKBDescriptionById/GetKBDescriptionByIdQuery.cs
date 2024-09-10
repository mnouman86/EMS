using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.KBDescription.Queries.GetKBDescriptionById
{
    public class GetKBDescriptionByIdQuery:IRequest<OperationResult<GetKBDescriptionByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
