using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.RoomDetails;

public class RoomDetails
{
    public int Id { get; set; }
    public int? GenericTitleId { get; set; }

    public string? HotelName { get; set; }
    public int? RoomTypeLookUpId { get; set; }

    public string? RoomType { get; set; }
    public int? RoomSizeUnitLookUpID { get; set; }

    public string? RoomSizeUnit { get; set; }
    public string? RoomSize { get; set; }
    public bool? IsSharedBathroom { get; set; }
    public bool? IsPartiallyRefundable { get; set; }
    public bool? IsFullyRefundable { get; set; }
    public decimal? Price { get; set; }
    public decimal? AdditionalMatricCharges { get; set; }
    public string? RoomNumber { get; set; }
    public bool? IsAvailable { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsDeleted { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
	public int? CultureId { get; set; }
}
