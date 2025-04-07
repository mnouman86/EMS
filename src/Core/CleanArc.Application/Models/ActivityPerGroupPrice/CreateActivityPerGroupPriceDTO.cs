using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.ActivityPerGroupPrice
{
    public class CreateActivityPerGroupPriceDTO
    {

        public int? GenericTitleId { get; set; }
        public int? MinGroupSize { get; set; }
        public int? MaxGroupSize { get; set; }
        public decimal? Price { get; set; }
        public int? CreatedBy { get; set; }
        public int? CultureId { get; set; }

    }
}
