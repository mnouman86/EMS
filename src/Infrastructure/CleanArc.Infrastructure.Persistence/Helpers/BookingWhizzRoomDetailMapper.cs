using CleanArc.Domain.Entities.AmenityMapping;
using CleanArc.Domain.Entities.GenericMedia;
using CleanArc.Domain.Entities.Hotel;
using CleanArc.Domain.Entities.RoomDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CleanArc.Infrastructure.Persistence.Helpers
{
    public static class BookingWhizzRoomDetailMapper
    {
        public static HotelDetail MapHotelDetail(XDocument searchDoc, XDocument detailDoc, XDocument availabilityDoc)
        {
            var hotelElement = searchDoc.Descendants("result")
                .FirstOrDefault();

            if (hotelElement == null)
                return null;

            // Extract Check-in/out from detailDoc
            var detailResult = detailDoc.Descendants("Result").FirstOrDefault();
            var checkInFrom = detailResult?.Element("checkin")?.Element("from")?.Value;
            var checkOutFrom = detailResult?.Element("checkout")?.Element("from")?.Value;

            DateTime? checkInTime = TimeSpan.TryParse(checkInFrom, out var ciTime)
                ? DateTime.Today.Add(ciTime)
                : null;
            DateTime? checkOutTime = TimeSpan.TryParse(checkOutFrom, out var coTime)
                ? DateTime.Today.Add(coTime)
                : null;

            var hotel = new HotelDetail
            {
                Id = int.Parse(hotelElement.Element("AccommodationId")?.Value ?? "0"),
                Name = hotelElement.Element("AccommodationName")?.Value,
                AddressLine1 = hotelElement.Element("Address")?.Value,
                About = hotelElement.Element("GeneralDescription")?.Value,
                RefundPolicy = hotelElement.Element("CancellationDescription")?.Value,
                CityLookUpId = int.TryParse(hotelElement.Element("CityId")?.Value, out var cityId) ? cityId : null,
                Latitude = hotelElement.Element("Latitude")?.Value,
                Longitude = hotelElement.Element("Longitude")?.Value,
                CheckInFrom = checkInTime,
                CheckOutFrom = checkOutTime,
                //Rating = int.TryParse(hotelElement.Element("Rating")?.Value, out var rating) ? rating : null,

                Amenities = hotelElement.Element("hotelExtras")?.Elements("Facility")
                    .Select((f, i) => new AmenityMapping
                    {
                        Id = int.TryParse(f.Attribute("ID")?.Value, out var id) ? id : i,
                        Amenity = f.Attribute("ExtraFacilityName")?.Value,
                        Icon = f.Attribute("ExtraFacilityName")?.Value,
                        Selected = true
                    }).ToList() ?? new List<AmenityMapping>(),

                Medias = hotelElement.Element("AccommodationImages")?.Elements("URL")
                    .Select((url, i) => new GenericMedia
                    {
                        Id = i,
                        ImagePath = url?.Value,
                        ImageTitle = url?.Value.Split('/').Last(),
                        IsMain = i == 0
                    }).ToList() ?? new List<GenericMedia>(),

                Rooms = availabilityDoc.Descendants("HotelRooms")
                    .Select(room => new RoomDetails
                    {
                        Id = int.Parse(room.Element("RoomId")?.Value ?? "0"),
                        RoomType = room.Element("RoomName")?.Value,
                        RoomTypeLookUpId = int.TryParse(room.Element("RoomTypeId")?.Value, out var rtId) ? rtId : null,
                        Description = room.Element("RoomDescription")?.Value,
                        RoomSize = room.Element("RoomSize")?.Value,
                        RoomAmenities = room.Element("RoomFacilityName")?.Value?.Split(',')
                            .Select((a, i) => new AmenityMapping
                            {
                                Id = i,
                                Amenity = a.Trim(),
                                Icon = a.Trim(),
                                Selected = true
                            }).ToList() ?? new List<AmenityMapping>(),
                        Medias = room.Element("RoomImages")?.Elements("RoomImage")
                            .Select((img, i) => new GenericMedia
                            {
                                Id = i,
                                ImagePath = img.Attribute("Photo_Max500")?.Value,
                                ImageTitle = img.Attribute("Photo_Max500")?.Value?.Split('/').Last(),
                                IsMain = i == 0
                            }).ToList() ?? new List<GenericMedia>(),
                        Price = decimal.TryParse(room.Element("RatePlanDetails")?
                            .Element("RatePlans")?
                            .Element("Rate")?.Value, out var rate) ? rate : null,
                        IsAvailable = true,
                        IsActive = true
                    }).ToList()
            };

            return hotel;
        }
    }


}
