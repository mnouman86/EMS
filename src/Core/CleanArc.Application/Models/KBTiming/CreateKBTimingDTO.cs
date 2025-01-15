using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.KBTiming
{
    public class CreateKBTimingDTO:Availability
    {
        public int? CreatedBy { get; set; }
    }
}
