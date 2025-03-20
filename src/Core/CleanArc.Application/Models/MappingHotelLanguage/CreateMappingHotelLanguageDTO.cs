using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.MappingHotelLanguage
{
    public class CreateMappingHotelLanguageDTO
    {
        public int? HotelId { get; set; }
        public string? AmenitiesIds { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CultureId { get; set; }
    }
}
