using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Common.Exceptions
{
    public class RateLimitExceededException : Exception
    {
        public int RetryAfterSeconds { get; }

        public RateLimitExceededException(int retryAfterSeconds)
            : base($"Rate limit exceeded. Please try again in {retryAfterSeconds} seconds.")
        {
            RetryAfterSeconds = retryAfterSeconds;
        }
    }
}
