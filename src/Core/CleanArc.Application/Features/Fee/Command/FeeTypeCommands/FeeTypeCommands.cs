using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Fee;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Fee.Command.FeeTypeCommands
{
    /* FEE-01: Upsert fee type */
    public record UpsertFeeTypeCommand(int? Id, string? Name, string? Code, string? Category, bool IsActive)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<UpsertFeeTypeCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<UpsertFeeTypeCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpsertFeeTypeCommand> v)
        {
            v.RuleFor(c => c.Name).NotEmpty().MaximumLength(100);
            v.RuleFor(c => c.Code).NotEmpty().MaximumLength(20);
            v.RuleFor(c => c.Category).NotEmpty()
                .Must(x => x == null || new[] { "OneTime", "Monthly", "Annual", "Periodic" }.Contains(x))
                .WithMessage("Category must be OneTime / Monthly / Annual / Periodic");
            return v;
        }
    }

    internal class UpsertFeeTypeCommandHandler : IRequestHandler<UpsertFeeTypeCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public UpsertFeeTypeCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(UpsertFeeTypeCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.FeeRepository.UpsertFeeTypeAsync(new UpsertFeeTypeDTO
            {
                Id = r.Id, Name = r.Name, Code = r.Code, Category = r.Category,
                IsActive = r.IsActive, UpdatedBy = user.Id
            });
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    /* FEE-01: Delete (soft/hard via DeleteRequest.ForceHard) */
    public record DeleteFeeTypeCommand(DeleteRequest deleteRequest)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<DeleteFeeTypeCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<DeleteFeeTypeCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteFeeTypeCommand> v)
        {
            v.RuleFor(c => c.deleteRequest.SelectedIds).NotEmpty();
            return v;
        }
    }

    internal class DeleteFeeTypeCommandHandler : IRequestHandler<DeleteFeeTypeCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public DeleteFeeTypeCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(DeleteFeeTypeCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.FeeRepository.DeleteFeeTypeAsync(r.deleteRequest, user.Id);
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    /* FEE-01: Upsert versioned amount per (FeeType, Class) */
    public record UpsertFeeTypeAmountCommand(int FeeTypeId, int SchoolClassId, decimal Amount, System.DateTime EffectiveFrom)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<UpsertFeeTypeAmountCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<UpsertFeeTypeAmountCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpsertFeeTypeAmountCommand> v)
        {
            v.RuleFor(c => c.FeeTypeId).GreaterThan(0);
            v.RuleFor(c => c.SchoolClassId).GreaterThan(0);
            v.RuleFor(c => c.Amount).GreaterThanOrEqualTo(0);
            return v;
        }
    }

    internal class UpsertFeeTypeAmountCommandHandler : IRequestHandler<UpsertFeeTypeAmountCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public UpsertFeeTypeAmountCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(UpsertFeeTypeAmountCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.FeeRepository.UpsertFeeTypeAmountAsync(new UpsertFeeTypeAmountDTO
            {
                FeeTypeId = r.FeeTypeId, SchoolClassId = r.SchoolClassId,
                Amount = r.Amount, EffectiveFrom = r.EffectiveFrom, CreatedBy = user.Id
            });
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    /* FEE-01: Configure billed months for a monthly fee type */
    public record ConfigureFeeCalendarCommand(int FeeTypeId, int AcademicYearId, List<int> BilledMonths)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<ConfigureFeeCalendarCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<ConfigureFeeCalendarCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<ConfigureFeeCalendarCommand> v)
        {
            v.RuleFor(c => c.FeeTypeId).GreaterThan(0);
            v.RuleFor(c => c.AcademicYearId).GreaterThan(0);
            v.RuleFor(c => c.BilledMonths).NotNull().NotEmpty();
            v.RuleForEach(c => c.BilledMonths).InclusiveBetween(1, 12);
            return v;
        }
    }

    internal class ConfigureFeeCalendarCommandHandler : IRequestHandler<ConfigureFeeCalendarCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public ConfigureFeeCalendarCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(ConfigureFeeCalendarCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.FeeRepository.ConfigureFeeCalendarAsync(new ConfigureFeeCalendarDTO
            {
                FeeTypeId = r.FeeTypeId, AcademicYearId = r.AcademicYearId,
                BilledMonthsCsv = string.Join(",", r.BilledMonths),
                UpdatedBy = user.Id
            });
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }
}
