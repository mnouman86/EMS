using CleanArc.Application.Features.GenericMedia.Queries.GetAllGenericMedia;
using CleanArc.Domain.Entities.Amenity;
using CleanArc.Domain.Entities.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.CarDetail.Queries.GetCarDetailById
{
    public class GetCarDetailByIdQueryResult
    {

        public int Id { get; set; }
        public int BusinessId { get; set; }
        public int? ServiceTypeEnumId { get; set; }
        public int? ServiceCategoryId { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? NoOfDays { get; set; }
        public List<LanguageLookUp>? Languages { get; set; }
        public List<AmenityLookUp>? Amenities { get; set; }
        public bool? IsPartiallyRefundable { get; set; }
        public bool? IsFullyRefundable { get; set; }
        public int? ReviewsCount { get; set; }
        public int? Rating { get; set; }
        public string Model { get; set; }
        public string TransmissionType { get; set; }
        public string Year { get; set; }
        public string Status { get; set; }
        public string VehicleIdentificationNumber { get; set; }
        public string PlateNumber { get; set; }
        public int NoOfSeat { get; set; }
        //public int RentPrice { get; set; }
        public int? RentPrice { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        //public bool IsRefundable { get; set; }
        //public bool IsCancelation { get; set; }
        public string? About { get; set; }
        public string? RefundPolicy { get; set; }
        public string? NonRefundPolicy { get; set; }
        public string? CancellationPolicy { get; set; }
        public int? VehicleTypeLookUpId { get; set; } 
        public int? ManufacturerLookUpId { get; set; } 
        public int? DrivingAvailabilityOptionLookUpId { get; set; } 
        public decimal? PerHourPrice { get; set; }

        public string? VehicleTypeName { get; set; }
        public string? DrivingAvailabilityOptionName { get; set; }
        public int CarId { get; set; }
        public string? Name { get; set; }
        public string? CountryName { get; set; }
        public string? StateName { get; set; }
        public string? CityName { get; set; }
        public int? CountryLookUpID { get; set; }
        public int? StateLookUpID { get; set; }
        public int? CityLookUpID { get; set; }
        public string? PostalCode { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? MobileNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public IEnumerable<GetAllGenericMediaQueryResult> Medias { get; set; }

    }
}
