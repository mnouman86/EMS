using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.RoomDiscountByUserType
{
    public class UpdateRoomDiscountByUserTypeDTO
    {
		public int? Id { get; set; }
		public int? GenericTitleId { get; set; }
		public int? RoomDetailId { get; set; }
		public string? RatePlanTypeIds { get; set; }
		public string? UserTypeIds { get; set; }
		public int? UpdatedBy { get; set; }
		public int? CultureId { get; set; }
	}
}
