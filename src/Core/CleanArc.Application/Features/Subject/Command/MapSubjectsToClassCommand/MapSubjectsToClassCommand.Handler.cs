using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Subject;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArc.Application.Features.Subject.Command.MapSubjectsToClassCommand;

internal class MapSubjectsToClassCommandHandler : IRequestHandler<MapSubjectsToClassCommand, OperationResult<ResponseEntity>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    private readonly ILogger<MapSubjectsToClassCommandHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MapSubjectsToClassCommandHandler(
        IUnitOfWork unitOfWork,
        IAppUserManager userManager,
        ILogger<MapSubjectsToClassCommandHandler> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async ValueTask<OperationResult<ResponseEntity>> Handle(MapSubjectsToClassCommand request, CancellationToken cancellationToken)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);

        var user = await _userManager.GetUserByIdAsync(request.UserId);
        if (user == null)
            return OperationResult<ResponseEntity>.FailureResult("User Not Found");

        var ids = request.SubjectIds == null || request.SubjectIds.Count == 0
            ? string.Empty
            : string.Join(",", request.SubjectIds);

        var result = await _unitOfWork.SubjectRepository.MapSubjectsToClassAsync(new MapSubjectsToClassDTO
        {
            SchoolClassId = request.SchoolClassId,
            SubjectIds = ids,
            UpdatedBy = user.Id,
            CultureId = request.CultureId ?? 0
        });

        await _unitOfWork.CommitAsync();
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
        return OperationResult<ResponseEntity>.SuccessResult(result);
    }
}
