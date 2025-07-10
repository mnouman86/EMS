using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Common
{
    public static class SuccessCodes
    {
        public const string UserCreatedWithPendingVerification = "user_created_with_pending_verification";
        public const string VerificationEmailSent = "verification_email_sent";
        public const string ResetPasswordEmailSent = "reset_password_email_sent";
        public const string EmailVerified = "email_verified";
        public const string PasswordChanged = "password_changed";
    }
}
