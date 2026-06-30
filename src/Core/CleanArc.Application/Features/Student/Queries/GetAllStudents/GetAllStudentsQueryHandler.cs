using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArc.Application.Features.Student.Queries.GetAllStudents;

internal class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, OperationResult<List<GetAllStudentsQueryResult>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllStudentsQueryHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITeacherScopeContext _scope;

    public GetAllStudentsQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        ILogger<GetAllStudentsQueryHandler> logger,
        ITeacherScopeContext scope)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        _scope = scope;
    }

    public async ValueTask<OperationResult<List<GetAllStudentsQueryResult>>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);

        var response = await _unitOfWork.StudentRepository.GetAllAsync(request.searchRequest);

        if (response.Code != 200)
            return OperationResult<List<GetAllStudentsQueryResult>>.FailureResult(response.Message, response.Code);

        var mapped = _mapper.Map<List<GetAllStudentsQueryResult>>(response.Data);

        // Teacher-only callers see only students of their *own* class
        // (class teacher relationship). Subject-only classes are filtered out —
        // those students surface separately in the Results module.
        var totalCount = response.TotalCount;
        if (_scope.IsTeacherScoped)
        {
            var classIds = await _scope.GetClassScopeAsync(TeacherScopeKind.ClassTeacher);
            mapped = mapped.Where(s => s.AdmittedClassId.HasValue && classIds.Contains(s.AdmittedClassId.Value)).ToList();
            totalCount = mapped.Count;
        }

        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mapped);

        return OperationResult<List<GetAllStudentsQueryResult>>.SuccessResult(
            mapped, response.Code, response.Message, totalCount);
    }
}
