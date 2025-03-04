using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Bank.Queries.GetAllBank
{
    public class GetAllBankQueryResult
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CountryLookUpID { get; set; }
        public string CountryName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
