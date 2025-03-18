using CleanArc.Application.Features.SearchHotelAmenities.Queries.GetSearchHotelAmenitiesById;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.SearchRoomAmenities.Queries.GetSearchRoomAmenitiesById;

public record GetSearchRoomAmenitiesByIdQuery(SearchRequestById searchRequestById):
    IRequest<OperationResult<GetSearchRoomAmenitiesByIdQueryResult>>;

