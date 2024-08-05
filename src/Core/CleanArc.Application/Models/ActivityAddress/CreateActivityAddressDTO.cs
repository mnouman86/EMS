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
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        //public bool? IsActive { get; set; }
        //public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public int? UpdatedBy { get; set; }
        //public DateTime? UpdatedAt { get; set; }

    }
}
