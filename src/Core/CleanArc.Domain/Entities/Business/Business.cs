using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.Business
{
    public class Business
    {
        public int? ID { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public int? CountryID { get; set; }
        public int? StateID { get; set; }
        public int? CityID { get; set; }
        public string? TaxIdentificationNumber { get; set; }
        public string? License { get; set; }
        public string? ProofOfInsurance { get; set; }
        public int? BankAccountDetailID { get; set; }
        public bool? IsCancelation { get; set; }
        public bool? IsRefundable { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
