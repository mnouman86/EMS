using CleanArc.Domain.Entities.AmenityMapping;
using CleanArc.Domain.Entities.GenericMedia;
using CleanArc.Domain.Entities.Hotel;
using CleanArc.Domain.Entities.RatePlan;
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
        public static HotelDetail MapHotelDetail(XElement hotelElement, /*XDocument detailDoc,*/ XDocument availabilityDoc,int? noOfRooms, int? noOfDays)
        {
            //var hotelElement = searchDoc.Descendants("result")
            //    .FirstOrDefault();

            if (hotelElement == null)
                return null;
            //var startDate = hotelElement?.Element("StartDate")?.Value;
            //var endDate = hotelElement?.Element("EndDate")?.Value;
            //DateTime? stDate = DateTime.TryParse(startDate, out var sDate)
            //    ? DateTime.Now
            //    : null;
            //DateTime? enDate = DateTime.TryParse(endDate, out var eDate)
            //    ? DateTime.Now
            //    : null;
            //int noOfDays=(enDate-stDate).Value.Days;
            // Extract Check-in/out from detailDoc
            //var detailResult = detailDoc.Descendants("Result").FirstOrDefault();
            var checkInFrom = hotelElement?.Element("CheckIn")?.Value;
            var checkOutFrom = hotelElement?.Element("CheckOut")?.Value;

            //var checkInTo = detailResult?.Element("checkin")?.Element("to")?.Value;
            //var checkOutTo = detailResult?.Element("checkout")?.Element("to")?.Value;

            DateTime? checkInTime = TimeSpan.TryParse(checkInFrom, out var ciTime)
                ? DateTime.Today.Add(ciTime)
                : null;
            DateTime? checkOutTime = TimeSpan.TryParse(checkOutFrom, out var coTime)
                ? DateTime.Today.Add(coTime)    
                : null;
            //DateTime? checkInTimeTo = TimeSpan.TryParse(checkInTo, out var citTime)
            //   ? DateTime.Today.Add(citTime)
            //   : null;
            //DateTime? checkOutTimeTo = TimeSpan.TryParse(checkOutTo, out var cotTime)
            //    ? DateTime.Today.Add(cotTime)
            //    : null;
            string RefundPolicyString = string.Empty;
            string CancellationPolicyString = string.Empty;
            string NonRefundPolicyString = string.Empty;
            var hotel = new HotelDetail
            {
                Id = int.Parse(hotelElement.Element("AccommodationId")?.Value ?? "0"),
                Name = hotelElement.Element("AccommodationName")?.Value,
                AddressLine1 = hotelElement.Element("Address")?.Value,
                About = hotelElement.Element("GeneralDescription")?.Value,
                RefundPolicy = hotelElement.Element("CancellationDescription")?.Value,//--
                CityLookUpId = int.TryParse(hotelElement.Element("CityId")?.Value, out var cityId) ? cityId : null,
                Latitude = hotelElement.Element("Latitude")?.Value,
                Longitude = hotelElement.Element("Longitude")?.Value,
                CheckInFrom = checkInTime,
                CheckOutFrom = checkOutTime,
                //CheckOutTo = checkOutTimeTo,
                //CheckInTo = checkInTimeTo,
                //Rating = int.TryParse(hotelElement.Element("Rating")?.Value, out var rating) ? rating : null,
                NoOfDays = noOfDays,
                NoOfRooms = noOfRooms,
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
                        HotelName = hotelElement.Element("AccommodationName")?.Value,
                        RoomType = room.Element("RoomName")?.Value,
                        RoomTypeLookUpId = int.TryParse(room.Element("RoomTypeId")?.Value, out var rtId) ? rtId : null,
                        Description = room.Element("RoomDescription")?.Value,
                        RoomSize = new string((room.Element("RoomSize")?.Value ?? "").Where(char.IsDigit).ToArray()),
                        Rating = int.TryParse(hotelElement.Element("UserRating")?.Value, out var rating) ? rating / 2 : 0,
                        HotelRating = int.TryParse(hotelElement.Element("Rating")?.Value, out var hotelRating) ? hotelRating : null,
                        RoomSizeUnit = "Sq. ft",
                        RoomAmenities = room.Element("RoomFacilityName")?.Value?
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select((a, i) => new AmenityMapping
                        {
                            Id = i,
                            Amenity = a.Trim(),
                            Icon = a.Trim(),
                            Selected = true
                        })
                        .Where(x => !string.IsNullOrWhiteSpace(x.Amenity))
                        .ToList()
                        ?? new List<AmenityMapping>(),
                        BookingPolicy = room
                                .Element("RatePlanDetails")?
                                .Elements("RatePlans")?
                                .Select(rp => rp.Element("BookingPolicy")?.Value)
                                .FirstOrDefault(v => !string.IsNullOrWhiteSpace(v)) ?? string.Empty,
                        CancellationPolicy = room
                                .Element("RatePlanDetails")?
                                .Elements("RatePlans")?
                                .Select(rp => rp.Element("CancellationPolicy")?.Value)
                                .FirstOrDefault(v => !string.IsNullOrWhiteSpace(v)) ?? string.Empty,
                        NoShowPolicy = room
                                .Element("RatePlanDetails")?
                                .Elements("RatePlans")?
                                .Select(rp => rp.Element("NoShowPolicy")?.Value)
                                .FirstOrDefault(v => !string.IsNullOrWhiteSpace(v)) ?? string.Empty,
                        Medias = room.Element("RoomImages")?.Elements("RoomImage")
                            .Select((img, i) => new GenericMedia
                            {
                                Id = i,
                                ImagePath = img.Attribute("Photo_Max500")?.Value,
                                ImageTitle = img.Attribute("Photo_Max500")?.Value?.Split('/').Last(),
                                IsMain = i == 0
                            }).ToList() ?? new List<GenericMedia>(),
                        //RatePlans = room.Element("RatePlanDetails")?.Elements("RatePlans")
                        //    .Select((rtp, i) => new RatePlan
                        //    {
                        //        RatePlanTypeId = int.TryParse(rtp.Element("RatePlanId")?.Value, out var rtId) ? rtId : null,
                        //        RatePlanName = rtp.Element("RatePlanName")?.Value,
                        //        AvailableRooms = int.TryParse(rtp.Element("NoOfRoomsAvailable")?.Value, out var ar) ? ar : null,
                        //        Rate = decimal.TryParse(rtp.Element("ConvertedRate")?.Value, out var rt) ? rt : null,
                        //        GuestQuantity = int.TryParse(rtp.Element("MaxPerson")?.Value, out var ms) ? ms : null,
                        //       TaxAmount = decimal.TryParse(rtp.Element("Taxs").Element("Tax")?.Attribute("TaxValue")?.Value, out var tax) ? tax : null,
                        //       TaxType = rtp.Element("Taxs").Element("Tax")?.Attribute("TaxType")?.Value
                        //    }).ToList() ?? new List<RatePlan>(),
                        RatePlans = room.Element("RatePlanDetails")?
                                .Elements("RatePlans")
                                .Select((rtp, i) =>
                                {
                                    // Parse Rate
                                    decimal? rate = decimal.TryParse(
                                        rtp.Element("ConvertedRate")?.Value,
                                        out var rt) ? rt*noOfRooms : null;

                                    // Tax node
                                    var taxElement = rtp.Element("Taxs")?.Element("Tax");

                                    // Parse TaxType
                                    string taxType = taxElement?.Attribute("TaxType")?.Value;

                                    // Parse TaxValue (percentage)
                                    decimal? taxPercent = decimal.TryParse(
                                        taxElement?.Attribute("TaxValue")?.Value,
                                        out var tv) ? tv : null;

                                    // Calculate TaxAmount ONLY if Excluded
                                    decimal? taxAmount = null;
                                    if (taxType == "Excluded" && rate.HasValue && taxPercent.HasValue)
                                    {
                                        taxAmount = (rate.Value * taxPercent.Value) / 100;
                                    }
                                    // ---------- RATE PER DATE ----------
                                    var rateDetails = rtp.Element("RateDetailsByDate")?
                                        .Elements("RatePerDate")
                                        .Select(rpd => new RateDetail
                                        {
                                            RateDate = DateTime.TryParse(
                                                rpd.Attribute("Date")?.Value,
                                                out var dt1) ? dt1 : null,

                                            Rate = decimal.TryParse(
                                                rpd.Attribute("Rate")?.Value,
                                                out var r) ? r : null,

                                            ConvertedRate = decimal.TryParse(
                                                rpd.Attribute("ConvertedRate")?.Value,
                                                out var cr) ? cr*noOfRooms : null
                                        })
                                        .ToList() ?? new List<RateDetail>();
                                    return new RatePlan
                                    {
                                        RatePlanTypeId = int.TryParse(rtp.Element("RatePlanId")?.Value, out var rpid) ? rpid : null,
                                        RatePlanName = rtp.Element("RatePlanName")?.Value,
                                        AvailableRooms = int.TryParse(rtp.Element("NoOfRoomsAvailable")?.Value, out var ar) ? ar : null,
                                        Rate = rate,
                                        GuestQuantity = int.TryParse(rtp.Element("MaxPerson")?.Value, out var ms) ? ms : null,
                                        TaxType = taxType,
                                        TaxAmount = taxAmount,
                                        RatesByDate = rateDetails
                                    };
                                }).ToList() ?? new List<RatePlan>(),
                        Price = decimal.TryParse(room.Element("RatePlanDetails")?
                            .Element("RatePlans")?
                            .Element("ConvertedRate")?.Value, out var rate) ? rate * noOfRooms : null,
                        RoomDetailPrice = decimal.TryParse(room.Element("RatePlanDetails")?
                            .Element("RatePlans")?
                            .Element("ConvertedRate")?.Value, out var rate1) ? rate1 * noOfRooms : null,
                        TotalPrice = decimal.TryParse(room.Element("RatePlanDetails")?
                            .Element("RatePlans")?
                            .Element("ConvertedRate")?.Value, out var rate2) ? rate2 * noOfRooms : null,
                        DiscountedPrice = decimal.TryParse(room.Element("RatePlanDetails")?
                            .Element("RatePlans")?
                            .Element("ConvertedRate")?.Value, out var rate3) ? rate3 * noOfRooms : null,
                        IsAvailable = true,
                        IsActive = true
                    }).ToList()
            };
            hotel.RefundPolicy = hotel.Rooms.FirstOrDefault()?.RefundPolicy;
            hotel.NonRefundPolicy = hotel.Rooms.FirstOrDefault()?.NonRefundPolicy;
            hotel.CancellationPolicy = hotel.Rooms.FirstOrDefault()?.CancellationPolicy;
            hotel.BookingPolicy = hotel.Rooms.FirstOrDefault()?.BookingPolicy;
            hotel.NoShowPolicy = hotel.Rooms.FirstOrDefault()?.NoShowPolicy;
            
            return hotel;
        }
    }


}
