using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.Amenity
{
    public class CreateAmenityDTO
    {
      //  public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? ServiceId { get; set; }
        public int? ServiceCategoryId { get; set; }

        public string? Icon { get; set; }
        public int? CreatedBy { get; set; }
        public int? CultureId { get; set; }
    }
}
