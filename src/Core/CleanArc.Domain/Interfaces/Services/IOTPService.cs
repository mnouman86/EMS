using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Interfaces.Services
{
    public interface IOTPService
    {
        Task<string> GenerateAndSendOTPAsync(string phoneNumber, int userId);
        Task<bool> VerifyOTPAsync(string phoneNumber, int userId, string code);
    }
}
