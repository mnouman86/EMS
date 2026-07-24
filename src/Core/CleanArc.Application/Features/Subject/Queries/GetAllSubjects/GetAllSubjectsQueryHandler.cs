using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArc.Application.Features.Subject.Queries.GetAllSubjects;

internal class GetAllSubjectsQueryHandler : IRequestHandler<GetAllSubjectsQuery, OperationResult<List<GetAllSubjectsQueryResult>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllSubjectsQueryHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITeacherScopeContext _scope;

    public GetAllSubjectsQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        ILogger<GetAllSubjectsQueryHandler> logger,
        ITeacherScopeContext scope)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        _scope = scope;
    }

    public async ValueTask<OperationResult<List<GetAllSubjectsQueryResult>>> Handle(GetAllSubjectsQuery request, CancellationToken cancellationToken)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);

        var response = await _unitOfWork.SubjectRepository.GetAllAsync(request.searchRequest);

        if (response.Code != 200)
        {
            return OperationResult<List<GetAllSubjectsQueryResult>>.FailureResult(response.Message, response.Code);
        }

        var mapped = _mapper.Map<List<GetAllSubjectsQueryResult>>(response.Data);
        var totalCount = response.TotalCount;

        // Teachers only see subjects they're actually assigned to teach.
        if (_scope.IsTeacherScoped)
        {
            var subjectIds = await _scope.GetSubjectScopeAsync();
            mapped = mapped.Where(s => subjectIds.Contains(s.Id)).ToList();
            totalCount = mapped.Count;
        }

        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mapped);

        return OperationResult<List<GetAllSubjectsQueryResult>>.SuccessResult(
            mapped, response.Code, response.Message, totalCount);
    }
}
