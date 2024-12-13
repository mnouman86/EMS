using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.HomeSlider.Queries.GetHomeSliderById;

public class GetHomeSliderByIdQuery: IRequest<OperationResult<GetHomeSliderByIdQueryResult>>
{
    public int Id { get; set; }

}

