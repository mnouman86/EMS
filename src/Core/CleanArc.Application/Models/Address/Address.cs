using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.Address
{
    public class GenericAddress
    {
        public int? CountryLookUpId { get; set; }
        public int? StateLookUpId { get; set; }
        public int? CityLookUpId { get; set; }
        public int? PostalCode { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
    }
}
