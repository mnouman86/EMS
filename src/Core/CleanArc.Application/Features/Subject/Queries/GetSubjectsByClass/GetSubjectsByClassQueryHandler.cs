using CleanArc.Application.Contracts.Identity;
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
    private readonly ITeacherScopeContext _scope;

    public GetSubjectsByClassQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        ILogger<GetSubjectsByClassQueryHandler> logger,
        ITeacherScopeContext scope)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        _scope = scope;
    }

    public async ValueTask<OperationResult<List<GetSubjectsByClassQueryResult>>> Handle(GetSubjectsByClassQuery request, CancellationToken cancellationToken)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);

        // Class-level fence: teachers can only pull subjects for classes in their scope.
        if (_scope.IsTeacherScoped)
        {
            var classes = await _scope.GetClassScopeAsync(TeacherScopeKind.Combined);
            if (!classes.Contains(request.searchRequestById.Id))
                return OperationResult<List<GetSubjectsByClassQueryResult>>.SuccessResult(
                    new List<GetSubjectsByClassQueryResult>(), 200, "Not assigned to this class.", 0);
        }

        var response = await _unitOfWork.SubjectRepository.GetSubjectsByClassAsync(request.searchRequestById);

        if (response.Code != 200)
        {
            return OperationResult<List<GetSubjectsByClassQueryResult>>.FailureResult(response.Message, response.Code);
        }

        var mapped = _mapper.Map<List<GetSubjectsByClassQueryResult>>(response.Data);
        var totalCount = response.TotalCount;

        // Filter within the class: only subjects the teacher teaches in this class.
        if (_scope.IsTeacherScoped)
        {
            var mySubjects = await _scope.GetSubjectScopeForClassAsync(request.searchRequestById.Id);
            mapped = mapped.Where(s => mySubjects.Contains(s.SubjectId)).ToList();
            totalCount = mapped.Count;
        }

        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mapped);

        return OperationResult<List<GetSubjectsByClassQueryResult>>.SuccessResult(
            mapped, response.Code, response.Message, totalCount);
    }
}
