using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.KBWhenToVisit
{
    public class UpdateKBWhenToVisitDTO
    {

        public int ID { get; set; }
        //public int? KBDetailID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        //public string? URL { get; set; }
        //public bool? IsActive { get; set; }
        //public bool? IsDeleted { get; set; }
        // public int? CreatedBy { get; set; }
        //public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CultureId { get; set; }
        public int? Code { get; set; }
        public int? Message { get; set; }
        //public DateTime? UpdatedAt { get; set; }
    }
}
