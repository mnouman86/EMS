using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.RoomDetails.Queries.GetAllRoomDetail;

public record GetAllRoomDetailQueryResult(int Id, int HotelID, int RoomTypeID, int RoomSizeUnitID, string RoomSize, bool IsBathroomPrivate, decimal Price, decimal AdditionalMatricCharges, string RoomNumber
    , bool IsAvailable, bool IsDeleted, int CreatedBy, DateTime CreatedAt, int UpdatedBy,DateTime UpdatedAt);

