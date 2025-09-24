using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Persistence.Configuration.FlightsConfig
{
    public class MosafirOptions
    {
        public string BaseUrl { get; set; } = "https://mosafir.pk/";
        public int TimeoutSeconds { get; set; } = 30;
        public string? ApiKey { get; set; }    // optional if Mosafir requires it
    }

}
