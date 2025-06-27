using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.StartupData
{
    public class StartupDataDto
    {
        public bool IsEmailVerificationEnabled { get; set; }
        public int EmailVerificationMaxAttempts { get; set; }
        public int ResendEmailVerificationTime { get; set; }

        public bool IsOtpEnabled { get; set; }
        public int OtpExpirationMinutes { get; set; }
    }
}
