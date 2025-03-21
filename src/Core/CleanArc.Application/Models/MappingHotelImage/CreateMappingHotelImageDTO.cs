using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.MappingHotelImage
{
    public class CreateMappingHotelImageDTO
    {
        public int? HotelID { get; set; }
        public string? ImagePath { get; set; }
        public List<string>? ImagePaths { get; set; }
        public string? ImageTitles { get; set; }
        public string? IsMains { get; set; }
        public int? CreatedBy { get; set; }
    }
}
