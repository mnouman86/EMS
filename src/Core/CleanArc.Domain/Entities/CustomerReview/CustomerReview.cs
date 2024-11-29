using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.CustomerReview
{
    public class CustomerReview
    {
        public int ID { get; set; }
        public int? GenericTitleID { get; set; }
        //public string? Name { get; set; }
        public int? ServiceCategoryID { get; set; }
        public int? Rating { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public int? ApprovedBy { get; set; }
        public string? ApprovedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CultureId { get; set; }
        public int? Code { get; set; }
        public string? Message { get; set; }

    }
}
