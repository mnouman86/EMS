using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.PopularItemsVisit;

public  class PopularItemsCityWise
{
    public int CityId { get; set; }
    public Decimal? MinPrice { get; set; }
    public Decimal? MaxPrice { get; set; }
    public int? VisitCount { get; set; }
    public int? ReviewCount { get; set; }
    public int? Rating { get; set; }
    public string? CityName { get;set; }
    public string? CountryName { get;set; }
    public string? ImageTitle { get; set; }
    public string? FilterCategory { get; set; }
    public string? Tag { get; set; }
    public string? ImagePath { get; set; }

}
