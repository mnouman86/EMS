using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.ActivityAddress
{
    public class CreateActivityAddressDTO
    {
        // public int ID { get; set; }
        public int? ServiceID { get; set; }
        public int? GenericAddressID { get; set; }
        public string? CountryLookUpID { get; set; }
        public string? CityLookUpID { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? StateLookUpID { get; set; }
        public string? PostalCode { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        //public bool? IsActive { get; set; }
        //public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public int? CultureId { get; set; }
        public int? Code { get; set; }
        public string? Message { get; set; }

        //public DateTime? CreatedAt { get; set; }
        //public int? UpdatedBy { get; set; }
        //public DateTime? UpdatedAt { get; set; }

    }
}
