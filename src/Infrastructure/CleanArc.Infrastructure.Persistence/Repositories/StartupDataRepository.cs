using CleanArc.Application.Common;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.StartupData;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.OTP;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Persistence.Repositories
{
    public class StartupDataRepository : IStartupDataRepository
    {
        private readonly bool _isEmailVerificationEnabled = false;
        private readonly bool _isOtpEnabled = false;
        private readonly int _emailVerificationMaxAttempts = 1;
        private readonly int _otpExpirationMinutes = 1;
        private readonly int _resendEmailVerificationTime = 30;
        public StartupDataRepository(IConfiguration configuration)
        {
            bool.TryParse(configuration["EmailVerificationSettings:Enabled"], out _isEmailVerificationEnabled);
            bool.TryParse(configuration["OTP:Enabled"], out _isOtpEnabled);
            int.TryParse(configuration["EmailVerificationSettings:MaxAttempts"], out _emailVerificationMaxAttempts);
            int.TryParse(configuration["OTP:ExpirationMinutes"], out _otpExpirationMinutes);
        }
        
        public Task<SingleResponseWrapper<StartupDataDto>> GetStartupDataAsync()
        {
            var result = new StartupDataDto
            {

                IsEmailVerificationEnabled = _isEmailVerificationEnabled,
                EmailVerificationMaxAttempts = _emailVerificationMaxAttempts,
                IsOtpEnabled = _isOtpEnabled,
                OtpExpirationMinutes = _otpExpirationMinutes,
                ResendEmailVerificationTime = _resendEmailVerificationTime
            };
            var startupData = new SingleResponseWrapper<StartupDataDto>
            {
                Data = result,
                Code = 200,
                Message = "Data retrieved successfully"
            };
            return Task.FromResult(startupData);
        }
    }
}
