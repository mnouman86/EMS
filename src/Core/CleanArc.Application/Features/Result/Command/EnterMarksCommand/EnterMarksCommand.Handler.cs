using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Result;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CleanArc.Application.Features.Result.Command.EnterMarksCommand;

internal class EnterMarksCommandHandler : IRequestHandler<EnterMarksCommand, OperationResult<ResponseEntity>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    private readonly ILogger<EnterMarksCommandHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EnterMarksCommandHandler(
        IUnitOfWork unitOfWork,
        IAppUserManager userManager,
        ILogger<EnterMarksCommandHandler> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async ValueTask<OperationResult<ResponseEntity>> Handle(EnterMarksCommand request, CancellationToken cancellationToken)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);

        var user = await _userManager.GetUserByIdAsync(request.UserId);
        if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");

        var json = JsonSerializer.Serialize(request.Entries);

        var result = await _unitOfWork.ResultRepository.EnterMarksAsync(new EnterMarksDTO
        {
            ResultSessionId = request.ResultSessionId,
            SchoolClassId = request.SchoolClassId,
            SubjectId = request.SubjectId,
            TeacherEmployeeId = request.TeacherEmployeeId,
            EntriesJson = json,
            UpdatedBy = user.Id
        });

        await _unitOfWork.CommitAsync();
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
        return OperationResult<ResponseEntity>.SuccessResult(result);
    }
}
