using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Settings
{
    public class RateLimitSettings
    {
        public bool Enabled { get; set; } = true;
        public int GlobalLimit { get; set; } = 1;
        public int GlobalWindowInSeconds { get; set; } = 60;
        public Dictionary<string, EndpointLimit> EndpointLimits { get; set; } = new();

        public class EndpointLimit
        {
            public int Limit { get; set; }
            public int WindowInSeconds { get; set; }
        }
    }
}
