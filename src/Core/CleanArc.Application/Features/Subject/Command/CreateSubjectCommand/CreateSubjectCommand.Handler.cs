using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArc.Application.Features.Subject.Command.CreateSubjectCommand;

internal class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand, OperationResult<ResponseEntity>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    private readonly ILogger<CreateSubjectCommandHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateSubjectCommandHandler(
        IUnitOfWork unitOfWork,
        IAppUserManager userManager,
        ILogger<CreateSubjectCommandHandler> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async ValueTask<OperationResult<ResponseEntity>> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);

        var user = await _userManager.GetUserByIdAsync(request.UserId);
        if (user == null)
            return OperationResult<ResponseEntity>.FailureResult("User Not Found");

        var result = await _unitOfWork.SubjectRepository.AddAsync(new Domain.Entities.Subject.Subject
        {
            Name = request.Name,
            ShortCode = request.ShortCode,
            DisplayOrder = request.DisplayOrder,
            IsRTL = request.IsRTL,
            IsActive = request.IsActive,
            CreatedBy = user.Id,
            CultureId = request.CultureId
        });

        await _unitOfWork.CommitAsync();
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
        return OperationResult<ResponseEntity>.SuccessResult(result);
    }
}
