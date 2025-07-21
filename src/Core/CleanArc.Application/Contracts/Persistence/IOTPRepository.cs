using CleanArc.Domain.Entities.OTP;
using CleanArc.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence
{
    public interface IOTPRepository
    {
        Task AddAsync(OTP otp);
        Task<OTP?> GetValidOTPAsync(string recipient, int userId, string code, OTPDeliveryMethod deliveryMethod);
        Task SaveChangesAsync();

        // Optional additional methods you might need:
        Task<int> GetOTPAttemptsCountAsync(string recipient, DateTime since);
        Task<bool> HasActiveOTPAsync(string recipient);
        Task InvalidateAllOTPsForPhoneAsync(string recipient);
    }
}
