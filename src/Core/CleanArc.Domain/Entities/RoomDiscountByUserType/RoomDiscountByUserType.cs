using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.RoomDiscountByUserType;

public class RoomDiscountByUserType
{
	public int? Id { get; set; }
	public int? GenericTitleId { get; set; }
	public int? RoomDetailId { get; set; }
	public string? RatePlanTypeIds { get; set; }
	public string? UserTypeIds { get; set; }
	public bool? IsActive { get; set; }
	public bool? IsDeleted { get; set; }
	public int? CreatedBy { get; set; }
	public DateTime? CreatedAt { get; set; }
	public int? UpdatedBy { get; set; }
	public DateTime? UpdatedAt { get; set; }
	public int? CultureId { get; set; }

}
