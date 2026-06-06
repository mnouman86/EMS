using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Fee;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Fee.Command.ArrearsCommands
{
    /* FEE-12: Carry forward outstanding balances to next session */
    public record CarryForwardArrearsCommand(int FromAcademicYearId, int ToAcademicYearId)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<CarryForwardArrearsCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<CarryForwardArrearsCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CarryForwardArrearsCommand> v)
        {
            v.RuleFor(c => c.FromAcademicYearId).GreaterThan(0);
            v.RuleFor(c => c.ToAcademicYearId).GreaterThan(0);
            v.RuleFor(c => c).Must(c => c.FromAcademicYearId != c.ToAcademicYearId)
                .WithMessage("From and To academic years must differ");
            return v;
        }
    }

    internal class CarryForwardArrearsCommandHandler : IRequestHandler<CarryForwardArrearsCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public CarryForwardArrearsCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(CarryForwardArrearsCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.FeeRepository.CarryForwardArrearsAsync(new CarryForwardArrearsDTO
            {
                FromAcademicYearId = r.FromAcademicYearId,
                ToAcademicYearId = r.ToAcademicYearId,
                UpdatedBy = user.Id
            });
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    /* FEE-12: Write off a specific arrear (Principal-approved) */
    public record WriteOffArrearCommand(int ArrearId, string? Reason, int? ApprovedByUserId)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<WriteOffArrearCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<WriteOffArrearCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<WriteOffArrearCommand> v)
        {
            v.RuleFor(c => c.ArrearId).GreaterThan(0);
            v.RuleFor(c => c.Reason).NotEmpty();
            return v;
        }
    }

    internal class WriteOffArrearCommandHandler : IRequestHandler<WriteOffArrearCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public WriteOffArrearCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(WriteOffArrearCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.FeeRepository.WriteOffArrearAsync(new WriteOffArrearDTO
            {
                ArrearId = r.ArrearId,
                Reason = r.Reason,
                ApprovedByUserId = r.ApprovedByUserId ?? user.Id
            });
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }
}
