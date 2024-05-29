using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.URL.Queries.GetURLById
{
    public class GetURLByIdQuery : IRequest<OperationResult<GetURLByIdQueryResult>>
    {
        public int Id { get; set; }
    }
}
