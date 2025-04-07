using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.ActivitySchedule
{
    public class UpdateActivityScheduleDTO
    {

      
        public int Id { get; set; }
        public string Title { get; set; }
        public int? UpdatedBy { get; set; }
       // public DateTime? UpdatedAt { get; set; }
        public int? CultureId { get; set; }
    }
}
