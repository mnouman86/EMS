using CleanArc.Application.Features.MappingHotelAmenity.Queries.GetMappingHotelAmenityByID;
using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.MappingRoomAmenity.Queries.GetMappingRoomAmenityById;

public class GetMappingRoomAmenityByIdQuery : IRequest<OperationResult<GetMappingRoomAmenityByIdQueryResult>>
{
    public int Id { get; set; }

}
