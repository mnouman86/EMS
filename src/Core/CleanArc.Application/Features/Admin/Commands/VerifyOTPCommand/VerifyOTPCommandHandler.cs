using CleanArc.Application.Contracts;
using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Jwt;
using CleanArc.Domain.Enums;
using CleanArc.Domain.Interfaces.Services;
using CleanArc.Domain.Settings;
using Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CleanArc.Application.Features.Admin.Commands.VerifyOTPCommand
{
    public class VerifyOTPCommandHandler : IRequestHandler<VerifyOTPCommand, OperationResult<AccessToken>>
    {
        private readonly IAppUserManager _userManager;
        private readonly IOTPService _otpService;
        private readonly IJwtService _jwtService;
        private readonly OTPSettings _otpSettings;


        public VerifyOTPCommandHandler(IAppUserManager userManager, IOTPService otpService, IJwtService jwtService,
                    IOptions<OTPSettings> otpSettings
)
        {
            _userManager = userManager;
            _otpService = otpService;
            _jwtService = jwtService;
            _otpSettings = otpSettings.Value;
        }

        public async ValueTask<OperationResult<AccessToken>> Handle(VerifyOTPCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserByIdAsync(request.UserId);
            var isValid = await _otpService.VerifyOTPAsync(_otpSettings.DeliveryMethod==OTPDeliveryMethod.Email?user.Email:user.PhoneNumber,request.UserId, request.Code, _otpSettings.DeliveryMethod);

            if (!isValid)
                return OperationResult<AccessToken>.FailureResult("Invalid or expired OTP");

            

            // Generate JWT token
            var token = await _jwtService.GenerateAsync(user,false);

            //var token = await _tokenService.GenerateTokenAsync(request.PhoneNumber);

            return OperationResult<AccessToken>.SuccessResult(token);
        }
    }
}
