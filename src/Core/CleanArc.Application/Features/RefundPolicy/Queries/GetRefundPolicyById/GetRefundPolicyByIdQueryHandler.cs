using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging; using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArc.Application.Features.MappingCarAmenity.Queries.GetMappingCarAmenityByID;

namespace CleanArc.Application.Features.RefundPolicy.Queries.GetRefundPolicyById;

internal class GetRefundPolicyByIdQueryHandler : IRequestHandler<GetRefundPolicyByIdQuery, OperationResult<GetRefundPolicyByIdQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetRefundPolicyByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



    public GetRefundPolicyByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetRefundPolicyByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    public async ValueTask<OperationResult<GetRefundPolicyByIdQueryResult>> Handle(GetRefundPolicyByIdQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            //var RefundPolicy = await _unitOfWork.RefundPolicyRepository.GetByIdAsync(request.searchRequestById);

            //if (RefundPolicy == null)
            //{
            //    return OperationResult<GetRefundPolicyByIdQueryResult>.NotFoundResult("RefundPolicy not found");
            //}

            ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
            //var result = _mapper.Map<GetRefundPolicyByIdQueryResult>(RefundPolicy);

            //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

            //return OperationResult<GetRefundPolicyByIdQueryResult>.SuccessResult(result);

            var response = await _unitOfWork.RefundPolicyRepository.GetByIdAsync(request.searchRequestById);

            if (response.Code != 200)
            {
                return OperationResult<GetRefundPolicyByIdQueryResult>.FailureResult(
                    response.Message,
                response.Code
                );
            }

            var mappedResult = _mapper.Map<GetRefundPolicyByIdQueryResult>(response.Data);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

            return OperationResult<GetRefundPolicyByIdQueryResult>.SuccessResult(
                mappedResult,
                response.Code,
                response.Message
            );
        }
    }

    
}

