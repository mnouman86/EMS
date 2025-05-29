using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.CarDetail
{
    public class CreateCarDetailDTO
    {
        //public int Id { get; set; }
        public int? BusinessId { get; set; }
        public int? ServiceTypeEnumId { get; set; }
        public int? ServiceCategoryId { get; set; }
        public string? Model { get; set; }
        public string? TransmissionType { get; set; }
        public string? Year { get; set; }
        public bool? IsPartiallyRefundable { get; set; }
        public bool? IsFullyRefundable { get; set; }
        public string? VehicleIdentificationNumber { get; set; }
        public string? PlateNumber { get; set; }
        public int? NoOfSeat { get; set; }
        public int? RentPrice { get; set; }
        //public bool? IsRefundable { get; set; }
        //public bool? IsCancelation { get; set; }
        // public bool? IsActive { get; set; }
        //  public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public int? CultureId { get; set; }
       // public DateTime? CreatedAt { get; set; }
       // public int? UpdatedBy { get; set; }
       // public DateTime? UpdatedAt { get; set; }
        public string? About { get; set; }
        public string? RefundPolicy { get; set; }
        public string? NonRefundPolicy { get; set; }
        public string? CancellationPolicy { get; set; }
        public int? VehicleTypeLookUpId { get; set; }
        public int? ManufacturerLookUpId { get; set; }
        public int? DrivingAvailabilityOptionLookUpId { get; set; }
        public decimal? PerHourPrice { get; set; }
    }
}
