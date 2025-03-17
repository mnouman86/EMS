using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.Country
{
    public class UpdateCountryDTO
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CultureId { get; set; }
    }
}
