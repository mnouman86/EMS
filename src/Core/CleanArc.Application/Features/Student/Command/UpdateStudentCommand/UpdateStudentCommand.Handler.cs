using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArc.Application.Features.Student.Command.UpdateStudentCommand;

internal class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, OperationResult<ResponseEntity>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    private readonly ILogger<UpdateStudentCommandHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateStudentCommandHandler(
        IUnitOfWork unitOfWork,
        IAppUserManager userManager,
        ILogger<UpdateStudentCommandHandler> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async ValueTask<OperationResult<ResponseEntity>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);

        var user = await _userManager.GetUserByIdAsync(request.UserId);
        if (user == null)
            return OperationResult<ResponseEntity>.FailureResult("User Not Found");

        var result = await _unitOfWork.StudentRepository.UpdateAsync(new Domain.Entities.Student.Student
        {
            Id = request.Id,
            FullName = request.FullName,
            Gender = request.Gender,
            Religion = request.Religion,
            DateOfBirth = request.DateOfBirth,
            GradeApplyingForId = request.GradeApplyingForId,
            ParentName = request.ParentName,
            ParentRelationship = request.ParentRelationship,
            ParentEmail = request.ParentEmail,
            ParentMobile = request.ParentMobile,
            HomeAddress = request.HomeAddress,
            EmergencyContactName = request.EmergencyContactName,
            EmergencyContactPhone = request.EmergencyContactPhone,
            HasMedicalConditions = request.HasMedicalConditions,
            MedicalDetails = request.MedicalDetails,
            PreviousSchoolName = request.PreviousSchoolName,
            PreviousSchoolClass = request.PreviousSchoolClass,
            PreviousSchoolDateLeft = request.PreviousSchoolDateLeft,
            UpdatedBy = user.Id,
            CultureId = request.CultureId
        });

        await _unitOfWork.CommitAsync();
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
        return OperationResult<ResponseEntity>.SuccessResult(result);
    }
}
