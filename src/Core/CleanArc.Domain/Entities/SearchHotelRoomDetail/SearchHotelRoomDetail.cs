using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.SearchHotelRoomDetail;

public class SearchHotelRoomDetail
{
    public int? Id { get; set; }
    public string? ImageTitle { get; set; }
    public string? ImagePath { get; set; }
    public decimal? RoomDetailPrice { get; set; }
    public string? RoomTypeDescription { get; set; }
    public string? RoomTypeName { get; set; }
    public string? CityDescription { get; set; }
    public string? CityName { get; set; }
    public string? HotelName { get; set; }
    public int? CityID { get; set; }
    public int? HotelID { get; set; }
    public List<RoomImage> RoomImages { get; set; }
    public List<RoomAmenities> RoomAmenities { get; set; }
}
public class RoomImage
{
    public string? ImageTitle { get; set; }
    public string? ImagePath { get; set; }
    public int? RoomID { get; set; }
    public bool? IsMain { get; set; }
}
public class RoomAmenities
{
    public string? Amenity { get; set; }
    public string? Description { get; set; }
}
