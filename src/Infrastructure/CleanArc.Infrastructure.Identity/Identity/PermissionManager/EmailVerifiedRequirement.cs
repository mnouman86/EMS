using CleanArc.Domain.Common.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Linq.Expressions;

namespace CleanArc.Infrastructure.Identity.Identity.PermissionManager;

public class EmailVerifiedHandler : AuthorizationHandler<EmailVerifiedRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        EmailVerifiedRequirement requirement)
    {
        if (context.User.HasClaim(c => c.Type == "email_verified" && c.Value == "true"))
        {
            context.Succeed(requirement);
        }
        else
        {
            throw new EmailVerificationRequiredException();
        }
        return Task.CompletedTask;
    }
}

public class EmailVerifiedRequirement : IAuthorizationRequirement { }