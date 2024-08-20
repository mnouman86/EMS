using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.PackageDetail
{
    public class UpdatePackageDetailDTO
    {

        public int ID { get; set; }
        public int? PackageTypeID { get; set; }
        public int? PaymentOrderID { get; set; }
        public decimal? Stay { get; set; }
        public decimal? Car { get; set; }
        public decimal? Flight { get; set; }
        public decimal? ThingsToDo { get; set; }
        //public bool? IsActive { get; set; }
        //public bool? IsDeleted { get; set; }
        //public int? CreatedBy { get; set; }
        //public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
       // public DateTime? UpdatedAt { get; set; }
        public int? CultureId { get; set; }
        public int? Code { get; set; }
        public int? Message { get; set; }
    }
}
