using CleanArc.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Settings
{
    public class OTPSettings
    {
        public bool Enabled { get; set; } = true;
        public int ExpirationMinutes { get; set; } = 5;
        public OTPDeliveryMethod DeliveryMethod { get; set; } = OTPDeliveryMethod.Email;
    }
}
