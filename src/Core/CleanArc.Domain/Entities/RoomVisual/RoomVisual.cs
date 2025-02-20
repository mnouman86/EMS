using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.RoomVisual
{
    public class RoomVisual
    {
        public int ID { get; set; }
        public int? HotelID { get; set; }
        public int? RoomID { get; set; }
        public int? CategoryID { get; set; }
        public string? ImageTitle { get; set; } // NVARCHAR(MAX)
        public string? ImagePath { get; set; } // NVARCHAR(MAX)
        public List<string>? ImagePaths { get; set; }  // Changed from string? to List<string>?
        public bool? IsMain { get; set; } // BIT
        public bool? IsActive { get; set; } // BIT
        public bool? IsDeleted { get; set; } // BIT
        public int? CreatedBy { get; set; } // NVARCHAR(100)
        public DateTime? CreatedAt { get; set; } // DATETIME
        public int? UpdatedBy { get; set; } // NVARCHAR(100)
        public DateTime? UpdatedAt { get; set; } // DATETIME, Nullable
    }
}

