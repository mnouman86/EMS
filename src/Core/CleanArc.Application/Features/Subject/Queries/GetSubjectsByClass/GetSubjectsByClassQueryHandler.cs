using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArc.Application.Features.Subject.Queries.GetSubjectsByClass;

internal class GetSubjectsByClassQueryHandler : IRequestHandler<GetSubjectsByClassQuery, OperationResult<List<GetSubjectsByClassQueryResult>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetSubjectsByClassQueryHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetSubjectsByClassQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        ILogger<GetSubjectsByClassQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async ValueTask<OperationResult<List<GetSubjectsByClassQueryResult>>> Handle(GetSubjectsByClassQuery request, CancellationToken cancellationToken)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);

        var response = await _unitOfWork.SubjectRepository.GetSubjectsByClassAsync(request.searchRequestById);

        if (response.Code != 200)
        {
            return OperationResult<List<GetSubjectsByClassQueryResult>>.FailureResult(response.Message, response.Code);
        }

        var mapped = _mapper.Map<List<GetSubjectsByClassQueryResult>>(response.Data);
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mapped);

        return OperationResult<List<GetSubjectsByClassQueryResult>>.SuccessResult(
            mapped, response.Code, response.Message, response.TotalCount);
    }
}
