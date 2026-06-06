using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Fee;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Fee;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Fee.Command.InvoiceCommands
{
    /* FEE-02: Generate monthly invoices (DryRun=true returns preview only) */
    public record GenerateMonthlyInvoicesCommand(
        int AcademicYearId, int BillingMonth, int BillingYear, int? ClassId,
        List<int>? ExcludedStudentIds, bool DryRun)
        : IRequest<OperationResult<List<InvoicePreviewRow>>>, IValidatableModel<GenerateMonthlyInvoicesCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<GenerateMonthlyInvoicesCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<GenerateMonthlyInvoicesCommand> v)
        {
            v.RuleFor(c => c.AcademicYearId).GreaterThan(0);
            v.RuleFor(c => c.BillingMonth).InclusiveBetween(1, 12);
            v.RuleFor(c => c.BillingYear).InclusiveBetween(2000, 2100);
            return v;
        }
    }

    internal class GenerateMonthlyInvoicesCommandHandler : IRequestHandler<GenerateMonthlyInvoicesCommand, OperationResult<List<InvoicePreviewRow>>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public GenerateMonthlyInvoicesCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<InvoicePreviewRow>>> Handle(GenerateMonthlyInvoicesCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<List<InvoicePreviewRow>>.FailureResult("User Not Found");
            var res = await _u.FeeRepository.GenerateMonthlyInvoicesAsync(new GenerateMonthlyInvoicesDTO
            {
                AcademicYearId = r.AcademicYearId,
                BillingMonth = r.BillingMonth,
                BillingYear = r.BillingYear,
                ClassId = r.ClassId,
                ExcludedStudentIdsCsv = r.ExcludedStudentIds == null || r.ExcludedStudentIds.Count == 0
                    ? string.Empty
                    : string.Join(",", r.ExcludedStudentIds),
                DryRun = r.DryRun,
                CreatedBy = user.Id
            });
            await _u.CommitAsync();
            if (res.Code != 200) return OperationResult<List<InvoicePreviewRow>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<InvoicePreviewRow>>.SuccessResult(res.Data.ToList(), res.Code, res.Message, res.TotalCount);
        }
    }

    /* FEE-02: Cancel an invoice */
    public record CancelInvoiceCommand(int InvoiceId, string? Reason)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<CancelInvoiceCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<CancelInvoiceCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CancelInvoiceCommand> v)
        {
            v.RuleFor(c => c.InvoiceId).GreaterThan(0);
            v.RuleFor(c => c.Reason).NotEmpty();
            return v;
        }
    }

    internal class CancelInvoiceCommandHandler : IRequestHandler<CancelInvoiceCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public CancelInvoiceCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(CancelInvoiceCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.FeeRepository.CancelInvoiceAsync(new CancelInvoiceDTO
            {
                InvoiceId = r.InvoiceId, Reason = r.Reason, UpdatedBy = user.Id
            });
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }
}
