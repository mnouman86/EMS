using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArc.Application.Features.SchoolClass.Command.CreateSchoolClassCommand;

internal class CreateSchoolClassCommandHandler : IRequestHandler<CreateSchoolClassCommand, OperationResult<ResponseEntity>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    private readonly ILogger<CreateSchoolClassCommandHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateSchoolClassCommandHandler(
        IUnitOfWork unitOfWork,
        IAppUserManager userManager,
        ILogger<CreateSchoolClassCommandHandler> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async ValueTask<OperationResult<ResponseEntity>> Handle(CreateSchoolClassCommand request, CancellationToken cancellationToken)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);

        var user = await _userManager.GetUserByIdAsync(request.UserId);
        if (user == null)
            return OperationResult<ResponseEntity>.FailureResult("User Not Found");

        var result = await _unitOfWork.SchoolClassRepository.AddAsync(new Domain.Entities.SchoolClass.SchoolClass
        {
            LevelName = request.LevelName,
            LevelCode = request.LevelCode?.ToUpperInvariant(),
            GradeNumber = request.GradeNumber,
            DisplayOrder = request.DisplayOrder,
            Capacity = request.Capacity,
            ClassTeacherId = request.ClassTeacherId,
            IsActive = request.IsActive,
            CreatedBy = user.Id,
            CultureId = request.CultureId
        });

        await _unitOfWork.CommitAsync();
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
        return OperationResult<ResponseEntity>.SuccessResult(result);
    }
}
