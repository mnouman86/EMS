using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Persistence.Providers.KPlus
{
    public class KPlusOptions
    {
        public string BaseUrl { get; set; } = "http://sandbox.kplus.com.tr/";
        public string ChannelCode { get; set; } = string.Empty;
        public string ChannelPassword { get; set; } = string.Empty;
        public int TimeoutSeconds { get; set; } = 60;
    }
}
