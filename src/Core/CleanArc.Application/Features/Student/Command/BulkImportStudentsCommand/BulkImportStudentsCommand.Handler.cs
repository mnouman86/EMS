using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Student;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CleanArc.Application.Features.Student.Command.BulkImportStudentsCommand;

internal class BulkImportStudentsCommandHandler : IRequestHandler<BulkImportStudentsCommand, OperationResult<ResponseEntity>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    private readonly ILogger<BulkImportStudentsCommandHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BulkImportStudentsCommandHandler(
        IUnitOfWork unitOfWork,
        IAppUserManager userManager,
        ILogger<BulkImportStudentsCommandHandler> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async ValueTask<OperationResult<ResponseEntity>> Handle(BulkImportStudentsCommand request, CancellationToken cancellationToken)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);

        var user = await _userManager.GetUserByIdAsync(request.UserId);
        if (user == null)
            return OperationResult<ResponseEntity>.FailureResult("User Not Found");

        var json = JsonSerializer.Serialize(request.Rows);

        var result = await _unitOfWork.StudentRepository.BulkImportAsync(new BulkImportStudentsDTO
        {
            RowsJson = json,
            UpdateExisting = request.UpdateExisting,
            CreatedBy = user.Id,
            CultureId = request.CultureId ?? 0
        });

        await _unitOfWork.CommitAsync();
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
        return OperationResult<ResponseEntity>.SuccessResult(result);
    }
}
