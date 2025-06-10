using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Common.Exceptions
{
    public class EmailVerificationRequiredException : UnauthorizedAccessException
    {
        public EmailVerificationRequiredException()
            : base("Email verification required") { }
    }
}
