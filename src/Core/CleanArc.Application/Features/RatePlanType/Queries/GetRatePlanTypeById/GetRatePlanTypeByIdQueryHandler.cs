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

namespace CleanArc.Application.Features.RatePlanType.Queries.GetRatePlanTypeById;

internal class GetRatePlanTypeByIdQueryHandler : IRequestHandler<GetRatePlanTypeByIdQuery, OperationResult<GetRatePlanTypeByIdQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetRatePlanTypeByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



    public GetRatePlanTypeByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetRatePlanTypeByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    public async ValueTask<OperationResult<GetRatePlanTypeByIdQueryResult>> Handle(GetRatePlanTypeByIdQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            //var RatePlanType = await _unitOfWork.RatePlanTypeRepository.GetByIdAsync(request.searchRequestById);

            //if (RatePlanType == null)
            //{
            //    return OperationResult<GetRatePlanTypeByIdQueryResult>.NotFoundResult("RatePlanType not found");
            //}

            ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
            //var result = _mapper.Map<GetRatePlanTypeByIdQueryResult>(RatePlanType);

            //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

            //return OperationResult<GetRatePlanTypeByIdQueryResult>.SuccessResult(result);

            var response = await _unitOfWork.RatePlanTypeRepository.GetByIdAsync(request.searchRequestById);

            if (response.Code != 200)
            {
                return OperationResult<GetRatePlanTypeByIdQueryResult>.FailureResult(
                    response.Message,
                response.Code
                );
            }

            var mappedResult = _mapper.Map<GetRatePlanTypeByIdQueryResult>(response.Data);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

            return OperationResult<GetRatePlanTypeByIdQueryResult>.SuccessResult(
                mappedResult,
                response.Code,
                response.Message
            );
        }
    }

    
}

