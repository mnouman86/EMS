using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Identity.Identity.Attributes
{
    public class ConditionalEmailVerifiedAttribute : AuthorizeAttribute
    {
        public ConditionalEmailVerifiedAttribute()
        {
            Policy = "EmailVerifiedWhenRoleExists";
        }

        // Optional: Add properties to customize behavior
        //public string[] RequiredRoles { get; set; } = new[] { "User", "Admin", "Vendor" };
    }
}
