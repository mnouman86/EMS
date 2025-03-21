using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.AdvertisementPlace
{
    public class CreateAdvertisementPlaceDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? CreatedBy { get; set; }
        public int? CultureId { get; set; }


    }
}
