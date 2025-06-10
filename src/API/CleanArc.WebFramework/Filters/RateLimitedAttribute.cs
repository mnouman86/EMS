using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.WebFramework.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class RateLimitedAttribute : Attribute
    {
        public int Limit { get; }
        public int WindowInSeconds { get; }

        public RateLimitedAttribute(int limit, int windowInSeconds)
        {
            Limit = limit;
            WindowInSeconds = windowInSeconds;
        }
    }
}
