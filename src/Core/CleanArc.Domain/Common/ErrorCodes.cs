using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Common
{
    public static class ErrorCodes
    {
        // Authentication & Authorization
        public const string EmailVerificationRequired = "email_verification_required";
        public const string InvalidCredentials = "invalid_credentials";
        public const string IncorrectPassword = "incorrect_password";
        public const string AccessDenied = "access_denied";
        public const string RateLimitExceeded = "rate_limit_exceeded";
        public const string VerificationCodeError = "verification_code_error";
        public const string VerificationCodeNotFound = "verification_code_not_found";

        // Validation
        public const string ValidationError = "validation_error";
        public const string UpdateClaimFail = "update_claim_fail";

        // System
        public const string ServerError = "server_error";
        public const string RoleNotFound = "role_not_found";
        public const string UserNotFound = "user_not_found";
        public const string EmailNotFound = "email_not_found";
    }
}
