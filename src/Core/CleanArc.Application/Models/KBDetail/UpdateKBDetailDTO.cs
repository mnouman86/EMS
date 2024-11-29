using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.KBDetail
{
    public class UpdateKBDetailDTO
    {

        public int ID { get; set; }
        public string? Title { get; set; }
       // public int? GenericTitleID { get; set; }  // Maps to GenericTitleID
        public string? KeyDate { get; set; }
        public string? Cost { get; set; }
        public int? ServiceID { get; set; }

        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public int? CountryLookUpID { get; set; }
        public int? CityLookUpID { get; set; }
        public int? StatelookUpID { get; set; }
        public string? PostalCode { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public int? CoreAreaLookupID { get; set; }
       // public int? RelatedUrlLinkLookupID { get; set; }
        public string? RelatedAreasLookupIDs { get; set; }

        public string? Access { get; set; }
        public string? Availablity { get; set; }
        public string? WhenToVisitIDs { get; set; }
        //public bool? IsActive { get; set; }
        //public bool? IsDeleted { get; set; }
        // public int? CreatedBy { get; set; }
        //public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CultureId { get; set; }
        public int? Code { get; set; }
        public string? Message { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        public string Status { get; set; }
        public int? ApprovedBy { get; set; }
        public string Remarks { get; set; }
         public string ApprovedDate { get; set; }
        public bool IsContributed { get; set; }
        public int? ExistingContentID { get; set; }

    }
}
