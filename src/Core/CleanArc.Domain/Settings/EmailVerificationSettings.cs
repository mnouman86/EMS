using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Settings
{
    public class EmailVerificationSettings
    {
        public bool Enabled { get; set; } = false;
        public int MaxAttempts { get; set; } = 3;
        public int CodeExpiryMinutes { get; set; } = 15;
        public int ConcurrentAttemptsMinutes { get; set; } = 10;
    }
}
