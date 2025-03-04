using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Business.Queries.GetAllBusiness
{
    public class GetAllBusinessQueryResult
    {
        public int? ID { get; set; }
        public int? BusinessTypeID { get; set; }

        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? MobileNumber { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Email { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public int? CountryID { get; set; }
        public int? StateID { get; set; }
        public int? CityID { get; set; }
        public string? TaxIdentificationNumber { get; set; }
        public string? License { get; set; }
        public string? ProofOfInsurance { get; set; }
        public int? BankAccountDetailID { get; set; }
        //public bool? IsCancelation { get; set; }
        //public bool? IsRefundable { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
