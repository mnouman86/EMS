using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.CampaignTarget;

public class CreateCampaignTargetItemsDTO
{
    public int? CampaignID { get; set; }
    public int? GenericTitleID { get; set; }
    public int? ServiceCategoryID { get; set; }
    public int? CreatedBy { get; set; }
}
