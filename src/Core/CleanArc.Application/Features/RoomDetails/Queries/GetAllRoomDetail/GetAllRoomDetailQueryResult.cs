using CleanArc.Application.Features.Amenities.Queries.GetAllAmenities;
using CleanArc.Application.Features.AmenityMapping.Queries.GetAllAmenityMapping;
using CleanArc.Application.Features.GenericMedia.Queries.GetAllGenericMedia;
using CleanArc.Application.Features.Language.Queries.GetAllLanguages;
using CleanArc.Application.Features.OutDoor.Queries.GetAllOutDoor;
using CleanArc.Application.Features.RoomView.Queries.GetAllRoomView;
using CleanArc.Domain.Entities.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.RoomDetails.Queries.GetAllRoomDetail;

public class GetAllRoomDetailQueryResult
{
    public int Id { get; set; }
    public int? GenericTitleId { get; set; }
    public Decimal? DiscountPercentage { get; set; }
    public Decimal? DiscountAmount { get; set; }
    public Decimal? DiscountedPrice { get; set; }

    public string? HotelName { get; set; }
    public string? Description { get; set; }
    public int? RoomTypeLookUpId { get; set; }

    public string? RoomType { get; set; }
    public int? RoomSizeUnitLookUpId { get; set; }

    public string? RoomSizeUnit { get; set; }
    public string? RoomSize { get; set; }
    public bool? IsSharedBathroom { get; set; }
    public bool? IsDealExist { get; set; }
    public bool? IsPartiallyRefundable { get; set; }
    public bool? IsFullyRefundable { get; set; }
    public decimal? Price { get; set; }
    public Decimal? TotalPrice { get; set; }
    public Decimal? RoomDetailPrice { get; set; }
    public int? ReviewsCount { get; set; }
    public int? Rating { get; set; }

    public decimal? AdditionalMatricCharges { get; set; }
    public string? RoomNumber { get; set; }
    public bool IsAvailable { get; set; }
    public bool IsDeleted { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public IEnumerable<GetAllGenericMediaQueryResult> Medias { get; set; }
    
    public List<GetAllAmenityMappingQueryResult> RoomAmenities { get; set; }

}

