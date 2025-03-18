using CleanArc.Application.Features.SearchHotel.Queries.GetSearchHotelById;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.SearchHotelAmenities.Queries.GetSearchHotelAmenitiesById
{
    public record GetSearchHotelAmenitiesByIdQuery(SearchRequestById searchRequestById):
        IRequest<OperationResult<GetSearchHotelAmenitiesByIdQueryResult>>;
}
