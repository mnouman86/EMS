using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.Request
{
    public class ThingsToDoSearchFilterRequest: SearchRequest
    {
        public string HotelName { get; set; } // NVARCHAR(MAX), NULL by default
        public decimal? MaxPrice { get; set; } // DECIMAL(18,2), NULL by default
        public decimal? MinPrice { get; set; } // DECIMAL(18,2), NULL by default
        public string HotelAmenities { get; set; } // NVARCHAR(MAX), Comma-separated IDs, NULL by default
        public string RoomAmenities { get; set; } // NVARCHAR(MAX), Comma-separated IDs, NULL by default
        public string BathroomAmenities { get; set; } // NVARCHAR(MAX), Comma-separated IDs, NULL by default
        public string RoomView { get; set; } // NVARCHAR(MAX), Comma-separated IDs, NULL by default
        public string RoomFeature { get; set; } // NVARCHAR(MAX), Comma-separated IDs, NULL by default
    }
    
}
