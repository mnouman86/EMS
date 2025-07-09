using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Persistence.Configuration.HotelProvidersConfig
{
    public class BookingWhizzSettings
    {
        public string BaseUrl { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string AgentId { get; set; }
        public string MultiLanguageId { get; set; }
    }
}
