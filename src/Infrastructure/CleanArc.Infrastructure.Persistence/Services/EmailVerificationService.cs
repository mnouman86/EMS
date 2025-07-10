using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.User;
using CleanArc.Domain.Settings;
using CleanArc.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CleanArc.Infrastructure.Persistence.Services
{
    public class EmailVerificationService : IEmailVerificationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;
        private readonly EmailVerificationSettings _settings;
        private readonly EmailSettings _emailSettings;

        //private const int _maxAttempts = 3;
        //private const int _codeExpiryMinutes = 15;

        public EmailVerificationService(
            ApplicationDbContext context,
            IEmailService emailService, IOptions<EmailVerificationSettings> settings, IOptions<EmailSettings> emailSettings)
        {
            _context = context;
            _emailService = emailService;
            _settings=settings.Value;
            _emailSettings = emailSettings.Value;
        }

        public async Task<IdentityResult> GenerateAndSendCodeAsync(string email)
        {
            try
            {
                var verification = await _context.EmailVerificationCodes
                    .FirstOrDefaultAsync(v => v.Email == email);

                // Rate limiting check
                if (verification != null &&
                    verification.RequestCount >= _settings.MaxAttempts &&
                    (DateTime.UtcNow - verification.LastRequestTime)?.TotalMinutes < _settings.ConcurrentAttemptsMinutes)
                {
                    var timeLeft = _settings.ConcurrentAttemptsMinutes - (int)(DateTime.UtcNow - verification.LastRequestTime)?.TotalMinutes;
                    return IdentityResult.Failed(new IdentityError
                    {
                        Code = ErrorCodes.RateLimitExceeded,
                        Description = $"Maximum code requests reached. Please try again in {timeLeft} minutes."
                    });
                }

                // Generate random 6-digit code
                var code = new Random().Next(100000, 999999).ToString();
                if (verification == null)
                {
                    verification = new EmailVerificationCode
                    {
                        Email = email,
                        Code = code,
                        Expiration = DateTime.UtcNow.AddMinutes(_settings.CodeExpiryMinutes),
                        Attempts = 0,
                        IsVerified = false,
                        LastRequestTime = null
                    };
                    _context.EmailVerificationCodes.Add(verification);
                }
                else
                {
                    // Reset request count if last request was more than 10 minutes ago
                    if ((DateTime.UtcNow - verification.LastRequestTime)?.TotalMinutes >= _settings.ConcurrentAttemptsMinutes)
                    {
                        verification.RequestCount = 0;
                    }

                    verification.Code = code;
                    verification.Expiration = DateTime.UtcNow.AddMinutes(_settings.CodeExpiryMinutes);
                    verification.Attempts = 0;
                    verification.IsVerified = false;
                    verification.RequestCount++;
                    verification.LastRequestTime = DateTime.UtcNow;                    
                }

                await _context.SaveChangesAsync();

                // Send email
                await _emailService.SendEmailAsync(
                    email,
                    "Your Verification Code",
                    $"Your verification code is: {code}");

                return IdentityResult.Success ;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = ErrorCodes.VerificationCodeError,
                    Description = $"Failed to generate verification code: {ex.Message}"
                });
            }
        }

        public async Task<IdentityResult> GeneratePasswordResetLinkAsync(string email, string token)
        {
            try
            {
                var resetLink = $"{_emailSettings.BaseUrl}reset-password?email={email}&token={HttpUtility.UrlEncode(token)}";

                // Send email
                await _emailService.SendEmailAsync(
                    email,
                    "Reset Your Password",
                    $"Click here: {resetLink}");

                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = ErrorCodes.VerificationCodeError,
                    Description = $"Failed to generate reset password link: {ex.Message}"
                });
            }
        }

        public async Task<IdentityResult> VerifyCodeAsync(string email, string code)
        {
            try
            {
                var verification = await _context.EmailVerificationCodes
                    .FirstOrDefaultAsync(v => v.Email == email);

                if (verification == null)
                    return IdentityResult.Failed(new IdentityError
                    {
                        Code =ErrorCodes.VerificationCodeError,
                        Description = "No verification request found for this email"
                    });
                //return Result.Failure("No verification request found for this email");

                if (verification.IsVerified)
                    return IdentityResult.Success; // Already verified

                if (verification.Attempts >= _settings.MaxAttempts)
                    return IdentityResult.Failed(new IdentityError
                    {
                        Code = ErrorCodes.VerificationCodeError,
                        Description = "Code expired. Please request a new one."
                    });
                //return Result.Failure("Too many attempts. Please request a new code.");

                if (DateTime.UtcNow > verification.Expiration)
                    return IdentityResult.Failed(new IdentityError
                    {
                        Code = ErrorCodes.VerificationCodeError,
                        Description = "Code expired. Please request a new one."
                    });
                //return Result.Failure("Code expired. Please request a new one.");

                if (verification.Code != code)
                {
                    verification.Attempts++;
                    await _context.SaveChangesAsync();

                    var remainingAttempts = _settings.MaxAttempts - verification.Attempts;
                    return IdentityResult.Failed(new IdentityError
                    {
                        Code =ErrorCodes.VerificationCodeError,
                        Description = $"Invalid code. {remainingAttempts} attempts remaining."
                    });
                    //return Result.Failure(
                    //    $"Invalid code. {remainingAttempts} attempts remaining.");
                }

                // Mark as verified
                verification.IsVerified = true;
                await _context.SaveChangesAsync();

                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                //return Result.Failure($"Verification failed: {ex.Message}");
                return IdentityResult.Failed(new IdentityError
                {
                    Code = ErrorCodes.VerificationCodeError,
                    Description = $"Verification failed: {ex.Message}"
                });
            }
        }
    }
}
