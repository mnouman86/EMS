using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Student;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArc.Application.Features.Student.Command.ChangeStudentStatusCommand;

internal class ChangeStudentStatusCommandHandler : IRequestHandler<ChangeStudentStatusCommand, OperationResult<ResponseEntity>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    private readonly ILogger<ChangeStudentStatusCommandHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ChangeStudentStatusCommandHandler(
        IUnitOfWork unitOfWork,
        IAppUserManager userManager,
        ILogger<ChangeStudentStatusCommandHandler> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async ValueTask<OperationResult<ResponseEntity>> Handle(ChangeStudentStatusCommand request, CancellationToken cancellationToken)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);

        var user = await _userManager.GetUserByIdAsync(request.UserId);
        if (user == null)
            return OperationResult<ResponseEntity>.FailureResult("User Not Found");

        var result = await _unitOfWork.StudentRepository.ChangeStatusAsync(new ChangeStudentStatusDTO
        {
            Id = request.Id,
            TargetStatus = request.TargetStatus,
            AdmittedClassId = request.AdmittedClassId,
            LifecycleReason = request.LifecycleReason,
            UpdatedBy = user.Id,
            CultureId = request.CultureId ?? 0
        });

        await _unitOfWork.CommitAsync();
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
        return OperationResult<ResponseEntity>.SuccessResult(result);
    }
}
