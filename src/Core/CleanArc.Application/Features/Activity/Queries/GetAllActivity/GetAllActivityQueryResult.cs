using CleanArc.Domain.Entities.SearchHotelRoomDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Activity.Queries.GetAllActivity;

public class GetAllActivityQueryResult
{

    public int Id { get; set; }
    public int BusinessId { get; set; }
    public string Title { get; set; }
    //public int? LanguageLookUpID { get; set; }
    //public string? LanguageName { get; set; }
    //public int? ServiceLookUpID { get; set; }
    //public int? SubServiceLookUpID { get; set; }
    //public string? OtherSubService { get; set; }
    //public int? MinAge { get; set; }
    //public int? MaxAge { get; set; }
    //public int? ActivityTypeLookUpID { get; set; }
    //public int? ActivityNatureLookUpID { get; set; }
    //// public int? MinGroupSize { get; set; }
    //public int? MaxGroupSize { get; set; }
    //public bool? IsPrivateActivity { get; set; }
    ////public int? PrivateParticipantLookUpID { get; set; }
    //public string WhoCanParticipate { get; set; }
    //public string WhoCannotParticipate { get; set; }
    //public int? ManageActivityLookUpID { get; set; }
    //public string? OtherManageActivity { get; set; }
    //public int? Days { get; set; }
    //public int? Hours { get; set; }
    //public string Description { get; set; }
    //public bool? IsTransportation { get; set; }
    //public int? TransportationLookUpID { get; set; }
    //public bool? IsDisability { get; set; }
    //public string AllowedItems { get; set; }
    //public string NotAllowedItems { get; set; }
    //public int? CurrencyLookUpID { get; set; }
    //public string? SeasonLookUpID { get; set; }
    //public string? IncludeOptionLookUpID { get; set; }
    //public string? DisabilityOptionLookUpID { get; set; }
    //public int? CultureId { get; set; }
    //public int? Code { get; set; }
    //public string? Message { get; set; }
    //public int? ActivityID { get; set; }
   // public Decimal? PerPersonPrice { get; set; }

	public Decimal? ActivityPrice { get; set; }

	public string? Duration { get; set; }
	// public int? PerGroupPrice { get; set; }
	public bool? IsActive { get; set; }
    public bool? IsDeleted { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? Cancellation { get; set; }
	public string? StartTime { get; set; } // Nullable TimeSpan for StartTime
	public string? EndTime { get; set; }   // Nullable TimeSpan for EndTime
	public DateTime? StartDate { get; set; } // Nullable DateTime for StartDate
	public DateTime? EndDate { get; set; }  // Nullable DateTime for EndDate

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
