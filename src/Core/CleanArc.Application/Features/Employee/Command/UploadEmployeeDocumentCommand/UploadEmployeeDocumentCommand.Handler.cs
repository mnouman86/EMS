using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Employee;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArc.Application.Features.Employee.Command.UploadEmployeeDocumentCommand;

internal class UploadEmployeeDocumentCommandHandler : IRequestHandler<UploadEmployeeDocumentCommand, OperationResult<ResponseEntity>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    private readonly ILogger<UploadEmployeeDocumentCommandHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UploadEmployeeDocumentCommandHandler(
        IUnitOfWork unitOfWork,
        IAppUserManager userManager,
        ILogger<UploadEmployeeDocumentCommandHandler> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async ValueTask<OperationResult<ResponseEntity>> Handle(UploadEmployeeDocumentCommand request, CancellationToken cancellationToken)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);

        var user = await _userManager.GetUserByIdAsync(request.UserId);
        if (user == null)
            return OperationResult<ResponseEntity>.FailureResult("User Not Found");

        var result = await _unitOfWork.EmployeeRepository.AddDocumentAsync(new UploadEmployeeDocumentDTO
        {
            EmployeeId = request.EmployeeId,
            DocumentType = request.DocumentType,
            FileName = request.FileName,
            FilePath = request.FilePath,
            ContentType = request.ContentType,
            FileSizeBytes = request.FileSizeBytes,
            IsPhoto = request.IsPhoto,
            UploadedBy = user.Id,
            CultureId = request.CultureId ?? 0
        });

        await _unitOfWork.CommitAsync();
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
        return OperationResult<ResponseEntity>.SuccessResult(result);
    }
}
