using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.RoomVisual
{
    public class UpdateRoomVisualDTO
    {
        public int ID { get; set; }
        public int? HotelID { get; set; }
        public int? RoomID { get; set; }
        public int? CategoryID { get; set; }
        public string? ImageTitle { get; set; } // NVARCHAR(MAX)
        public string? ImagePath { get; set; } // NVARCHAR(MAX)
        public bool? IsMain { get; set; } // BIT
        public int? UpdatedBy { get; set; }
     
    }
}
