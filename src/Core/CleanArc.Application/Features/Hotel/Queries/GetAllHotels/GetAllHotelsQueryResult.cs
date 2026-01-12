using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Hotel.Queries.GetAllHotels;

public class GetAllHotelsQueryResult
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CountryLookUpId { get; set; }
    public int StateLookUpId { get; set; }
    public int CityLookUpId { get; set; }
    public int BusinessId { get; set; }
    public int? ThirdPartyStayId { get; set; }
    public int? Stars { get; set; }
    public string Status { get; set; }
    public string PostalCode { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string MobileNumber { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string FocalPersonName { get; set; }
    public bool IsChanelManager { get; set; }
    public bool IsRating { get; set; }
    public int ServiceId { get; set; }
    public int ServiceCategoryId { get; set; }
    public bool IsChain { get; set; }
    public DateTime? CheckInFrom { get; set; }
    public DateTime? CheckInTo { get; set; }
    public DateTime? CheckOutFrom { get; set; }
    public DateTime? CheckOutTo { get; set; }
    public int? CultureId { get; set; }

    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int UpdatedBy { get; set; }
    public string? About { get; set; }
    public string? RefundPolicy { get; set; }
    public string? NonRefundPolicy { get; set; }
    public string? CancellationPolicy { get; set; }

}