using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Enums
{
    public enum OTPDeliveryMethod
    {
        None,
        WhatsApp,
        SMS,
        Email,
        SMS_Email,
        WhatsApp_Email,
        SMS_WhatsApp,
        SMS_WhatsApp_Email
    }
}
