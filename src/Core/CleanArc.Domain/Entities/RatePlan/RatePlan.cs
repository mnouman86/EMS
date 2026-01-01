using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.RatePlan
{
    public class RatePlan
    {
        public int Id { get; set; }
        public int? HotelId { get; set; }
        public int? RoomDetailId { get; set; }
        public int? RatePlanTypeId { get; set; }
        public string RatePlanName { get; set; } = string.Empty;
        public List<RateDetail>? RatesByDate { get; set; }
        public DateTime? RateDate { get; set; }
        public int? AvailableRooms { get; set; }
        public int? GuestQuantity { get; set; }
        public decimal? Rate { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? AfterDiscountAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public string? TaxType { get; set; }

        public bool StopSell { get; set; }
        public int? MinStay { get; set; }
        public int? MaxStay { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
		public int? CultureId { get; set; }
	}

    public class RateDetail
    {
        public DateTime? RateDate { get; set; }
        public decimal? Rate { get; set; }
        public decimal? ConvertedRate { get; set; }
    }
}
