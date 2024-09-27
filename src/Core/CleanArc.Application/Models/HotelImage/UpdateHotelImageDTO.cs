using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.HotelImage
{
    public class UpdateHotelImageDTO
    {
        public int ID { get; set; }
        //public int? HotelID { get; set; }
        public string? ImagePath { get; set; } // NVARCHAR(MAX)
        public string? ImageTitle { get; set; } // NVARCHAR(MAX)
        public bool? IsMain { get; set; } // BIT
        public int? UpdatedBy { get; set; }
		public int? CultureId { get; set; }
		public int? Code { get; set; }
		public string? Message { get; set; }

	}
}
