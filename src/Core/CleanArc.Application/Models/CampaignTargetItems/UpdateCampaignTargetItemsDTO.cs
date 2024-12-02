using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.CampaignTargetItems
{
    public class UpdateCampaignTargetItemsDTO
    {
        public int ID { get; set; }
        public int? CampaignTargetID { get; set; }
        public string? CampaignItemIDs { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CultureId { get; set; }
        
    }
}
