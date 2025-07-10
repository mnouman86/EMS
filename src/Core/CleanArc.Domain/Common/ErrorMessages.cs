using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Common
{
    public static class ErrorMessages
    {
        private static readonly Dictionary<string, string> _messages = new()
    {
        { ErrorCodes.EmailVerificationRequired, "Email verification is required to proceed." },
        { ErrorCodes.InvalidCredentials, "The provided credentials are invalid." },
        { ErrorCodes.IncorrectPassword, "Password is not correct." },
        { ErrorCodes.UserNotFound, "User not found." },
        { ErrorCodes.EmailNotFound, "Email not found." },
        { ErrorCodes.AccessDenied, "You do not have permission to perform this action." },
        { ErrorCodes.ValidationError, "One or more validation errors occurred." },
        { ErrorCodes.ServerError, "An internal server error occurred. Please try again later." },
        { ErrorCodes.RoleNotFound, "Specified role not found." },
        { ErrorCodes.RateLimitExceeded, "Maximum code requests reached. Please try again in 10 minutes." },
        { ErrorCodes.VerificationCodeError, "Failed to generate verification code." },
        { ErrorCodes.VerificationCodeNotFound, "No verification request found for this email." },
        { ErrorCodes.UpdateClaimFail, "Could Not Update Claims for given Role." }
    };

        public static string GetMessage(string errorCode)
        {
            return _messages.TryGetValue(errorCode, out var message)
                ? message
                : "An unknown error occurred.";
        }
    }
}
