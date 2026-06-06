using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArc.Application.Features.Result.Queries.GetResultSessions;

internal class GetResultSessionsQueryHandler : IRequestHandler<GetResultSessionsQuery, OperationResult<List<GetResultSessionsQueryResult>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetResultSessionsQueryHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetResultSessionsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper,
        IHttpContextAccessor httpContextAccessor, ILogger<GetResultSessionsQueryHandler> logger)
    {
        _unitOfWork = unitOfWork; _mapper = mapper;
        _httpContextAccessor = httpContextAccessor; _logger = logger;
    }

    public async ValueTask<OperationResult<List<GetResultSessionsQueryResult>>> Handle(GetResultSessionsQuery request, CancellationToken cancellationToken)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);
        var response = await _unitOfWork.ResultRepository.GetSessionsAsync(request.searchRequest);
        if (response.Code != 200)
            return OperationResult<List<GetResultSessionsQueryResult>>.FailureResult(response.Message, response.Code);

        var mapped = _mapper.Map<List<GetResultSessionsQueryResult>>(response.Data);
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mapped);
        return OperationResult<List<GetResultSessionsQueryResult>>.SuccessResult(mapped, response.Code, response.Message, response.TotalCount);
    }
}
