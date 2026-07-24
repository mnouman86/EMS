using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArc.Application.Features.SchoolClass.Queries.GetAllSchoolClasses;

internal class GetAllSchoolClassesQueryHandler : IRequestHandler<GetAllSchoolClassesQuery, OperationResult<List<GetAllSchoolClassesQueryResult>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllSchoolClassesQueryHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITeacherScopeContext _scope;

    public GetAllSchoolClassesQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        ILogger<GetAllSchoolClassesQueryHandler> logger,
        ITeacherScopeContext scope)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        _scope = scope;
    }

    public async ValueTask<OperationResult<List<GetAllSchoolClassesQueryResult>>> Handle(GetAllSchoolClassesQuery request, CancellationToken cancellationToken)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);

        var response = await _unitOfWork.SchoolClassRepository.GetAllAsync(request.searchRequest);

        if (response.Code != 200)
        {
            return OperationResult<List<GetAllSchoolClassesQueryResult>>.FailureResult(response.Message, response.Code);
        }

        var mapped = _mapper.Map<List<GetAllSchoolClassesQueryResult>>(response.Data);
        var totalCount = response.TotalCount;

        // Teacher-only callers see only classes in their combined scope
        // (class-teacher OR any subject they teach in that class).
        if (_scope.IsTeacherScoped)
        {
            var classIds = await _scope.GetClassScopeAsync(TeacherScopeKind.Combined);
            mapped = mapped.Where(c => classIds.Contains(c.Id)).ToList();
            totalCount = mapped.Count;
        }

        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mapped);

        return OperationResult<List<GetAllSchoolClassesQueryResult>>.SuccessResult(
            mapped, response.Code, response.Message, totalCount);
    }
}
