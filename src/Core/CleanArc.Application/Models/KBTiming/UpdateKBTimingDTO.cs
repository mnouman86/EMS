using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.KBTiming
{
    public class UpdateKBTimingDTO: Availability
    {
        public int ID { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
