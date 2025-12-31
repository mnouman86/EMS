using System.Security.Claims;
using CleanArc.Application.Models.Jwt;
using CleanArc.Domain.Entities.User;

namespace CleanArc.Application.Contracts;

public interface IJwtService
{
    Task<AccessToken> GenerateAsync(User user, bool RememberMe);
    Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
    Task<AccessToken> GenerateByPhoneNumberAsync(string phoneNumber, bool RememberMe);
    Task<AccessToken> RefreshToken(Guid refreshTokenId, bool RememberMe);
}