using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Advertisement.Queries.GetAllAdvertisement;

public class GetAllAdvertisementQueryResult
{

    public int ID { get; set; }
    public int? PageID { get; set; }
    public string? PageName { get; set; }
    public int? PlaceID { get; set; }
    public string? PlaceName { get; set; }
    public string? ImageTitle { get; set; }
    public string? ImagePath { get; set; }
    public string? Url { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsShow { get; set; }
    public bool? IsDeleted { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool? IsShow {  get; set; }
}

//    public GetAllAdvertisementQueryResult(int iD, string name, string description, bool isDeleted, bool isActive, int createdBy, DateTime createdAt, int updatedBy, DateTime updatedAt)
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
