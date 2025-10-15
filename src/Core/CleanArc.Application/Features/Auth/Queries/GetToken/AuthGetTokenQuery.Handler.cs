using CleanArc.Application.Contracts;
using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.Auth.Queries.GetToken;
using CleanArc.Application.Features.Users.Commands.Create;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Jwt;
using CleanArc.Domain.Common;
using Mediator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

namespace CleanArc.Application.Features.Auth.Queries.GetTokenn;

public class AuthGetTokenQueryHandler : IRequestHandler<AuthGetTokenQuery, AuthTokenResponse>
{
    private readonly IAppUserManager _userManager;
    private readonly IJwtService _jwtService;
    private readonly ILogger<AuthGetTokenQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public AuthGetTokenQueryHandler(
        IAppUserManager userManager,
        IJwtService jwtService,
        ILogger<AuthGetTokenQueryHandler> logger,
        IUnitOfWork unitOfWork) // Injecting UnitOfWork
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _logger = logger;
        _unitOfWork = unitOfWork; // Assigning UnitOfWork
    }

    public async ValueTask<AuthTokenResponse> Handle(AuthGetTokenQuery request, CancellationToken cancellationToken)
    {
        const string expectedGrant = "client_credentials";
        if (!string.Equals(request.GrantType, expectedGrant, StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogError("Unsupported grant_type. Use 'client_credentials'.");
            return null;
        }

        var user = await _userManager.GetByUserName(request.ClientId);

        if (user is null)
            return null;

        // Check if user is locked out
        var isUserLockedOut = await _userManager.IsUserLockedOutAsync(user);
        _logger.LogInformation("User locked out checked from {@methodName}, Response: {@isUserLockedOut}", "Musalik Login", isUserLockedOut);

        if (isUserLockedOut)
            if (user.LockoutEnd != null)
            {
                return null;
            }

        // Validate password
        var passwordValidator = await _userManager.AdminLogin(user, request.ClientSecret);

        if (!passwordValidator.Succeeded)
        {
            // Increment access failed count
            var lockoutIncrementResult = await _userManager.IncrementAccessFailedCountAsync(user);
            _logger.LogError("Incorrect Password.");

            return null;
        }

        // Generate token
        var token = await _jwtService.GenerateAsync(user);
        //{
        await _unitOfWork.CommitAsync();
        if (token is null)
        {
            _logger.LogError("JwtService returned null token for client {clientId}", request.ClientId);
            return null;
        }

        // Map your generated AccessToken -> VendorTokenResponse (the exact shape client expects)
        // If generated is of type AccessToken (your class), map fields directly.
        if (token is AccessToken accessToken)
        {
            var response = new AuthTokenResponse
            {
                token_type = accessToken.token_type ?? "bearer",
                access_token = accessToken.access_token ?? string.Empty,
                expires_in = accessToken.expires_in
            };

            return response;
        }

        // If your GenerateAsync returns JwtSecurityToken instead, construct AccessToken and map it
        //if (token is JwtSecurityToken jwtToken)
        //{
        //    // Create your AccessToken using its constructor that accepts JwtSecurityToken
        //    var createdAccessToken = new AccessToken(jwtToken, refreshToken: string.Empty, loginuserID: 0, loginRoleID: 0);

        //    var response = new AuthTokenResponse
        //    {
        //        token_type = createdAccessToken.token_type ?? "bearer",
        //        access_token = createdAccessToken.access_token ?? string.Empty,
        //        expires_in = createdAccessToken.expires_in
        //    };

        //    return OperationResult<AuthTokenResponse>.SuccessResult(response);
        //}

        // If GenerateAsync returns something else (e.g., string token), handle that case:
        //if (token is string tokenString)
        //{
        //    var expiresIn = _configuration.GetValue<int?>("Auth:VendorTokenExpiresInSeconds") ?? 20;

        //    var response = new AuthTokenResponse
        //    {
        //        token_type = "bearer",
        //        access_token = tokenString,
        //        expires_in = expiresIn
        //    };

        //    return OperationResult<AuthTokenResponse>.SuccessResult(response);
        //}

        // Fallback — unknown type returned from JwtService
        _logger.LogError("JwtService returned unsupported token type {type} for client {clientId}", token.GetType().FullName, request.ClientId);
        return null;
    }
    //private bool VerifyClientSecret(string providedSecret, string storedSecret)
    //{
    //    // NOTE: THIS IS A PLAIN TEXT COMPARISON FOR DEMO ONLY.
    //    // In production: store hashed secrets and verify with a secure algorithm (IPasswordHasher<T> etc).
    //    return string.Equals(providedSecret, storedSecret, StringComparison.Ordinal);
    //}
}
