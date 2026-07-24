using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArc.Application.Features.SchoolClass.Queries.GetSchoolClassById;

internal class GetSchoolClassByIdQueryHandler : IRequestHandler<GetSchoolClassByIdQuery, OperationResult<GetSchoolClassByIdQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetSchoolClassByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITeacherScopeContext _scope;

    public GetSchoolClassByIdQueryHandler(
        IUnitOfWork unitOfWork,
        ILogger<GetSchoolClassByIdQueryHandler> logger,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        ITeacherScopeContext scope)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        _scope = scope;
    }

    public async ValueTask<OperationResult<GetSchoolClassByIdQueryResult>> Handle(GetSchoolClassByIdQuery request, CancellationToken cancellationToken)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);

        // A teacher can only fetch a class they teach or head.
        if (_scope.IsTeacherScoped)
        {
            var classes = await _scope.GetClassScopeAsync(TeacherScopeKind.Combined);
            if (!classes.Contains(request.searchRequestById.Id))
                return OperationResult<GetSchoolClassByIdQueryResult>.FailureResult("Not authorized for this class.", 403);
        }

        var response = await _unitOfWork.SchoolClassRepository.GetByIdAsync(request.searchRequestById);

        if (response.Code != 200)
        {
            return OperationResult<GetSchoolClassByIdQueryResult>.FailureResult(response.Message, response.Code);
        }

        var mapped = _mapper.Map<GetSchoolClassByIdQueryResult>(response.Data);
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mapped);

        return OperationResult<GetSchoolClassByIdQueryResult>.SuccessResult(mapped, response.Code, response.Message);
    }
}
