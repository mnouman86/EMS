using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.KBAddress
{
    public class UpdateKBAddressDTO
    {

        public int GenericTitleID { get; set; }
        //public int ID { get; set; }
       public string? Cost { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public int? CountryLookUpID { get; set; }
        public int? CityLookUpID { get; set; }
        public int? StatelookUpID { get; set; }
        public string? PostalCode { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? Access { get; set; }
        public string? PhoneNo { get; set; }
        public string? WhenToVisitIDs { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CultureId { get; set; }

    }
}
