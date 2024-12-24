using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.HotelImage.Queries.GetHotelImageById;

public class GetHotelImageByIdQuery : IRequest<OperationResult<GetHotelImageByIdQueryResult>>
{
    public int Id { get; set; }

}
