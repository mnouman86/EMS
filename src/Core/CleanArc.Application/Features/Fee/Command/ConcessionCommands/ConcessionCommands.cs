using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Fee;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Fee.Command.ConcessionCommands
{
    /* FEE-09: Grant a concession (approval required for high-value by API gate) */
    public record GrantConcessionCommand(
        int StudentId, string? ConcessionType, decimal Value,
        List<int>? ApplicableFeeTypeIds,
        DateTime EffectiveFrom, DateTime? EffectiveTo, string? Reason, int? ApprovedByUserId)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<GrantConcessionCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<GrantConcessionCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<GrantConcessionCommand> v)
        {
            v.RuleFor(c => c.StudentId).GreaterThan(0);
            v.RuleFor(c => c.ConcessionType).NotEmpty()
                .Must(x => x == null || new[] { "Percentage", "Fixed", "Sibling", "FullWaiver" }.Contains(x))
                .WithMessage("ConcessionType must be Percentage / Fixed / Sibling / FullWaiver");
            v.RuleFor(c => c.Value).GreaterThanOrEqualTo(0);
            v.When(c => c.ConcessionType == "Percentage", () =>
                v.RuleFor(c => c.Value).InclusiveBetween(0, 100).WithMessage("Percentage must be 0..100"));
            return v;
        }
    }

    internal class GrantConcessionCommandHandler : IRequestHandler<GrantConcessionCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public GrantConcessionCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(GrantConcessionCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.FeeRepository.GrantConcessionAsync(new GrantConcessionDTO
            {
                StudentId = r.StudentId,
                ConcessionType = r.ConcessionType,
                Value = r.Value,
                ApplicableFeeTypeIdsCsv = r.ApplicableFeeTypeIds == null || r.ApplicableFeeTypeIds.Count == 0
                    ? string.Empty
                    : string.Join(",", r.ApplicableFeeTypeIds),
                EffectiveFrom = r.EffectiveFrom,
                EffectiveTo = r.EffectiveTo,
                Reason = r.Reason,
                ApprovedByUserId = r.ApprovedByUserId,
                CreatedBy = user.Id
            });
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    /* FEE-09: Revoke an active concession */
    public record RevokeConcessionCommand(int ConcessionId, string? Reason)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<RevokeConcessionCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<RevokeConcessionCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<RevokeConcessionCommand> v)
        {
            v.RuleFor(c => c.ConcessionId).GreaterThan(0);
            v.RuleFor(c => c.Reason).NotEmpty();
            return v;
        }
    }

    internal class RevokeConcessionCommandHandler : IRequestHandler<RevokeConcessionCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public RevokeConcessionCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(RevokeConcessionCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.FeeRepository.RevokeConcessionAsync(new RevokeConcessionDTO
            {
                ConcessionId = r.ConcessionId, Reason = r.Reason, UpdatedBy = user.Id
            });
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }
}
