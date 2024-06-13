using CleanArc.Application.Features.SearchHotel.Queries.GetSearchHotelById;
using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.SearchBusinessCarDetail.Queries.GetSearchBusinessCarDetailById;

public class GetSearchBusinessCarDetailByIdQuery : IRequest<OperationResult<GetSearchBusinessCarDetailByIdQueryResult>>
{
    public int Id { get; set; }

}

