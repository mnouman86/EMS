using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.CampaignTargetItems;

public class CreateCampaignTargetItemsDTO
{
    public int? CampaignTargetID { get; set; }
    public string? CampaignItemIDs { get; set; }
    public int? CreatedBy { get; set; }
}
