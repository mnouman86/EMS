using CleanArc.Domain.Entities.OTP;
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
        Task<OTP?> GetValidOTPAsync(string phoneNumber, int userId, string code);
        Task SaveChangesAsync();

        // Optional additional methods you might need:
        Task<int> GetOTPAttemptsCountAsync(string phoneNumber, DateTime since);
        Task<bool> HasActiveOTPAsync(string phoneNumber);
        Task InvalidateAllOTPsForPhoneAsync(string phoneNumber);
    }
}
