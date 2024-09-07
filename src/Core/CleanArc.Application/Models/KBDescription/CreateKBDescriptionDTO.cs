using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.KBDescription
{
    public class CreateKBDescriptionDTO
    {
        // public int ID { get; set; }
        public int? KBDetailID { get; set; }
        public string? SubHeading { get; set; }
        public string? Content { get; set; }
        public int? KBContentType { get; set; }

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
