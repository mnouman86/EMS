using CleanArc.Application.Contracts;
using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Features.Users.Commands.Create;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Jwt;
using Mediator;
using Microsoft.Extensions.Logging;
using CleanArc.Domain.Common;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.StartupData;
using Microsoft.Extensions.Configuration;
using CleanArc.Application.Features.Service.Queries.GetServiceById;
using CleanArc.SharedKernel.Extensions;
using Serilog.Core;
using MapsterMapper;
using Microsoft.AspNetCore.Http;

namespace CleanArc.Application.Features.Admin.Queries.GetToken;

public class GetStartupDataQueryHandler : IRequestHandler<GetStartupDataQuery, OperationResult<StartupDataDto>>
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly ILogger<GetStartupDataQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor

    //private readonly bool _isEmailVerificationEnabled = false;
    //private readonly bool _isOtpEnabled = false;
    //private readonly int _emailVerificationMaxAttempts = 1;
    //private readonly int _otpExpirationMinutes = 1;
    //private readonly int _resendEmailVerificationTime = 30;
    public GetStartupDataQueryHandler(
        IMapper mapper,
        IConfiguration configuration,
        ILogger<GetStartupDataQueryHandler> logger,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor) // Injecting UnitOfWork
    {
        _mapper = mapper;
        _configuration = configuration;
        _logger = logger;
        _unitOfWork = unitOfWork; // Assigning UnitOfWork
        _httpContextAccessor = httpContextAccessor;
        //bool.TryParse(configuration["EmailVerificationSettings:Enabled"], out _isEmailVerificationEnabled);
        //bool.TryParse(configuration["OTP:Enabled"], out _isOtpEnabled);
        //int.TryParse(configuration["EmailVerificationSettings:MaxAttempts"], out _emailVerificationMaxAttempts);
        //int.TryParse(configuration["OTP:ExpirationMinutes"], out _otpExpirationMinutes);
    }

    public async  ValueTask<OperationResult<StartupDataDto>> Handle(GetStartupDataQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            string methodName = "AdminGetTokenQueryHandler";
            _logger.LogInformation("Handler Started: {@methodName}, Query Request: {@request}", methodName, request);
            var response = await _unitOfWork.StartupDataRepository.GetStartupDataAsync();

            if (response.Code != 200)
            {
                return OperationResult<StartupDataDto>.FailureResult(
                    response.Message,
                response.Code
                );
            }

            var mappedResult = _mapper.Map<StartupDataDto>(response.Data);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

            return OperationResult<StartupDataDto>.SuccessResult(
                mappedResult,
                response.Code,
                response.Message
            );
        }
        //SerilogSettings = _configuration.GetSection("SerilogSettings").Get<SerilogSettingsDto>()

        //var startupData = new StartupDataDto
        //    {

        //    IsEmailVerificationEnabled = _isEmailVerificationEnabled,
        //    EmailVerificationMaxAttempts = _emailVerificationMaxAttempts,
        //    IsOtpEnabled = _isOtpEnabled,
        //    OtpExpirationMinutes = _otpExpirationMinutes,
        //    ResendEmailVerificationTime= _resendEmailVerificationTime
        //    };

        //return OperationResult<StartupDataDto>.SuccessResult(startupData);
    }
}
