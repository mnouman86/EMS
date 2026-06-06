using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArc.Application.Features.Subject.Command.UpdateSubjectCommand;

internal class UpdateSubjectCommandHandler : IRequestHandler<UpdateSubjectCommand, OperationResult<ResponseEntity>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    private readonly ILogger<UpdateSubjectCommandHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateSubjectCommandHandler(
        IUnitOfWork unitOfWork,
        IAppUserManager userManager,
        ILogger<UpdateSubjectCommandHandler> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async ValueTask<OperationResult<ResponseEntity>> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);

        var user = await _userManager.GetUserByIdAsync(request.UserId);
        if (user == null)
            return OperationResult<ResponseEntity>.FailureResult("User Not Found");

        var result = await _unitOfWork.SubjectRepository.UpdateAsync(new Domain.Entities.Subject.Subject
        {
            Id = request.Id,
            Name = request.Name,
            ShortCode = request.ShortCode,
            DisplayOrder = request.DisplayOrder,
            IsRTL = request.IsRTL,
            IsActive = request.IsActive,
            UpdatedBy = user.Id,
            CultureId = request.CultureId
        });

        await _unitOfWork.CommitAsync();
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
        return OperationResult<ResponseEntity>.SuccessResult(result);
    }
}
