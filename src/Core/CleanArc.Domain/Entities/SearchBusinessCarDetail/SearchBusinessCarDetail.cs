using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.SearchBusinessCarDetail;

public class SearchBusinessCarDetail
{
    public int? ID { get; set; }
    public int? BusinessID { get; set; }
    public string? BusinessName { get; set; }
    public int? CityID { get; set; }
    public string? CarModel { get; set; }
    public string? CarModelYear { get; set; }
    public decimal? CarRentPrice { get; set; }
    public decimal? CarDetailPrice { get; set; }
    public decimal? DiscountAmount { get; set; }
    public decimal? DiscountedPrice { get; set; }
    public int? TotalDays { get; set; }


    public List<SearchCarImage> SearchCarImage { get; set; }
    public List<SearchCarAmenities> SearchCarAmenities { get; set; }
}
public class SearchCarImage
{
    public string? ImageTitle { get; set; }
    public string? ImagePath { get; set; }
    public int? RoomID { get; set; }
    public bool? IsMain { get; set; }
}
public class SearchCarAmenities
{
    public string? Amenity { get; set; }
    public string? Description { get; set; }
}
