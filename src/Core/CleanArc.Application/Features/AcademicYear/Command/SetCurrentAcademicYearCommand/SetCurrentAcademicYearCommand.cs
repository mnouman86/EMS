using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.AcademicYear;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.AcademicYear.Command.SetCurrentAcademicYearCommand
{
    public record SetCurrentAcademicYearCommand(int Id)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<SetCurrentAcademicYearCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<SetCurrentAcademicYearCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<SetCurrentAcademicYearCommand> v)
        {
            v.RuleFor(c => c.Id).GreaterThan(0);
            return v;
        }
    }

    internal class SetCurrentAcademicYearCommandHandler : IRequestHandler<SetCurrentAcademicYearCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public SetCurrentAcademicYearCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(SetCurrentAcademicYearCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.AcademicYearRepository.SetCurrentAsync(new SetCurrentAcademicYearDTO { Id = r.Id, UpdatedBy = user.Id });
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }
}
