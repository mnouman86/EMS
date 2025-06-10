using CleanArc.Application.Contracts;
using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Interfaces.Services;
using Mediator;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArc.Application.Models.Jwt;


namespace CleanArc.Application.Features.Admin.Commands.VerifyOTPCommand
{
    public class VerifyOTPCommandHandler : IRequestHandler<VerifyOTPCommand, OperationResult<AccessToken>>
    {
        private readonly IAppUserManager _userManager;
        private readonly IOTPService _otpService;
        private readonly IJwtService _jwtService;

        public VerifyOTPCommandHandler(IAppUserManager userManager, IOTPService otpService, IJwtService jwtService)
        {
            _userManager = userManager;
            _otpService = otpService;
            _jwtService = jwtService;
        }

        public async ValueTask<OperationResult<AccessToken>> Handle(VerifyOTPCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserByIdAsync(request.UserId);
            var isValid = await _otpService.VerifyOTPAsync(user.PhoneNumber,request.UserId, request.Code);

            if (!isValid)
                return OperationResult<AccessToken>.FailureResult("Invalid or expired OTP");

            

            // Generate JWT token
            var token = await _jwtService.GenerateAsync(user);

            //var token = await _tokenService.GenerateTokenAsync(request.PhoneNumber);

            return OperationResult<AccessToken>.SuccessResult(token);
        }
    }
}
