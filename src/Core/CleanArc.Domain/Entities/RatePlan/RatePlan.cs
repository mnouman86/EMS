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
        public int HotelId { get; set; }
        public int RatePlanTypeId { get; set; }
        public DateTime RateDate { get; set; }
        public int AvailableRooms { get; set; }
        public decimal Rate { get; set; }
        public bool StopSell { get; set; }
        public int MinStay { get; set; }
        public int MaxStay { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
		public int? CultureId { get; set; }
	}
}
