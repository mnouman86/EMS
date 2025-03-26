using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Common
{
    public static class TimeFormats
    {
        public static readonly string[] formats = new[]{
    "h:m tt",     // e.g., 1:5 AM
    "hh:m tt",    // e.g., 01:5 AM
    "h:mm tt",    // e.g., 1:15 AM
    "hh:mm tt"    // e.g., 01:15 AM
        };
    }
}
