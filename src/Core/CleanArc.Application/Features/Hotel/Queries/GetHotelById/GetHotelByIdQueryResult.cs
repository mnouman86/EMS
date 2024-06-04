using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Hotel.Queries.GetHotelById;

public record GetHotelByIdQueryResult(int Id, string Name, int CountryID, int StateID, int CityID, int ZipCode, string Address1,
string Address2, string Latitude, string Longitude, string MobileNumber, string PhoneNumber, string Email,
string FocalPersonName, bool IsChanelManager, bool IsRating, bool IsChain, DateTime CheckInFrom, DateTime CheckInTo,
DateTime CheckOutFrom, DateTime CheckOutTo, bool IsActive, bool IsDeleted, int CreatedBy, DateTime CreatedAt, DateTime UpdatedAt, int UpdatedBy);
