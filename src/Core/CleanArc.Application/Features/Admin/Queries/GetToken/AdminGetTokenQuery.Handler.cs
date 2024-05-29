using CleanArc.Application.Contracts;
using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Features.Users.Commands.Create;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Jwt;
using Mediator;
using Microsoft.Extensions.Logging;

namespace CleanArc.Application.Features.Admin.Queries.GetToken;

public class AdminGetTokenQueryHandler:IRequestHandler<AdminGetTokenQuery,OperationResult<AccessToken>>
{
    private readonly IAppUserManager _userManager;
    private readonly IJwtService _jwtService;
    private readonly ILogger<AdminGetTokenQueryHandler> _logger;

    public AdminGetTokenQueryHandler(IAppUserManager userManager, IJwtService jwtService, ILogger<AdminGetTokenQueryHandler> logger)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _logger = logger;
    }

    public async ValueTask<OperationResult<AccessToken>> Handle(AdminGetTokenQuery request, CancellationToken cancellationToken)
    {
        string methodName = "AdminGetTokenQueryHandler";
        _logger.LogInformation("Hander Started: {@methodName}, Query Request: {@request}",methodName,request);
        var user = await _userManager.GetByUserName(request.UserName);
        _logger.LogInformation("GetByUserName from {@methodName}, GetByUserName Response: {@user}", methodName, user);


        if (user is null)
            return OperationResult<AccessToken>.FailureResult("User not found");
        
        var isUserLockedOut = await _userManager.IsUserLockedOutAsync(user);
        _logger.LogInformation("User locked out checked from {@methodName}, Response: {@isUserLockedOut}", methodName, isUserLockedOut);

        if (isUserLockedOut)
            if (user.LockoutEnd != null)
                return OperationResult<AccessToken>.FailureResult(
                    $"User is locked out. Try in {(user.LockoutEnd-DateTimeOffset.Now).Value.Minutes} Minutes");

        var passwordValidator = await _userManager.AdminLogin(user, request.Password);


        if (!passwordValidator.Succeeded)
        {
          var lockoutIncrementResult= await _userManager.IncrementAccessFailedCountAsync(user);

            return OperationResult<AccessToken>.FailureResult("Password is not correct");
        }

        var token= await _jwtService.GenerateAsync(user);
        _logger.LogInformation("Token generated from {@methodName}, Response: {@token}", methodName,token!=null?true:false);


        return OperationResult<AccessToken>.SuccessResult(token);
    }
}