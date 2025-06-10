using CleanArc.Application.Contracts;
using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.Admin.Queries.GetToken;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.Domain.Enums;
using CleanArc.Domain.Interfaces.Services;
using Mediator;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Admin.Commands.SendOTPCommand
{
    public class SendOTPCommandHandler : IRequestHandler<SendOTPCommand, OperationResult<bool>>
    {
        private readonly IAppUserManager _userManager;
        private readonly IJwtService _jwtService;
        private readonly ILogger<AdminGetTokenQueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOTPService _otpService;
        public SendOTPCommandHandler(IAppUserManager userManager,
        IJwtService jwtService,
        ILogger<AdminGetTokenQueryHandler> logger,
        IUnitOfWork unitOfWork,
        IOTPService otpService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _logger = logger;
            _unitOfWork = unitOfWork; // Assigning UnitOfWork
            _otpService = otpService;
        }

        public async ValueTask<OperationResult<bool>> Handle(SendOTPCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string methodName = "AdminGetTokenQueryHandler";
                _logger.LogInformation("Handler Started: {@methodName}, Query Request: {@request}", methodName, request);
                var user = await _userManager.GetByUserName(request.UserName);
                _logger.LogInformation("GetByUserName from {@methodName}, Response: {@user}", methodName, user);

                if (user is null)
                    return OperationResult<bool>.FailureResult(statusCode: 404, errorCode: ErrorCodes.UserNotFound);

                // Check if user is locked out
                var isUserLockedOut = await _userManager.IsUserLockedOutAsync(user);
                _logger.LogInformation("User locked out checked from {@methodName}, Response: {@isUserLockedOut}", methodName, isUserLockedOut);

                if (isUserLockedOut)
                    if (user.LockoutEnd != null)
                        return OperationResult<bool>.FailureResult(
                            $"User is locked out. Try in {(user.LockoutEnd - DateTimeOffset.Now).Value.Minutes} Minutes");

                // Validate password
                var passwordValidator = await _userManager.AdminLogin(user, request.Password);

                if (!passwordValidator.Succeeded)
                {
                    // Increment access failed count
                    var lockoutIncrementResult = await _userManager.IncrementAccessFailedCountAsync(user);
                    return OperationResult<bool>.FailureResult(ErrorCodes.IncorrectPassword);
                }

                var otp = await _otpService.GenerateAndSendOTPAsync(request.Method==OTPDeliveryMethod.Email?user.Email :user.PhoneNumber, user.Id,request.Method);
                return OperationResult<bool>.SuccessResult(true,200,"OTP sent successfully");
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.FailureResult("OTP_SEND_FAILED_"+ ex.Message);
            }
        }
    }
}
