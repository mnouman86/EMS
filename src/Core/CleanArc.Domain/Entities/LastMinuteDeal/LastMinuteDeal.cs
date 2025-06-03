using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.LastMinuteDeal
{
    public class LastMinuteDeal
    {
        public int Id { get; set; }
        public int ServiceTypeEnumId { get; set; }
        public int GenericTitleId { get; set; }
        public int FilterCategoryLookUpId { get; set; }
        public int? Priority { get; set; }
        public decimal? Discount { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
		public int? CultureId { get; set; }
        public int? NoOfRooms { get; set; }
        public int? NoOfDays { get; set; }
        public int? HotelID { get; set; }
        public int? CityID { get; set; }
        public String? Name { get; set; }
        public String? CityName { get; set; }
        public String? CityDescription { get; set; }
        public String? RoomTypeName { get; set; }
        public String? RoomTypeDescription { get; set; }
        public Decimal? DiscountPercentage { get; set; }
        public Decimal? DiscountAmount { get; set; }
        public Decimal? DiscountedPrice { get; set; }
        public String? ImagePath { get; set; }
        public String? ImageTitle { get; set; }
        public String? Description { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public int? TotalDays { get; set; }
        public Decimal? TotalPrice { get; set; }
        public Decimal? RoomDetailPrice { get; set; }
        public int? ReviewsCount { get; set; }
        public int? Rating { get; set; }
        public string? RefundPolicy { get; set; }
        public List<GenericMedia.GenericMedia> HotelImages { get; set; }
        public List<AmenityMapping.AmenityMapping> Amenities { get; set; }
        public string? FilterCategoryName { get; set; }
    }
}
