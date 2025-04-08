using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.HotelImage.Queries.GetAllHotelImage
{
    public class GetAllHotelImageQueryResult
    {
        public int Id { get; set; }
        public int? GenericTitleId { get; set; }
        public string? ImagePath { get; set; } // NVARCHAR(MAX)
        public string? ImageTitle { get; set; } // NVARCHAR(MAX)
        public bool? IsMain { get; set; } // BIT
        public DateTime? CreatedAt { get; set; } // DATETIME
    }
}
