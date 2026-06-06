using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Employee;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArc.Application.Features.Employee.Command.MarkEmployeeLeftCommand;

internal class MarkEmployeeLeftCommandHandler : IRequestHandler<MarkEmployeeLeftCommand, OperationResult<ResponseEntity>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    private readonly ILogger<MarkEmployeeLeftCommandHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MarkEmployeeLeftCommandHandler(
        IUnitOfWork unitOfWork,
        IAppUserManager userManager,
        ILogger<MarkEmployeeLeftCommandHandler> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async ValueTask<OperationResult<ResponseEntity>> Handle(MarkEmployeeLeftCommand request, CancellationToken cancellationToken)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);

        var user = await _userManager.GetUserByIdAsync(request.UserId);
        if (user == null)
            return OperationResult<ResponseEntity>.FailureResult("User Not Found");

        var result = await _unitOfWork.EmployeeRepository.MarkAsLeftAsync(new MarkEmployeeLeftDTO
        {
            Id = request.Id,
            LastWorkingDay = request.LastWorkingDay,
            ReasonForLeaving = request.ReasonForLeaving,
            UpdatedBy = user.Id,
            CultureId = request.CultureId ?? 0
        });

        await _unitOfWork.CommitAsync();
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
        return OperationResult<ResponseEntity>.SuccessResult(result);
    }
}
