using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.KBTiming
{
    public class Availability
    {
        public int? GenericTitleID { get; set; }
        public string? Day { get; set; }
        public string? TimeFrom { get; set; }
        public string? TimeTo { get; set; }
        public bool? IsAlwaysOpen { get; set; }
        public int? CultureId { get; set; }

    }
}
