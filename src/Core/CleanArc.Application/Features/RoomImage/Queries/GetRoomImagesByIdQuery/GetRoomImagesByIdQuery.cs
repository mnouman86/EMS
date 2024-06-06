using CleanArc.Application.Features.SearchHotelImage.Queries.GetByIdSearchHotelImage;
using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.RoomImage.Queries.GetRoomImagesByIdQuery;

public  record GetRoomImagesByIdQuery : IRequest<OperationResult<GetRoomImagesByIdQueryResult>>
{
    public int Id { get; set; }

}
