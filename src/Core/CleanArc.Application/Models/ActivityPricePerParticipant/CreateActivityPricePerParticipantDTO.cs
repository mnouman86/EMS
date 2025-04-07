using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.ActivityPricePerParticipant
{
    public class CreateActivityPricePerParticipantDTO
    {
        // public int Id { get; set; }
        public int? GenericTitleId { get; set; }
        public Decimal? PerParticipationPrice { get; set; }
        //public bool? IsActive { get; set; }
        //public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public int? CultureId { get; set; }

    }
}
