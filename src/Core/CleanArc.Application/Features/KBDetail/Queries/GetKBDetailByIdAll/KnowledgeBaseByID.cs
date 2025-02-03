using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.KBDetail.Queries.GetKBDetailByIdAll
{
    public class KnowledgeBaseByID
    {
        public int? ID { get; set; }
        public int? ServiceCategoryID { get; set; }
        public string ServiceCategory { get; set; }
        public string Title { get; set; }
        public KnowledgeBaseDetail Detail { get; set; }
        public IEnumerable<KnowledgeBaseDescription> Description { get; set; }
        public IEnumerable<KnowledgeBaseAddress> Address { get; set; }
        public IEnumerable<KnowledgeBaseMedia> Media { get; set; }
        public IEnumerable<KnowledgeBaseTiming> Timing { get; set; }
    }
    public class KnowledgeBaseDetail
    {
        public int? ID { get; set; }
        public int? GenericTitleID { get; set; }
        public int? SectionID { get; set; }
        public string Section { get; set; }
        public string KeyDate { get; set; }
        public string Cost { get; set; }
        public string CoreArea { get; set; }
        public int? CoreAreaLookupID { get; set; }
        public string ImagePath { get; set; }
        public string Icon { get; set; }
        public string RelatedAreasLookupIDs { get; set; }
        public string RelatedAreas { get; set; }
        public string Access { get; set; }
        public string Availablity { get; set; }
        public string WhenToVisitIDs { get; set; }
        public string WhenToVisit { get; set; }
        public string Status { get; set; }
        public string ApprovedDate { get; set; }
        public string Remarks { get; set; }
        public string ApprovedBy { get; set; }
        public string IsContributed { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    public class KnowledgeBaseDescription
    {
        public int? ID { get; set; }
        public int? GenericTitleID { get; set; }
        public string SubHeading { get; set; }
        public string Content { get; set; }
        public string KBContentType { get; set; }
        public int? SectionID { get; set; }
        public string Section { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    public class KnowledgeBaseAddress
    {
        public int? ID { get; set; }
        public int? GenericTitleID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int? CountryLookUpID { get; set; }
        public string? Country { get; set; }
        public int? StatelookUpID { get; set; }
        public string State { get; set; }
        public int? CityLookUpID { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string? PhoneNo { get; set; }
        public string? WhenToVisitIDs { get; set; }
        public string? WhenToVisit { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    public class KnowledgeBaseMedia
    {
        public int? ID { get; set; }
        public int? GenericTitleID { get; set; }
        public string MediaType { get; set; }
        public string ImagePath { get; set; }
        public string ImageTitle { get; set; }
        public bool? IsMain { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    public class KnowledgeBaseTiming
    {
        public int? ID { get; set; }
        public int? GenericTitleID { get; set; }
        public string Day { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public bool? IsAlwaysOpen { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
