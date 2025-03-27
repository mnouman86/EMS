using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.HotelImage
{
    public class Hotel_Image
    {
        public int Id { get; set; }
        public int? GenericTitleId { get; set; }
        public string? ImagePath { get; set; } // NVARCHAR(MAX)
                                             
        public List<string>? ImagePaths { get; set; }
        public string? ImageTitle { get; set; } // NVARCHAR(MAX)
        public bool? IsMain { get; set; } // BIT
        public int? ServiceTypeEnumId { get; set; }
        public int? CultureId { get; set; }
        public bool? IsActive { get; set; } // BIT
        public bool? IsDeleted { get; set; } // BIT
        public int? CreatedBy { get; set; } // NVARCHAR(100)
        public DateTime? CreatedAt { get; set; } // DATETIME
        public int? UpdatedBy { get; set; } // NVARCHAR(100)
        public DateTime? UpdatedAt { get; set; } // DATETIME, Nullable
    }
}
