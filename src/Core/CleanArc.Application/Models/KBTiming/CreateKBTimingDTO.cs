using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.KBTiming
{
    public class CreateKBTimingDTO
    {
        // public int ID { get; set; }
        public int? KBDetailID { get; set; }
        public int? Day { get; set; }
        public string? TimeFrom { get; set; }
        public string? TimeTo { get; set; }
        public bool? IsAlwaysOpen { get; set; }

        //public bool? IsActive { get; set; }
        //public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public int? CultureId { get; set; }
        public int? Code { get; set; }
        public int? Message { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public int? UpdatedBy { get; set; }
        //public DateTime? UpdatedAt { get; set; }

    }
}
