using CleanArc.Application.Features.SearchHotel.Queries.GetSearchHotelById;
using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.CarRentalSearchFilter.Queries.GetCarRentalSearchFilterById;

public class GetCarRentalSearchFilterByIdQuery : IRequest<OperationResult<GetCarRentalSearchFilterByIdQueryResult>>
{
    public int Id { get; set; }

}

