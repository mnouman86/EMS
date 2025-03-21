using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.PopularItemsVisit
{
    public class UpdatePopularItemsVisitDTO
    {

        public int Id { get; set; }
        //public int? KBDetailID { get; set; }
        public int? UserID { get; set; } // Foreign Key or reference to another table
        public string? PageVisiteUrl { get; set; }
        //public DateTime? DateTime { get; set; }
        public string? SessionDuration { get; set; }
        public int? VisitCount { get; set; }
        //public DateTime? FirstVisitAt { get; set; }
        public DateTime? LastVisitAt { get; set; }
        //public bool? IsActive { get; set; }
        //public bool? IsDeleted { get; set; }
        // public int? CreatedBy { get; set; }
        //public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CultureId { get; set; }
        public int? Code { get; set; }
        public string? Message { get; set; }
        //public DateTime? UpdatedAt { get; set; }
    }
}
