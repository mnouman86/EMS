using CleanArc.Application.Features.SearchHotelAmenities.Queries.GetSearchHotelAmenitiesById;
using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.SearchRoomAmenities.Queries.GetSearchRoomAmenitiesById;

public class GetSearchRoomAmenitiesByIdQuery : IRequest<OperationResult<GetSearchRoomAmenitiesByIdQueryResult>>
{
    public int Id { get; set; }

}

