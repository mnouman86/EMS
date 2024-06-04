using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.SearchHotel.Queries.GetAllSearchHotels;

public record GetAllSearchHotelsQueryResult(int Id,int HotelID, int CityID, String HotelName, String CityName, String CityDescription, String RoomTypeName, String RoomTypeDescription, Decimal RoomDetailPrice);

