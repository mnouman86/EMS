using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.ActivityPricePerParticipant;

public  class ActivityPricePerParticipant
    
{
    public int ID { get; set; }
    public int? ActivityID { get; set; }
    public Decimal? PerParticipationPrice { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsDeleted { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? CultureId { get; set; }
    public int? Code { get; set; }
    public int? Message { get; set; }

}
