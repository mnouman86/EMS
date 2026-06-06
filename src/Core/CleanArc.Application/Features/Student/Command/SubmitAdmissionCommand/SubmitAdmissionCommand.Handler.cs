using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArc.Application.Features.Student.Command.SubmitAdmissionCommand;

internal class SubmitAdmissionCommandHandler : IRequestHandler<SubmitAdmissionCommand, OperationResult<ResponseEntity>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    private readonly ILogger<SubmitAdmissionCommandHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SubmitAdmissionCommandHandler(
        IUnitOfWork unitOfWork,
        IAppUserManager userManager,
        ILogger<SubmitAdmissionCommandHandler> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async ValueTask<OperationResult<ResponseEntity>> Handle(SubmitAdmissionCommand request, CancellationToken cancellationToken)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);

        // For parent-online submissions, UserId may be 0 (no internal account). Admin-on-behalf submissions
        // will have a valid UserId. Either path passes through to the SP which records CreatedBy = 0 for
        // anonymous submissions.
        var createdBy = 0;
        if (request.UserId > 0)
        {
            var user = await _userManager.GetUserByIdAsync(request.UserId);
            if (user == null)
                return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            createdBy = user.Id;
        }

        var result = await _unitOfWork.StudentRepository.AddAsync(new Domain.Entities.Student.Student
        {
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
            DeclarationAccepted = request.DeclarationAccepted,
            DeclarationAcceptedAt = DateTime.UtcNow,
            SignatureName = request.SignatureName,
            SignatureImagePath = request.SignatureImagePath,
            SignatureDate = request.SignatureDate ?? DateTime.UtcNow,
            Status = "Applied",
            CreatedBy = createdBy,
            CultureId = request.CultureId
        });

        await _unitOfWork.CommitAsync();
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
        return OperationResult<ResponseEntity>.SuccessResult(result);
    }
}
