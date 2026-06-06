using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.AcademicYear;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using Mediator;

namespace CleanArc.Application.Features.AcademicYear.Command.CreateAcademicYearCommand;

internal class CreateAcademicYearCommandHandler : IRequestHandler<CreateAcademicYearCommand, OperationResult<ResponseEntity>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    public CreateAcademicYearCommandHandler(IUnitOfWork u, IAppUserManager m) { _unitOfWork = u; _userManager = m; }

    public async ValueTask<OperationResult<ResponseEntity>> Handle(CreateAcademicYearCommand request, CancellationToken ct)
    {
        var user = await _userManager.GetUserByIdAsync(request.UserId);
        if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");

        var result = await _unitOfWork.AcademicYearRepository.CreateAsync(new CreateAcademicYearDTO
        {
            Code = request.Code,
            DisplayName = request.DisplayName,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            IsOpen = request.IsOpen,
            CreatedBy = user.Id
        });
        await _unitOfWork.CommitAsync();
        return OperationResult<ResponseEntity>.SuccessResult(result);
    }
}
