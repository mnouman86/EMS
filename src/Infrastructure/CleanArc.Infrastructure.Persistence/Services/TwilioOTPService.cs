using CleanArc.Application.Contracts.Persistence;
using CleanArc.Domain.Entities.OTP;
using CleanArc.Domain.Enums;
using CleanArc.Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
namespace CleanArc.Infrastructure.Persistence.Services
{
    public class TwilioOTPService : IOTPService, IDisposable
    {
        private readonly string _accountSid;
        private readonly bool _isEnabled=false;
        private readonly string _authToken; 
        private readonly string _twilioPhoneNumber;
        private readonly string _whatsAppFromNumber;
        private readonly int _otpExpirationMinutes;
        private readonly IOTPRepository _otpRepository;
        private readonly IEmailService _emailService;

        public TwilioOTPService(
            IConfiguration configuration,
            IOTPRepository otpRepository, IEmailService emailService)
        {
            bool.TryParse( configuration["Twilio:Enabled"],out _isEnabled);
            _accountSid = configuration["Twilio:AccountSID"];
            _authToken = configuration["Twilio:AuthToken"];
            _twilioPhoneNumber = configuration["Twilio:PhoneNumber"];
            _whatsAppFromNumber = configuration["Twilio:WhatsAppFromNumber"];
            _otpExpirationMinutes = int.Parse(configuration["OTP:ExpirationMinutes"] ?? "5");
            _otpRepository = otpRepository;
            _emailService = emailService;

            TwilioClient.Init(_accountSid, _authToken);
        }

        public async Task<string> GenerateAndSendOTPAsync(string recipient, int userId, OTPDeliveryMethod deliveryMethod)
        {
            // Generate 6-digit OTP
            var otpCode = new Random().Next(100000, 999999).ToString();
            var expiration = DateTime.UtcNow.AddMinutes(_otpExpirationMinutes);

            // Save to database
            var otp = new OTP
            {
                Recipient = recipient,
                UserId = userId,
                Code = otpCode,
                ExpiresAt = expiration,
                DeliveryMethod=deliveryMethod.ToString()
            };

            await _otpRepository.AddAsync(otp);
            await _otpRepository.SaveChangesAsync();

            // Send via WhatsApp
            //var message = await MessageResource.CreateAsync(
            //    body: $"Your verification code is: {otpCode}",
            //    from: new PhoneNumber($"whatsapp:{_whatsAppFromNumber}"),
            //    to: new PhoneNumber($"whatsapp:{phoneNumber}")
            //);
            if (_isEnabled)
            {
                switch (deliveryMethod)
                {
                    case OTPDeliveryMethod.WhatsApp:
                        await SendWhatsAppOTP(recipient, otpCode);
                        break;

                    case OTPDeliveryMethod.SMS:
                        await SendSMSOTP(recipient, otpCode);
                        break;

                    case OTPDeliveryMethod.Email:
                        await SendEmailOTP(recipient, otpCode);
                        break;
                }
            }
            
            return otpCode;
        }
        private async Task SendWhatsAppOTP(string phoneNumber, string otpCode)
        {
            await MessageResource.CreateAsync(
                body: $"Your verification code is: {otpCode}",
                from: new PhoneNumber($"whatsapp:{_whatsAppFromNumber}"),
                to: new PhoneNumber($"whatsapp:{phoneNumber}")
            );
        }

        private async Task SendSMSOTP(string phoneNumber, string otpCode)
        {
            await MessageResource.CreateAsync(
                body: $"Your verification code is: {otpCode}",
                from: new PhoneNumber(_twilioPhoneNumber),
                to: new PhoneNumber(phoneNumber)
            );
        }
        private async Task SendEmailOTP(string email, string otpCode)
        {
            await _emailService.SendEmailAsync(
                   email,
                   "Your Verification Code",
                   $"Your verification code is: {otpCode}");
        }
       
        public async Task<bool> VerifyOTPAsync(string recipient, int userId, string code, OTPDeliveryMethod deliveryMethod)
        {
            var otp = await _otpRepository.GetValidOTPAsync(recipient, userId, code,deliveryMethod);

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
