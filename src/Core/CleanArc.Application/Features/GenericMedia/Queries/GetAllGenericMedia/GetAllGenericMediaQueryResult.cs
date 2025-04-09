using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.GenericMedia.Queries.GetAllGenericMedia
{
    public class GetAllGenericMediaQueryResult
    {
        public int Id { get; set; }
        public int? GenericTitleId { get; set; }
        public string? ImagePath { get; set; } // NVARCHAR(MAX)
        public string? ImageTitle { get; set; } // NVARCHAR(MAX)
        public bool? IsMain { get; set; } // BIT
        public DateTime? CreatedAt { get; set; } // DATETIME
    }
}
