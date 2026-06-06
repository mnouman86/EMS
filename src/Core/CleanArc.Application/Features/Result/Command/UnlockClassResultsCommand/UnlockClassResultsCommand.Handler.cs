using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Result;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArc.Application.Features.Result.Command.UnlockClassResultsCommand;

internal class UnlockClassResultsCommandHandler : IRequestHandler<UnlockClassResultsCommand, OperationResult<ResponseEntity>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    private readonly ILogger<UnlockClassResultsCommandHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UnlockClassResultsCommandHandler(
        IUnitOfWork unitOfWork,
        IAppUserManager userManager,
        ILogger<UnlockClassResultsCommandHandler> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async ValueTask<OperationResult<ResponseEntity>> Handle(UnlockClassResultsCommand request, CancellationToken cancellationToken)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);

        var user = await _userManager.GetUserByIdAsync(request.UserId);
        if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");

        var result = await _unitOfWork.ResultRepository.UnlockClassResultsAsync(new LockResultsDTO
        {
            ResultSessionId = request.ResultSessionId,
            SchoolClassId = request.SchoolClassId,
            Reason = request.Reason,
            ActorUserId = user.Id
        });

        await _unitOfWork.CommitAsync();
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
        return OperationResult<ResponseEntity>.SuccessResult(result);
    }
}
