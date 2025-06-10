using CleanArc.Application.Contracts.Persistence;
using CleanArc.Domain.Entities.OTP;
using CleanArc.Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
namespace CleanArc.Infrastructure.Persistence.Services
{
    public class TwilioWhatsAppService : IOTPService, IDisposable
    {
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _whatsAppFromNumber;
        private readonly int _otpExpirationMinutes;
        private readonly IOTPRepository _otpRepository;

        public TwilioWhatsAppService(
            IConfiguration configuration,
            IOTPRepository otpRepository)
        {
            _accountSid = configuration["Twilio:AccountSID"];
            _authToken = configuration["Twilio:AuthToken"];
            _whatsAppFromNumber = configuration["Twilio:WhatsAppFromNumber"];
            _otpExpirationMinutes = int.Parse(configuration["OTP:ExpirationMinutes"] ?? "5");
            _otpRepository = otpRepository;

            TwilioClient.Init(_accountSid, _authToken);
        }

        public async Task<string> GenerateAndSendOTPAsync(string phoneNumber, int userId)
        {
            // Generate 6-digit OTP
            var otpCode = new Random().Next(100000, 999999).ToString();
            var expiration = DateTime.UtcNow.AddMinutes(_otpExpirationMinutes);

            // Save to database
            var otp = new OTP
            {
                PhoneNumber = phoneNumber,
                UserId = userId,
                Code = otpCode,
                ExpiresAt = expiration
            };

            await _otpRepository.AddAsync(otp);
            await _otpRepository.SaveChangesAsync();

            // Send via WhatsApp
            var message = await MessageResource.CreateAsync(
                body: $"Your verification code is: {otpCode}",
                from: new PhoneNumber($"whatsapp:{_whatsAppFromNumber}"),
                to: new PhoneNumber($"whatsapp:{phoneNumber}")
            );

            return otpCode;
        }

        public async Task<bool> VerifyOTPAsync(string phoneNumber, int userId, string code)
        {
            var otp = await _otpRepository.GetValidOTPAsync(phoneNumber,userId, code);

            if (otp == null || !otp.IsValid())
                return false;

            otp.IsUsed = true;
            await _otpRepository.SaveChangesAsync();
            return true;
        }

        public void Dispose()
        {
            TwilioClient.Invalidate();
        }
    }
}
