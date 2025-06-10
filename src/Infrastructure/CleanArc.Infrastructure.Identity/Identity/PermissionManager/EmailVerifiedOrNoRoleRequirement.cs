using CleanArc.Domain.Common.Exceptions;
using CleanArc.Infrastructure.Identity.Identity.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace CleanArc.Infrastructure.Identity.Identity.PermissionManager;

public class EmailVerifiedOrNoRoleRequirement : IAuthorizationRequirement { }

public class EmailVerifiedOrNoRoleHandler : AuthorizationHandler<EmailVerifiedOrNoRoleRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EmailVerifiedOrNoRoleHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        EmailVerifiedOrNoRoleRequirement requirement)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var endpoint = httpContext?.GetEndpoint();
        var attribute = endpoint?.Metadata.GetMetadata<ConditionalEmailVerifiedAttribute>();

        //var requiredRoles = attribute?.RequiredRoles ?? new[] { "User", "Admin", "Vendor" };

        var hasAnyRole = context.User.Claims
            .Any(c => c.Type == ClaimTypes.Role);

        // Check if user has any of the specified roles
        //var hasRelevantRole = context.User.Claims
        //    .Any(c => c.Type == ClaimTypes.Role &&
        //           requiredRoles.Contains(c.Value));

        if (!hasAnyRole)
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        if (context.User.HasClaim(c => c.Type == "email_verified" && c.Value == "true"))
        {
            context.Succeed(requirement);
        }
        else
        {
            // Store the reason in HttpContext for the exception handler
            //httpContext.Items["AuthorizationFailureReason"] = "Email verification required";
            //context.Fail();
            //throw new UnauthorizedAccessException("Email verification required");
            throw new EmailVerificationRequiredException();
        }

        return Task.CompletedTask;
    }
}