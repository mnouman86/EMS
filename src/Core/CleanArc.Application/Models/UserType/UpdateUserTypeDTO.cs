using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.UserType
{
    public class UpdateUserTypeDTO
    {
		public int? Id { get; set; }
		public string? Title { get; set; }
		public int? NoOfBookings { get; set; }
		public decimal? DiscountPercentage { get; set; }
		public decimal? DiscountCap { get; set; }
		public int? UpdatedBy { get; set; }
		public int? CultureId { get; set; }
	}
}
