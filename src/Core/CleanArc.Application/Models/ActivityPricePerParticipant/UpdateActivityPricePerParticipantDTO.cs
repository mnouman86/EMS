using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.ActivityPricePerParticipant
{
    public class UpdateActivityPricePerParticipantDTO
    {

       // public int Id { get; set; }
        public int? Id { get; set; }
        public Decimal? PerPersonPrice { get; set; }
        //public bool? IsActive { get; set; }
        //public bool? IsDeleted { get; set; }
        // public int? CreatedBy { get; set; }
        //public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CultureId { get; set; }
    }
}
