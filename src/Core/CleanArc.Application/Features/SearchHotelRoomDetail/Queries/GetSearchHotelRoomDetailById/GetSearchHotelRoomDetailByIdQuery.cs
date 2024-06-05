using CleanArc.Application.Features.SearchHotel.Queries.GetSearchHotelById;
using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.SearchHotelRoomDetail.Queries.GetSearchHotelRoomDetailById;

public class GetSearchHotelRoomDetailByIdQuery : IRequest<OperationResult<GetSearchHotelRoomDetailByIdQueryResult>>
{
    public int Id { get; set; }

}

