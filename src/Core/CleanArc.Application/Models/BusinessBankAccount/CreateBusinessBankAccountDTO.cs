using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.BusinessBankAccount
{
    public class CreateBusinessBankAccountDTO
    {
        //public int Id { get; set; }
        public string AccountTitle { get; set; }
        public int? BankLookUpId { get; set; }
        public int? BusinessID { get; set; }
        public string IBAN { get; set; }
       // public bool? IsActive { get; set; }
        //public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
       // public DateTime? CreatedAt { get; set; }
        //public int? UpdatedBy { get; set; }
        //public DateTime? UpdatedAt { get; set; }
    }
}
