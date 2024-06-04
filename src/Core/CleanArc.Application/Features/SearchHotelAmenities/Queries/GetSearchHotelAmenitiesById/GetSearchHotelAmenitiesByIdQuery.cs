using CleanArc.Application.Features.SearchHotel.Queries.GetSearchHotelById;
using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.SearchHotelAmenities.Queries.GetSearchHotelAmenitiesById
{
    public class GetSearchHotelAmenitiesByIdQuery : IRequest<OperationResult<GetSearchHotelAmenitiesByIdQueryResult>>
    {
        public int Id { get; set; }

    }
}
