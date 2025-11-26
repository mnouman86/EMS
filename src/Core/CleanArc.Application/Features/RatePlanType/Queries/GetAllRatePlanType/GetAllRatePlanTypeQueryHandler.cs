using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.AgeType.Queries.GetAllAgeType;
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
using CleanArc.Application.Features.Activity.Queries.GetAllActivity;

namespace CleanArc.Application.Features.RatePlanType.Queries.GetAllRatePlanTypes;

internal class GetAllRatePlanTypesQueryHandler : IRequestHandler<GetAllRatePlanTypesQuery, OperationResult<List<GetAllRatePlanTypesQueryResult>>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllRatePlanTypesQueryHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor


    public GetAllRatePlanTypesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<GetAllRatePlanTypesQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;

    }

    public async ValueTask<OperationResult<List<GetAllRatePlanTypesQueryResult>>> Handle(GetAllRatePlanTypesQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            //var RatePlanType = await _unitOfWork.RatePlanTypeRepository.GetAllAsync(request.searchRequest);

            ////var resultCheck = uRLs.Select(c => new GetAllProductsQueryResult(c.Id, c.Path, c.Title, c.Description)).ToList();
            //var result = _mapper.Map<List<GetAllRatePlanTypesQueryResult>>(RatePlanType);
            //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
            //return OperationResult<List<GetAllRatePlanTypesQueryResult>>.SuccessResult(result);

            var response = await _unitOfWork.RatePlanTypeRepository.GetAllAsync(request.searchRequest);

            if (response.Code != 200)
            {
                return OperationResult<List<GetAllRatePlanTypesQueryResult>>.FailureResult(
                    response.Message,
                response.Code
                );
            }

            var mappedResult = _mapper.Map<List<GetAllRatePlanTypesQueryResult>>(response.Data);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

            return OperationResult<List<GetAllRatePlanTypesQueryResult>>.SuccessResult(
                mappedResult,
                response.Code,
                response.Message,
                    response.TotalCount
            );
        }
    }
}


