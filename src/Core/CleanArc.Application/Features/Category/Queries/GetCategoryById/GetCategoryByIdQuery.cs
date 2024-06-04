using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Category.Queries.GetCategoryById;

public class GetCategoryByIdQuery : IRequest<OperationResult<GetCategoryByIdQueryResult>>
{
    public int Id { get; set; }

}
