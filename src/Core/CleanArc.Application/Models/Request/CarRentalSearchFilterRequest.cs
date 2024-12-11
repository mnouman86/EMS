using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.Request
{
    public class CarRentalSearchFilterRequest : SearchRequest
    {
        public string? Name { get; set; }
        public string? CityName { get; set; }
        public string? CarModel { get; set; }
        public decimal? MaxPrice { get; set; }
        public decimal? MinPrice { get; set; }
        public string CarAmenities { get; set; }

    }

}
