using CleanArc.Domain.Entities.SearchHotelRoomDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.SearchFilterThingsToDo.Queries.GetAllSearchFilterThingsToDo;

public class GetAllSearchFilterThingsToDoQueryResult
{
    public int? CityID { get; set; }                 // Maps to C.ID AS CityID
    public string? CityName { get; set; }            // ISNULL(C.Name, '''') AS CityName
    public string? Name { get; set; }        // ISNULL(A.Title, '''') AS ActivityName
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

    public List<CleanArc.Domain.Entities.Activity.ActivityIDImageMapping> ActivityImages { get; set; }

public List<CleanArc.Domain.Entities.Activity.ActivityAddressMapping> ActivityAddress { get; set; }

}

//    public GetAllActivityQueryResult(int iD, string name, string description, bool isDeleted, bool isActive, int createdBy, DateTime createdAt, int updatedBy, DateTime updatedAt)
//    {
//        ID = iD;
//        Name = name;
//        Description = description;
//        IsDeleted = isDeleted;
//        IsActive = isActive;
//        CreatedBy = createdBy;
//        CreatedAt = createdAt;
//        UpdatedBy = updatedBy;
//        UpdatedAt = updatedAt;
//    }
//}
