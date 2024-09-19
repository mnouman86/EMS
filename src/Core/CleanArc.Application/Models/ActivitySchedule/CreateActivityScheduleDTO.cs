using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.ActivitySchedule
{
    public class CreateActivityScheduleDTO
    {
      
        public int? ActivityID { get; set; }
        public string Title { get; set; }
        public int? CreatedBy { get; set; }
        public int? CultureId { get; set; }
        public int? Code { get; set; }
        public string? Message { get; set; }

    }
}
