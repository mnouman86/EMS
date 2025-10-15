using Azure;
using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.OneBillPayment;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.OneBillPayment.Command.UpdateOneBillPaymentCommand;

internal class UpdateOneBillPaymentCommandHandler : IRequestHandler<UpdateOneBillPaymentCommand, OneBillPaymentResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    private readonly IConfiguration configuration;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateOneBillPaymentCommandHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor
    //private readonly IUnitOfWork _unitOfWork;
    //private readonly IAppUserManager _userManager;


    public UpdateOneBillPaymentCommandHandler(IUnitOfWork unitOfWork, IAppUserManager userManager, IConfiguration configuration, IMapper mapper, ILogger<UpdateOneBillPaymentCommandHandler> logger, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        this.configuration = configuration;
        _mapper = mapper;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        //_unitOfWork = unitOfWork;
        //_userManager = userManager;
    }
    public async ValueTask<OneBillPaymentResponseDto> Handle(UpdateOneBillPaymentCommand request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {

            var user = await _userManager.GetUserByIdAsync(request.UserId);
            if (user == null)
                return null;
            var result = await _unitOfWork.OneBillPaymentRepository.RecordPaymentAsync(request.Request,request.UserId,cancellationToken);
            //await _unitOfWork.CommitAsync();
            //return OperationResult<ResponseEntity>.SuccessResult(result);
            //return new OneBillPaymentResponseDto
            //{
            //    ResponseCode = "00",
            //    AuthIdResponse = request.Request.Stan,
            //    TransactionLogId = result,
            //    Reserved = string.Empty
            //};

            if (result.Code != 200 || result.Data == null)
            {
                
                return new OneBillPaymentResponseDto
                {
                    ResponseCode = "02", // Not Found
                    TransactionLogId = request.Request.Stan, Reserved = string.Empty,AuthIdResponse=request.Request.Stan,
                };

            }

            
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

            //return OperationResult<GetKBDetailByIdAllQueryResult>.SuccessResult(result);
            return result.Data;
        }
    }

}
