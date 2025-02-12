using CleanArc.Application.Contracts;
using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Features.Users.Commands.Create;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Jwt;
using Mediator;
using Microsoft.Extensions.Logging;
using CleanArc.Domain.Common;
using CleanArc.Application.Contracts.Persistence;

namespace CleanArc.Application.Features.Admin.Queries.GetToken;

public class AdminGetTokenQueryHandler : IRequestHandler<AdminGetTokenQuery, OperationResult<AccessToken>>
{
    private readonly IAppUserManager _userManager;
    private readonly IJwtService _jwtService;
    private readonly ILogger<AdminGetTokenQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public AdminGetTokenQueryHandler(
        IAppUserManager userManager,
        IJwtService jwtService,
        ILogger<AdminGetTokenQueryHandler> logger,
        IUnitOfWork unitOfWork) // Injecting UnitOfWork
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _logger = logger;
        _unitOfWork = unitOfWork; // Assigning UnitOfWork
    }

    public async ValueTask<OperationResult<AccessToken>> Handle(AdminGetTokenQuery request, CancellationToken cancellationToken)
    {
        string methodName = "AdminGetTokenQueryHandler";
        _logger.LogInformation("Handler Started: {@methodName}, Query Request: {@request}",methodName,request);
        var user = await _userManager.GetByUserName(request.UserName);
        _logger.LogInformation("GetByUserName from {@methodName}, Response: {@user}", methodName, user);

        if (user is null)
            return OperationResult<AccessToken>.FailureResult("User not found");

        // Check if user is locked out
        var isUserLockedOut = await _userManager.IsUserLockedOutAsync(user);
        _logger.LogInformation("User locked out checked from {@methodName}, Response: {@isUserLockedOut}", methodName, isUserLockedOut);

        if (isUserLockedOut)
            if (user.LockoutEnd != null)
                return OperationResult<AccessToken>.FailureResult(
                    $"User is locked out. Try in {(user.LockoutEnd - DateTimeOffset.Now).Value.Minutes} Minutes");

        // Validate password
        var passwordValidator = await _userManager.AdminLogin(user, request.Password);

        if (!passwordValidator.Succeeded)
        {
            // Increment access failed count
            var lockoutIncrementResult = await _userManager.IncrementAccessFailedCountAsync(user);
            return OperationResult<AccessToken>.FailureResult("Password is not correct");
        }

        // Generate token
        var token = await _jwtService.GenerateAsync(user);
        _logger.LogInformation("Token generated from {@methodName}, Response: {@token}", methodName, token != null);

        // Add reward for successful login
        var result = await _unitOfWork.UserAssignRewardsRepository.AddAsync(new Domain.Entities.UserAssignRewards.UserAssignRewards
        {
            UserID = user.Id,
            RoleID = user.RoleId,
            RewardRulesID = 2
        });

        if (result != null) // Assuming AddAsync returns the entity or a success indicator
        {
            await _unitOfWork.CommitAsync();
            _logger.LogInformation("User signup reward added for user: {UserID}", user.Id);
        }
        else
        {
            _logger.LogWarning("Failed to add signup reward for user: {UserID}", user.Id);
        }

        return OperationResult<AccessToken>.SuccessResult(token);
    }
}
