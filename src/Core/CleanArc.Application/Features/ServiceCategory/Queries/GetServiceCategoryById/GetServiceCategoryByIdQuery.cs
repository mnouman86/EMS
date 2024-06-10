using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.ServiceCategory.Queries.GetServiceCategoryById;

public class  GetServiceCategoryByIdQuery : IRequest<OperationResult<GetServiceCategoryByIdQueryResult>>
{
    public int Id { get; set; }

}
