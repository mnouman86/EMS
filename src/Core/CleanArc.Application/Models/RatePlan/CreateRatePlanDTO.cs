using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.RatePlan
{
    public class CreateRatePlanDTO
    {
        public int HotelId { get; set; }
        public int RatePlanTypeId { get; set; }
        public DateTime RateDate { get; set; }
        public int AvailableRooms { get; set; }
        public decimal Rate { get; set; }
        public bool StopSell { get; set; }
        public int MinStay { get; set; }
        public int MaxStay { get; set; }
        public int? CreatedBy { get; set; }
        public int? CultureId { get; set; }
    }
    public class RatePlanRequestDto
    {
        public int HotelId { get; set; }
        public List<RoomRatePlanDto> RoomRatePlans { get; set; } = new();
        public int? CultureId { get; set; }
    }

    public class RoomRatePlanDto
    {
        public int RoomTypeId { get; set; }
        public int RatePlanTypeId { get; set; }
        public List<DailyRateDto> DailyRates { get; set; } = new();
    }

    public class DailyRateDto
    {
        public DateTime RateDate { get; set; }
        public int AvailableRooms { get; set; }
        public decimal Rate { get; set; }
        public bool StopSell { get; set; }
        public int MinStay { get; set; } = 1;
        public int MaxStay { get; set; } = 30;
    }
}
