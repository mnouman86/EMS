using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.GenericMedia
{
    public class UpdateGenericMediaDTO
    {
        public int Id { get; set; }
        //public int? GenericTitleId { get; set; }
        public string? ImagePath { get; set; } // NVARCHAR(MAX)
        //public List<string>? ImagePaths { get; set; }
        public string? ImageTitle { get; set; } // NVARCHAR(MAX)
        public bool? IsMain { get; set; } // BIT
        public int? UpdatedBy { get; set; } // NVARCHAR(100)
        public int? CultureId { get; set; } // NVARCHAR(100)
        //public int? ServiceTypeEnumId { get; set; } // NVARCHAR(100)

    }
}
