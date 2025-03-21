using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.HotelImage
{
    public class CreateHotelImageDTO
    {
        public int? HotelID { get; set; }
        public string? ImagePath { get; set; } // NVARCHAR(MAX)
        public List<string>? ImagePaths { get; set; }
        public string? ImageTitle { get; set; } // NVARCHAR(MAX)
        public bool? IsMain { get; set; } // BIT
        public int? CreatedBy { get; set; } // NVARCHAR(100)
    }
}
