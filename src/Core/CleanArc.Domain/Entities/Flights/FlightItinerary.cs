using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.Flights
{
    public class FlightItinerary
    {
        public List<FlightSegment> Segments { get; set; } = new();
        public Price Price { get; set; } = new();
        public string DeeplinkUrl { get; set; } = default!;
    }
}
