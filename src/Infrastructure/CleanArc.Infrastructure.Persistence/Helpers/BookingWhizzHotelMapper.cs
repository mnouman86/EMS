using CleanArc.Domain.Entities.SearchHotelDetail;
using CleanArc.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CleanArc.Infrastructure.Persistence.Helpers
{
    //Helper to map XML to SearchDetail
    public static class BookingWhizzHotelMapper
    {
        public static SearchDetail MapHotelFromXml(XElement element)
        {
            try
            {
                return new SearchDetail
                {                    
                    Id = int.Parse(element.Element("MinRoomId")?.Value ?? "0"),
                    GenericTitleId = int.Parse(element.Element("AccommodationId")?.Value ?? "0"),
                    HotelID = int.Parse(element.Element("AccommodationId")?.Value ?? "0"),
                    Name = element.Element("AccommodationName")?.Value,
                    CityID = int.TryParse(element.Element("CityId")?.Value, out var cityId) ? cityId : 0,
                    CityName = element.Element("CityName")?.Value,
                    RoomTypeName = element.Element("MinRoomName")?.Value,
                    RoomDetailPrice = decimal.TryParse(element.Element("MinRate")?.Value, out var price) ? price : 0,
                    DiscountedPrice = decimal.TryParse(element.Element("MinRate")?.Value, out var discPrice) ? discPrice : 0,
                    Description = element.Element("GeneralDescription")?.Value,
                    ImagePath = element.Element("ImageURL")?.Value,
                    Rating = int.TryParse(element.Element("Rating")?.Value, out var rating) ? rating : 0,
                    RefundPolicy = element.Element("CancellationDescription")?.Value,
                    Longitude = decimal.TryParse(element.Element("Longitude")?.Value, out var lng) ? lng : null,
                    Latitude = decimal.TryParse(element.Element("Latitude")?.Value, out var lat) ? lat : null,
                    StartDate = DateTime.TryParse(element.Element("StartDate")?.Value, out var start) ? start : null,
                    EndDate = DateTime.TryParse(element.Element("EndDate")?.Value, out var end) ? end : null,
                    Provider=APIProvider.BookingWhizz,
                    //Amenities = element.Element("AFacilityName")?.Value?.Split(',').Select((val, i) => new Domain.Entities.AmenityMapping.AmenityMapping
                    //{
                    //    Id = i,
                    //    Amenity = val.Trim(),
                    //    Icon = val.Trim(),
                    //    Selected = true
                    //}).Take(3).ToList() ?? new List<Domain.Entities.AmenityMapping.AmenityMapping>(),
                    Amenities = element.Element("hotelExtras")?
                    .Elements("Facility")
                    .Select((f, i) => new Domain.Entities.AmenityMapping.AmenityMapping
                    {
                        Id = int.TryParse(f.Attribute("ID")?.Value, out var id) ? id : i,
                        Amenity = f.Attribute("ExtraFacilityName")?.Value ?? "Unknown",
                        Icon = f.Attribute("ExtraFacilityName")?.Value ?? "Unknown",
                        Selected = true
                    }).Take(3).ToList() ?? new List<Domain.Entities.AmenityMapping.AmenityMapping>(),
                    HotelImages = element.Element("AccommodationImages")?.Elements("URL").Select((url, i) => new Domain.Entities.GenericMedia.GenericMedia
                    {
                        Id = i,
                        ImagePath = url.Value,
                        ImageTitle = url.Value.Split('/').Last(),
                        IsMain = i == 0
                    }).ToList() ?? new List<Domain.Entities.GenericMedia.GenericMedia>()
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
