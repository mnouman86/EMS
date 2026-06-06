using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Finance;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Finance.Command.SetOpeningCashBalanceCommand
{
    /* FIN-05: Admin sets the opening cash balance for an academic year */
    public record SetOpeningCashBalanceCommand(int AcademicYearId, decimal OpeningAmount, DateTime AsOfDate)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<SetOpeningCashBalanceCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<SetOpeningCashBalanceCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<SetOpeningCashBalanceCommand> v)
        {
            v.RuleFor(c => c.AcademicYearId).GreaterThan(0);
            v.RuleFor(c => c.OpeningAmount).GreaterThanOrEqualTo(0);
            return v;
        }
    }

    internal class SetOpeningCashBalanceCommandHandler : IRequestHandler<SetOpeningCashBalanceCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public SetOpeningCashBalanceCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(SetOpeningCashBalanceCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.FinanceRepository.SetOpeningCashBalanceAsync(new SetOpeningCashBalanceDTO
            { AcademicYearId = r.AcademicYearId, OpeningAmount = r.OpeningAmount, AsOfDate = r.AsOfDate, SetByUserId = user.Id });
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }
}
