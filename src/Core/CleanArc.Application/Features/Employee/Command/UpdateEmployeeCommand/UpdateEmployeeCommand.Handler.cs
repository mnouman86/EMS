using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArc.Application.Features.Employee.Command.UpdateEmployeeCommand;

internal class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, OperationResult<ResponseEntity>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    private readonly ILogger<UpdateEmployeeCommandHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateEmployeeCommandHandler(
        IUnitOfWork unitOfWork,
        IAppUserManager userManager,
        ILogger<UpdateEmployeeCommandHandler> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async ValueTask<OperationResult<ResponseEntity>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);

        var user = await _userManager.GetUserByIdAsync(request.UserId);
        if (user == null)
            return OperationResult<ResponseEntity>.FailureResult("User Not Found");

        var entity = new Domain.Entities.Employee.Employee
        {
            Id = request.Id,
            FullName = request.FullName,
            FatherOrHusbandName = request.FatherOrHusbandName,
            Relation = request.Relation,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            MaritalStatus = request.MaritalStatus,
            Nationality = request.Nationality,
            Religion = request.Religion,
            CNIC = request.CNIC,
            CNICExpiry = request.CNICExpiry,
            BloodGroup = request.BloodGroup,
            MedicalCondition = request.MedicalCondition,
            Designation = request.Designation,
            Department = request.Department,
            DateOfJoining = request.DateOfJoining,
            EmploymentType = request.EmploymentType,
            PersonalMobile = request.PersonalMobile,
            Whatsapp = request.Whatsapp,
            Landline = request.Landline,
            AlternateMobile = request.AlternateMobile,
            OfficialEmail = request.OfficialEmail,
            PersonalEmail = request.PersonalEmail,
            PresentAddress = request.PresentAddress,
            PermanentAddress = request.PermanentSameAsPresent ? request.PresentAddress : request.PermanentAddress,
            PermanentSameAsPresent = request.PermanentSameAsPresent,
            EmergencyContact1Name = request.EmergencyContact1Name,
            EmergencyContact1Relation = request.EmergencyContact1Relation,
            EmergencyContact1Mobile = request.EmergencyContact1Mobile,
            EmergencyContact2Name = request.EmergencyContact2Name,
            EmergencyContact2Relation = request.EmergencyContact2Relation,
            EmergencyContact2Mobile = request.EmergencyContact2Mobile,
            HighestDegree = request.HighestDegree,
            FieldOrMajor = request.FieldOrMajor,
            PassingYear = request.PassingYear,
            Institution = request.Institution,
            Certifications = request.Certifications,
            TotalExperienceYears = request.TotalExperienceYears,
            PreviousOrganisation = request.PreviousOrganisation,
            PreviousPosition = request.PreviousPosition,
            PreviousDuration = request.PreviousDuration,
            PreviousSubjectsTaught = request.PreviousSubjectsTaught,
            PreviousReasonForLeaving = request.PreviousReasonForLeaving,
            BankName = request.BankName,
            BankBranch = request.BankBranch,
            AccountTitle = request.AccountTitle,
            AccountNumber = request.AccountNumber,
            Reference1Name = request.Reference1Name,
            Reference1Designation = request.Reference1Designation,
            Reference1Organisation = request.Reference1Organisation,
            Reference1Contact = request.Reference1Contact,
            Reference2Name = request.Reference2Name,
            Reference2Designation = request.Reference2Designation,
            Reference2Organisation = request.Reference2Organisation,
            Reference2Contact = request.Reference2Contact,
            UpdatedBy = user.Id,
            CultureId = request.CultureId
        };

        var result = await _unitOfWork.EmployeeRepository.UpdateAsync(entity);
        await _unitOfWork.CommitAsync();
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
        return OperationResult<ResponseEntity>.SuccessResult(result);
    }
}
