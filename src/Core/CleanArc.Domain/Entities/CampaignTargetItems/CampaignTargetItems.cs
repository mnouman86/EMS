using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.CampaignTargetItems;

public class CampaignTargetItems
{
    public int ID { get; set; }
    public int? CampaignTargetID { get; set; }
    public string? CampaignItemIDs { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsDeleted { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? CultureId { get; set; }


}
