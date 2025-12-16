using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.RatePlan
{
    public class RatePlanResponseDto
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; } = string.Empty;
        public List<RoomTypeRatePlanDto> RoomTypes { get; set; } = new();
    }

    public class RoomTypeRatePlanDto
    {
        public int RoomDetailId { get; set; }
        public string RoomTypeName { get; set; } = string.Empty;
        public List<RoomAvailabilityResponseDto> Availabilities { get; set; } = new();

        public List<RatePlanDetailDto> RatePlans { get; set; } = new();
    }

    public class RatePlanDetailDto
    {
        public int RatePlanTypeId { get; set; }
        public string RatePlanName { get; set; } = string.Empty;
        public List<DailyRateResponseDto> DailyRates { get; set; } = new();
    }
    public class RoomAvailabilityResponseDto
    {
        public long? RoomAvailabilityId { get; set; }
        public DateTime RateDate { get; set; }
        public int AvailableRooms { get; set; }
    }

    public class DailyRateResponseDto
    {
        public long? DailyRatePlanId { get; set; }
        public DateTime RateDate { get; set; }
        //public int AvailableRooms { get; set; }
        public decimal Rate { get; set; }
        public bool StopSell { get; set; }
        public int MinStay { get; set; }
        public int MaxStay { get; set; }
    }
}
