using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.KBDetail
{
    public class UpdateKBDetailDTO
    {

        public int ID { get; set; }
        public string? Title { get; set; }
        public string? KeyDate { get; set; }
        public string? Cost { get; set; }
        public int? ServiceID { get; set; }
        public int? CoreAreaLookupID { get; set; }
        public int? RelatedUrlLinkLookupID { get; set; }
        public string? Access { get; set; }
        public string? Availablity { get; set; }
        public string? WhenToVisitIDs { get; set; }
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
