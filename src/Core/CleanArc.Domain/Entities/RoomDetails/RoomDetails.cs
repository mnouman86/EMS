using CleanArc.Domain.Entities.FAQs;
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
    public string? Description { get; set; }
    public int? RoomTypeLookUpId { get; set; }

    public string? RoomType { get; set; }
    public int? RoomSizeUnitLookUpId { get; set; }

    public string? RoomSizeUnit { get; set; }
    public string? RoomSize { get; set; }
    public bool? IsSharedBathroom { get; set; }
    public bool? IsPartiallyRefundable { get; set; }
    public bool? IsFullyRefundable { get; set; }
    public decimal? Price { get; set; }
    public Decimal? TotalPrice { get; set; }
    public Decimal? RoomDetailPrice { get; set; }
    public int? ReviewsCount { get; set; }
    public int? Rating { get; set; }

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
    public List<AmenityMapping.AmenityMapping> RoomAmenities { get; set; }
    public List<GenericMedia.GenericMedia> Medias { get; set; }


}
