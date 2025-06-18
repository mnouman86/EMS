using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.NearByLocation
{
    public class NearByLocation
    {
        public int GenericTitleID { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? Title { get; set; }
        public decimal? Distance { get; set; }
        public string? DistanceUnit { get; set; }
	}
}
