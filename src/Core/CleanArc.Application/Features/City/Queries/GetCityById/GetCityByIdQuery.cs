using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.City.Queries.GetCityById;

public class GetCityByIdQuery : IRequest<OperationResult<GetCityByIdQueryResult>>
{
    public int Id { get; set; }

}
