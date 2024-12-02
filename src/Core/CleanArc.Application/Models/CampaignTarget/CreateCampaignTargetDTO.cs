using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.CampaignTarget;

public class CreateCampaignTargetDTO
{
    public int? CampaignID { get; set; }
    public int? GenericTitleID { get; set; }
    public int? ServiceCategoryID { get; set; }
    public int? CreatedBy { get; set; }
    public int? CultureID { get; set; }
}
