using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.Request
{
    public class ThingsToDoSearchFilterRequest: SearchRequest
    {
        public string? ActivityName { get; set; }  // Nullable, NVARCHAR(MAX)
        public string? CityName { get; set; }      // Nullable, NVARCHAR(MAX)
        public decimal? MaxPrice { get; set; } // DECIMAL(18,2), NULL by default
        public decimal? MinPrice { get; set; } // DECIMAL(18,2), NULL by default
        public string? ActivityCategory { get; set; }  // Comma-separated IDs, NVARCHAR(MAX)
        public string? SeasonName { get; set; }
    }
    
}
