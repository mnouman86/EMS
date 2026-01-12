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

namespace CleanArc.Application.Features.PolicyType.Queries.GetPolicyTypeById;

internal class GetPolicyTypeByIdQueryHandler : IRequestHandler<GetPolicyTypeByIdQuery, OperationResult<GetPolicyTypeByIdQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetPolicyTypeByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



    public GetPolicyTypeByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetPolicyTypeByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    public async ValueTask<OperationResult<GetPolicyTypeByIdQueryResult>> Handle(GetPolicyTypeByIdQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            //var PolicyType = await _unitOfWork.PolicyTypeRepository.GetByIdAsync(request.searchRequestById);

            //if (PolicyType == null)
            //{
            //    return OperationResult<GetPolicyTypeByIdQueryResult>.NotFoundResult("PolicyType not found");
            //}

            ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
            //var result = _mapper.Map<GetPolicyTypeByIdQueryResult>(PolicyType);

            //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

            //return OperationResult<GetPolicyTypeByIdQueryResult>.SuccessResult(result);

            var response = await _unitOfWork.PolicyTypeRepository.GetByIdAsync(request.searchRequestById);

            if (response.Code != 200)
            {
                return OperationResult<GetPolicyTypeByIdQueryResult>.FailureResult(
                    response.Message,
                response.Code
                );
            }

            var mappedResult = _mapper.Map<GetPolicyTypeByIdQueryResult>(response.Data);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

            return OperationResult<GetPolicyTypeByIdQueryResult>.SuccessResult(
                mappedResult,
                response.Code,
                response.Message
            );
        }
    }

    
}

