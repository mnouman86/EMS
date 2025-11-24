using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.RatePlanType
{
    public class CreateRatePlanTypeDTO
    {
       public int RoomTypeId { get; set; }
        public int GuestQuantity { get; set; }
        public decimal DefaultRate { get; set; } = decimal.Zero;
        public string RatePlanName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? CreatedBy { get; set; }
        public int? CultureId { get; set; }
    }
}
