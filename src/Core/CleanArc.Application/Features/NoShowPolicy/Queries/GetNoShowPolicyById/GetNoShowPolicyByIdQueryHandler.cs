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

namespace CleanArc.Application.Features.NoShowPolicy.Queries.GetNoShowPolicyById;

internal class GetNoShowPolicyByIdQueryHandler : IRequestHandler<GetNoShowPolicyByIdQuery, OperationResult<GetNoShowPolicyByIdQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetNoShowPolicyByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



    public GetNoShowPolicyByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetNoShowPolicyByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    public async ValueTask<OperationResult<GetNoShowPolicyByIdQueryResult>> Handle(GetNoShowPolicyByIdQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            //var NoShowPolicy = await _unitOfWork.NoShowPolicyRepository.GetByIdAsync(request.searchRequestById);

            //if (NoShowPolicy == null)
            //{
            //    return OperationResult<GetNoShowPolicyByIdQueryResult>.NotFoundResult("NoShowPolicy not found");
            //}

            ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
            //var result = _mapper.Map<GetNoShowPolicyByIdQueryResult>(NoShowPolicy);

            //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

            //return OperationResult<GetNoShowPolicyByIdQueryResult>.SuccessResult(result);

            var response = await _unitOfWork.NoShowPolicyRepository.GetByIdAsync(request.searchRequestById);

            if (response.Code != 200)
            {
                return OperationResult<GetNoShowPolicyByIdQueryResult>.FailureResult(
                    response.Message,
                response.Code
                );
            }

            var mappedResult = _mapper.Map<GetNoShowPolicyByIdQueryResult>(response.Data);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

            return OperationResult<GetNoShowPolicyByIdQueryResult>.SuccessResult(
                mappedResult,
                response.Code,
                response.Message
            );
        }
    }

    
}

