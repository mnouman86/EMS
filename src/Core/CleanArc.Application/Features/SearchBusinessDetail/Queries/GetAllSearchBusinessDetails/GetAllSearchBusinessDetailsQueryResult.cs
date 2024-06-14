using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.SearchBusinessDetail.Queries.GetAllSearchBusinessDetails;

public class GetAllSearchBusinessDetailsQueryResult 
{
    //public int ID { get; set; }
    public int TotalDays { get; set; }
    public decimal DiscountedPrice { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal CarDetailPrice { get; set; }
    public decimal CarRentPrice { get; set; }
    public string CarModelYear { get; set; }
    public string CarModel { get; set; }
    public int CityID { get; set; }
    public string BusinessName { get; set; }
    public int BusinessID { get; set; }
    public int CarID { get; set; }
    public List<CleanArc.Domain.Entities.SearchCarImage.SearchCarImage> CarImages { get; set; }

    //public int TotalDays { get; set; }
    //public string? Description { get; set; }
    //public decimal? Longitude { get; set; }
    //public decimal? Latitude { get; set; }

}

