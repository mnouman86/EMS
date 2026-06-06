using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Employee;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArc.Application.Features.Employee.Command.AssignTeacherSubjectsCommand;

internal class AssignTeacherSubjectsCommandHandler : IRequestHandler<AssignTeacherSubjectsCommand, OperationResult<ResponseEntity>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    private readonly ILogger<AssignTeacherSubjectsCommandHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AssignTeacherSubjectsCommandHandler(
        IUnitOfWork unitOfWork,
        IAppUserManager userManager,
        ILogger<AssignTeacherSubjectsCommandHandler> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async ValueTask<OperationResult<ResponseEntity>> Handle(AssignTeacherSubjectsCommand request, CancellationToken cancellationToken)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);

        var user = await _userManager.GetUserByIdAsync(request.UserId);
        if (user == null)
            return OperationResult<ResponseEntity>.FailureResult("User Not Found");

        var pairs = request.Assignments == null || request.Assignments.Count == 0
            ? string.Empty
            : string.Join(",", request.Assignments.Select(a => $"{a.SchoolClassId}:{a.SubjectId}"));

        var result = await _unitOfWork.EmployeeRepository.AssignTeacherSubjectsAsync(new AssignTeacherSubjectsDTO
        {
            EmployeeId = request.EmployeeId,
            ClassSubjectPairs = pairs,
            UpdatedBy = user.Id,
            CultureId = request.CultureId ?? 0
        });

        await _unitOfWork.CommitAsync();
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
        return OperationResult<ResponseEntity>.SuccessResult(result);
    }
}
