using CleanArc.Application.Common;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.Hotel;
using CleanArc.Domain.Entities.KBDetail;
using CleanArc.Domain.Entities.RoomDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public interface IRoomDetailsRepository:IRepository<RoomDetails>
{
    Task<SingleResponseWrapper<HotelDetail>> GetHotelDetailForRoomAsync(HotelDetailSearchRequest searchRequest);
}
