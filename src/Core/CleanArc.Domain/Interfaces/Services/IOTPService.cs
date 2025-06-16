using CleanArc.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Interfaces.Services
{
    public interface IOTPService
    {
        Task<string> GenerateAndSendOTPAsync(string recipient, int userId, OTPDeliveryMethod method);
        Task<bool> VerifyOTPAsync(string recipient, int userId, string code);
    }
}
