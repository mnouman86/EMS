using AutoMapper;
using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.OneBillPayment.Queries.GetOneBillPayment;
using CleanArc.Application.Features.SearchBusinessCarDetail.Queries.GetSearchBusinessCarDetailById;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.OneBillPayment;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.User;
using CleanArc.SharedKernel.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace CleanArc.Application.Features.OneBillPayment.Queries.OneBillPayment;

internal class GetOneBillPaymentQueryHandler : IRequestHandler<GetOneBillPaymentQuery, OneBillInquiryResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetOneBillPaymentQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IAppUserManager _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetOneBillPaymentQueryHandler(IUnitOfWork unitOfWork, ILogger<GetOneBillPaymentQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;

    }


    public async ValueTask<OneBillInquiryResponseDto> Handle(GetOneBillPaymentQuery request, CancellationToken cancellationToken)
    {

        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            var response = await _unitOfWork.OneBillPaymentRepository.GetOneBillPaymentAsync(request.request.UtilityConsumerNumber, request.request.UtilityCompanyId);

            if (response.Code != 200 || response.Data==null)
            {
                //return OperationResult<OneBillPaymentResponseDto>.FailureResult(
                //    response.Message,
                //    response.Code
                //);
                return new OneBillInquiryResponseDto
                {
                        ResponseCode = "01", // Not Found
                        ConsumerDetail = "NOT FOUND"
                    };
                
            }

            //var mappedResult = response.Data;
            var mappedResult = _mapper.Map<OneBillInquiryResponseDto>(response.Data);

            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

            //return OperationResult<GetKBDetailByIdAllQueryResult>.SuccessResult(result);
            return mappedResult;
        }
    }
}