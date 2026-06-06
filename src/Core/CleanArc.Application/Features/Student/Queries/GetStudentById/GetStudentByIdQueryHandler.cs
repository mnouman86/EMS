using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArc.Application.Features.Student.Queries.GetStudentById;

internal class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, OperationResult<GetStudentByIdQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetStudentByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITeacherScopeContext _scope;

    public GetStudentByIdQueryHandler(
        IUnitOfWork unitOfWork,
        ILogger<GetStudentByIdQueryHandler> logger,
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

    public async ValueTask<OperationResult<GetStudentByIdQueryResult>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);

        if (_scope.IsTeacherScoped && !await _scope.OwnsStudentAsync(request.searchRequestById.Id))
            return OperationResult<GetStudentByIdQueryResult>.FailureResult("Not authorized for this student.", 403);

        var response = await _unitOfWork.StudentRepository.GetByIdAsync(request.searchRequestById);

        if (response.Code != 200)
            return OperationResult<GetStudentByIdQueryResult>.FailureResult(response.Message, response.Code);

        var mapped = _mapper.Map<GetStudentByIdQueryResult>(response.Data);
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mapped);

        return OperationResult<GetStudentByIdQueryResult>.SuccessResult(mapped, response.Code, response.Message);
    }
}
