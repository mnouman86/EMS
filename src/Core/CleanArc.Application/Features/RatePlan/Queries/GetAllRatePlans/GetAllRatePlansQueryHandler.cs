using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging; 
using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArc.Application.Features.MappingCarAmenity.Queries.GetMappingCarAmenityByID;
using CleanArc.Application.Models.RatePlan;

namespace CleanArc.Application.Features.RatePlan.Queries.GetRatePlanById;

internal class GetAllRatePlansQueryHandler : IRequestHandler<GetAllRatePlansQuery, OperationResult<RatePlanResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAllRatePlansQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



    public GetAllRatePlansQueryHandler(IUnitOfWork unitOfWork, ILogger<GetAllRatePlansQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    public async ValueTask<OperationResult<RatePlanResponseDto>> Handle(GetAllRatePlansQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            //var RatePlan = await _unitOfWork.RatePlanRepository.GetByIdAsync(request.searchRequestById);

            //if (RatePlan == null)
            //{
            //    return OperationResult<GetRatePlanByIdQueryResult>.NotFoundResult("RatePlan not found");
            //}

            ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
            //var result = _mapper.Map<GetRatePlanByIdQueryResult>(RatePlan);

            //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

            //return OperationResult<GetRatePlanByIdQueryResult>.SuccessResult(result);

            var response = await _unitOfWork.RatePlanRepository.GetAllRatePlansAsync(request.searchRequest);

            if (response.Code != 200)
            {
                return OperationResult<RatePlanResponseDto>.FailureResult(
                    response.Message,
                response.Code
                );
            }

            var mappedResult = _mapper.Map<RatePlanResponseDto>(response.Data);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

            return OperationResult<RatePlanResponseDto>.SuccessResult(
                mappedResult,
                response.Code,
                response.Message
            );
        }
    }

    
}

