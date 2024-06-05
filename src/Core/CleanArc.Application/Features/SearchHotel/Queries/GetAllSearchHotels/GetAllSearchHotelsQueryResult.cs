using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.SearchHotel.Queries.GetAllSearchHotels;

public class GetAllSearchHotelsQueryResult {
    //public int ID { get; set; }
    public int HotelID { get; set; }
    public int CityID { get; set; }
    public string HotelName { get; set; }
    public string CityName { get; set; }
    public string CityDescription { get; set; }
    public string RoomTypeName { get; set; }
    public string RoomTypeDescription { get; set; }
    public decimal RoomDetailPrice { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal DiscountedPrice { get; set; }
    public string ImagePath { get; set; }
    public string ImageTitle{ get; set; }
}

