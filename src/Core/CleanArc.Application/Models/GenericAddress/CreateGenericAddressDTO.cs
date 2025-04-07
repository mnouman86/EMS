using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.GenericAddress
{
    public class CreateGenericAddressDTO
    {
        // public int Id { get; set; }
        public int? GenericTitleId { get; set; }
        public string? CountryLookUpId { get; set; }
        public string? CityLookUpId { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? StateLookUpId { get; set; }
        public string? PostalCode { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        //public bool? IsActive { get; set; }
        //public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public int? CultureId { get; set; }

    }
}
