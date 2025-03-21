using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.BusinessProfile
{
    public class CreateBusinessProfileDTO
    {
        // public int Id { get; set; }
        public int? CultureId { get; set; }
        public int? BusinessTypeID { get; set; }
        public string FullLegalName { get; set; }
        public string MobileNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public int? CountryID { get; set; }
        public int? CityID { get; set; }
        public int? StateID { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int? ServiceID { get; set; }

        public int? Code { get; set; }
        public string? Message { get; set; }
        //public bool? IsActive { get; set; }
        //public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public int? UpdatedBy { get; set; }
        //public DateTime? UpdatedAt { get; set; }
    }
}
