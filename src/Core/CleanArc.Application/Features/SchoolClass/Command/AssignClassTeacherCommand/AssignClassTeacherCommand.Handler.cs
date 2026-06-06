using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.SchoolClass;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArc.Application.Features.SchoolClass.Command.AssignClassTeacherCommand;

internal class AssignClassTeacherCommandHandler : IRequestHandler<AssignClassTeacherCommand, OperationResult<ResponseEntity>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    private readonly ILogger<AssignClassTeacherCommandHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AssignClassTeacherCommandHandler(
        IUnitOfWork unitOfWork,
        IAppUserManager userManager,
        ILogger<AssignClassTeacherCommandHandler> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async ValueTask<OperationResult<ResponseEntity>> Handle(AssignClassTeacherCommand request, CancellationToken cancellationToken)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);

        var user = await _userManager.GetUserByIdAsync(request.UserId);
        if (user == null)
            return OperationResult<ResponseEntity>.FailureResult("User Not Found");

        var result = await _unitOfWork.SchoolClassRepository.AssignClassTeacherAsync(new AssignClassTeacherDTO
        {
            SchoolClassId = request.SchoolClassId,
            ClassTeacherId = request.ClassTeacherId,
            UpdatedBy = user.Id,
            CultureId = request.CultureId ?? 0
        });

        await _unitOfWork.CommitAsync();
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
        return OperationResult<ResponseEntity>.SuccessResult(result);
    }
}
