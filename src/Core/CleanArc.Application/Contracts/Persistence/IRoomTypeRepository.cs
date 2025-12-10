using CleanArc.Application.Common;
using CleanArc.Application.Features.Activity.Queries.GetActivityCheckoutDetail;
using CleanArc.Application.Features.RoomType.Queries.GetRoomTypeByHotelId;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.RoomType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public interface IRoomTypeRepository:IRepository<RoomType>
{
	Task<ListResponseWrapper<RoomType>> GetRoomTypeDetailByHotelAsync(RoomTypeByHotelSearchRequest searchRequest);
}
