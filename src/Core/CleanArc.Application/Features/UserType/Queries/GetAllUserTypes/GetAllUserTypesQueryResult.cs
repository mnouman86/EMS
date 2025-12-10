using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.UserType.Queries.GetAllUserTypes;

public class GetAllUserTypesQueryResult
//(int Id, string Name, string Description, bool IsDeleted, bool IsActive, int CreatedBy, DateTime CreatedAt, int UpdatedBy, DateTime UpdatedAt);
{
	public int? Id { get; set; }
	public string? Title { get; set; }
	public string? Description { get; set; }

	public int? NoOfBookings { get; set; }
	public decimal? DiscountPercentage { get; set; }
	public decimal? DiscountCap { get; set; }
	public bool? IsActive { get; set; }
	public bool? IsDeleted { get; set; }
	public int? CreatedBy { get; set; }
	public DateTime? CreatedAt { get; set; }
	public int? UpdatedBy { get; set; }
	public DateTime? UpdatedAt { get; set; }
	public int? CultureId { get; set; }

}
