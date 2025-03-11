using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.SearchHotelRoomDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.SearchFilterThingsToDo;

public  class SearchFilterThingsToDo
{
    public int ID { get; set; }
    public int? CityID { get; set; }                 // Maps to C.ID AS CityID
    public string? CityName { get; set; }            // ISNULL(C.Name, '''') AS CityName
    public string? Title { get; set; }        // ISNULL(A.Title, '''') AS ActivityName
    public string? SeasonName { get; set; }          // ISNULL(LS.Name, '''') AS SeasonName
    public decimal? ActivityPrice { get; set; }      // ISNULL(A.PerPersonPrice, 0) AS ActivityPrice
    public decimal? DiscountPercentage { get; set; } // Maps to DiscountPercentage
    public decimal? DiscountAmount { get; set; }     // Maps to DiscountAmount
    public decimal? DiscountedPrice { get; set; }    // Maps to DiscountedPrice
    public int? Days { get; set; }                   // ISNULL(A.Days, 0) AS Days
    public int? Hours { get; set; }                  // ISNULL(A.Hours, 0) AS Hours
	public string? Duration { get; set; }
	public bool? IsActive { get; set; }
    public bool? IsDeleted { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? CultureId { get; set; }
    public int? Code { get; set; }
    public string? Message { get; set; }
    
    //public string? StartTime { get; set; } // Nullable TimeSpan for StartTime
    //public string? EndTime { get; set; }   // Nullable TimeSpan for EndTime
    //public DateTime? StartDate { get; set; } // Nullable DateTime for StartDate
    //public DateTime? EndDate { get; set; }   // Nullable DateTime for EndDate
    public List<ActivityIDImageMapping> ActivityImages { get; set; }
    public List<ActivityAddressMapping> ActivityAddress { get; set; }

	public string? StartTime { get; set; } // Nullable TimeSpan for StartTime
	public string? EndTime { get; set; }   // Nullable TimeSpan for EndTime



}
public class ActivityIDImageMapping
{
    public string? ImageTitle { get; set; }
    public string? ImagePath { get; set; }
    public int? ActivityID { get; set; }
    public bool? IsMain { get; set; }
}
public class ActivityAddressMapping
{
    public int? ActivityID { get; set; }
    public int? CountryLookUpID { get; set; }
    public int? CityLookUpID { get; set; }
    public string? CityName { get; set; }
    public int? StateLookUpID { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? PostalCode { get; set; }
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
}

