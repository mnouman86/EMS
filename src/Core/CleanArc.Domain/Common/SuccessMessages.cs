using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Common
{
    public static class SuccessMessages
    {
        private static readonly Dictionary<string, string> _messages = new()
    {
        { SuccessCodes.UserCreatedWithPendingVerification, "User created successfully. A verification code has been sent to your email. Please check your inbox to verify your account." },
        { SuccessCodes.VerificationEmailSent, "An email verification code has been sent to your email. Please check your inbox to verify your account." },
        { SuccessCodes.ResetPasswordEmailSent, "An email with reset password link has been sent to your email. Please check your inbox to reset password." },
        { SuccessCodes.EmailVerified, "Email verified successfully. Your account is now activated." },
        { SuccessCodes.PasswordChanged, "Password has been changed successfully." }
    };

        public static string GetMessage(string successCode)
        {
            return _messages.TryGetValue(successCode, out var message)
                ? message
                : "An unknown error occurred.";
        }
    }
}
