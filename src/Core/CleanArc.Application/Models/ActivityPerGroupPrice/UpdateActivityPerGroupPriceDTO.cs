using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.ActivityPerGroupPrice
{
    public class UpdateActivityPerGroupPriceDTO
    {

      
        public int Id { get; set; }
        public int? MinGroupSize { get; set; }
        public int? MaxGroupSize { get; set; }
        public decimal? Price { get; set; }
        public int? UpdatedBy { get; set; }
       // public DateTime? UpdatedAt { get; set; }
        public int? CultureId { get; set; }
    }
}
