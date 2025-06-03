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

namespace CleanArc.Application.Features.LastMinuteDeal.Queries.GetLastMinuteDealById;

internal class GetLastMinuteDealByIdQueryHandler : IRequestHandler<GetLastMinuteDealByIdQuery, OperationResult<GetLastMinuteDealByIdQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetLastMinuteDealByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



    public GetLastMinuteDealByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetLastMinuteDealByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    public async ValueTask<OperationResult<GetLastMinuteDealByIdQueryResult>> Handle(GetLastMinuteDealByIdQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            //var LastMinuteDeal = await _unitOfWork.LastMinuteDealRepository.GetByIdAsync(request.searchRequestById);

            //if (LastMinuteDeal == null)
            //{
            //    return OperationResult<GetLastMinuteDealByIdQueryResult>.NotFoundResult("LastMinuteDeal not found");
            //}

            ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
            //var result = _mapper.Map<GetLastMinuteDealByIdQueryResult>(LastMinuteDeal);

            //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

            //return OperationResult<GetLastMinuteDealByIdQueryResult>.SuccessResult(result);

            var response = await _unitOfWork.LastMinuteDealRepository.GetByIdAsync(request.searchRequestById);

            if (response.Code != 200)
            {
                return OperationResult<GetLastMinuteDealByIdQueryResult>.FailureResult(
                    response.Message,
                response.Code
                );
            }

            var mappedResult = _mapper.Map<GetLastMinuteDealByIdQueryResult>(response.Data);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

            return OperationResult<GetLastMinuteDealByIdQueryResult>.SuccessResult(
                mappedResult,
                response.Code,
                response.Message
            );
        }
    }

    
}

