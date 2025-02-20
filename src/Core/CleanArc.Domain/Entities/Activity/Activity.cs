using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.SearchHotelRoomDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.Activity;

public  class Activity    
{
    public int ID { get; set; }
    public string Title { get; set; }
    public int? LanguageLookUpID { get; set; }
    public string? LanguageName { get; set; }
    public int? ServiceLookUpID { get; set; }
    public int? SubServiceLookUpID { get; set; }
    public string? OtherSubService { get; set; }
    public int? MinAge { get; set; }
    public int? MaxAge { get; set; }
    public int? ActivityTypeLookUpID { get; set; }
    public int? ActivityNatureLookUpID { get; set; }
    public int? MaxGroupSize { get; set; }
    //public bool? IsPrivateActivity { get; set; }
    public string WhoCanParticipate { get; set; }
    public string WhoCannotParticipate { get; set; }
    public int? ManageActivityLookUpID { get; set; }
    public string? OtherManageActivity { get; set; }
    public int? Days { get; set; }
    public int? Hours { get; set; }
    public string Description { get; set; }
    public bool? IsTransportation { get; set; }
    public int? TransportationLookUpID { get; set; }
    public bool? IsDisability { get; set; }
    public string AllowedItems { get; set; }
    public string NotAllowedItems { get; set; }
    public int? CurrencyLookUpID { get; set; }
    public string? SeasonLookUpID { get; set; }
    public string? IncludeOptionLookUpID { get; set; }
    public string? DisabilityOptionLookUpID { get; set; }
    public Decimal? Price { get; set; }
    public Decimal? PerPersonPrice { get; set; }
    public int? TotalParticipant { get; set; }
    public Decimal? ActivityPrice { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsDeleted { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? CultureId { get; set; }
    public int? Code { get; set; }
    public string? Message { get; set; }
    public int? ActivityID { get; set; }
    public string? Duration { get; set; }
    public string? Cancellation { get; set; }
    public string? StartTime { get; set; } // Nullable TimeSpan for StartTime
    public string? EndTime { get; set; }   // Nullable TimeSpan for EndTime
    public DateTime? StartDate { get; set; } // Nullable DateTime for StartDate
    public DateTime? EndDate { get; set; }   // Nullable DateTime for EndDate
    public List<ActivityIDImageMapping> ActivityImages { get; set; }
    public List<ActivityAddressMapping> ActivityAddress { get; set; }



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

