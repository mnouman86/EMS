using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.ActivityGroup
{
    public class CreateActivityGroupDTO
    {

        public int? ActivityID { get; set; }
        public string? From { get; set; } // Assumes the column name is "From"
        public string? To { get; set; }
        public int? Size { get; set; }
        public int? CreatedBy { get; set; }
        public int? CultureId { get; set; }
        public int? Code { get; set; }
        public int? Message { get; set; }

    }
}
