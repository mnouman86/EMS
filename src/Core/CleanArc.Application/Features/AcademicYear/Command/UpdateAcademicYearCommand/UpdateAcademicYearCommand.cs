using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.AcademicYear;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.AcademicYear.Command.UpdateAcademicYearCommand
{
    public record UpdateAcademicYearCommand(int Id, string? DisplayName, DateTime StartDate, DateTime EndDate)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<UpdateAcademicYearCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<UpdateAcademicYearCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateAcademicYearCommand> v)
        {
            v.RuleFor(c => c.Id).GreaterThan(0);
            v.RuleFor(c => c.DisplayName).NotEmpty();
            v.RuleFor(c => c).Must(c => c.EndDate > c.StartDate).WithMessage("EndDate must be after StartDate");
            return v;
        }
    }

    internal class UpdateAcademicYearCommandHandler : IRequestHandler<UpdateAcademicYearCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public UpdateAcademicYearCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(UpdateAcademicYearCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.AcademicYearRepository.UpdateAsync(new UpdateAcademicYearDTO
            {
                Id = r.Id, DisplayName = r.DisplayName, StartDate = r.StartDate, EndDate = r.EndDate, UpdatedBy = user.Id
            });
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }
}
