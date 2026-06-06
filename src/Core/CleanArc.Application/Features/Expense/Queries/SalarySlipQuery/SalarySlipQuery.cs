using CleanArc.Application.Contracts.Pdf;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Expense;
using CleanArc.Application.Models.Request;
using Mediator;
using System.Globalization;

namespace CleanArc.Application.Features.Expense.Queries.SalarySlipQuery
{
    /* EXP-05: Build salary slip PDF bytes for a single payroll entry */
    public record GetSalarySlipPdfQuery(int PayrollEntryId, bool IncludeBankDetails)
        : IRequest<OperationResult<SalarySlipDownloadResult>>;

    public class SalarySlipDownloadResult
    {
        public byte[] Bytes { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; } = "application/pdf";
    }

    internal class GetSalarySlipPdfQueryHandler : IRequestHandler<GetSalarySlipPdfQuery, OperationResult<SalarySlipDownloadResult>>
    {
        private readonly IUnitOfWork _u;
        private readonly IPdfRenderer<SalarySlipModel> _pdf;
        public GetSalarySlipPdfQueryHandler(IUnitOfWork u, IPdfRenderer<SalarySlipModel> pdf) { _u = u; _pdf = pdf; }

        public async ValueTask<OperationResult<SalarySlipDownloadResult>> Handle(GetSalarySlipPdfQuery r, CancellationToken ct)
        {
            var entry = await _u.ExpenseRepository.GetPayrollEntryAsync(r.PayrollEntryId);
            if (entry.Code != 200 || entry.Data == null)
                return OperationResult<SalarySlipDownloadResult>.FailureResult("Payroll entry not found", 404);

            string bankName = null, accountNumber = null;
            if (r.IncludeBankDetails)
            {
                var emp = await _u.EmployeeRepository.GetByIdAsync(new SearchRequestById { Id = entry.Data.EmployeeId });
                if (emp.Code == 200 && emp.Data != null)
                {
                    bankName = emp.Data.BankName;
                    accountNumber = emp.Data.AccountNumber;
                }
            }

            var model = new SalarySlipModel
            {
                SlipNo = entry.Data.SalarySlipNo,
                EmployeeCode = entry.Data.EmployeeCode,
                EmployeeFullName = entry.Data.EmployeeFullName,
                Designation = entry.Data.Designation,
                BankName = bankName,
                AccountNumber = accountNumber,
                BasicSalary = entry.Data.BasicSalary,
                Allowances = entry.Data.Allowances,
                Gross = entry.Data.Gross,
                FixedDeductions = entry.Data.FixedDeductions,
                AdvanceDeduction = entry.Data.AdvanceDeduction,
                FineDeduction = entry.Data.FineDeduction,
                OtherDeduction = entry.Data.OtherDeduction,
                TotalDeductions = entry.Data.TotalDeductions,
                NetPayable = entry.Data.NetPayable
            };

            // Month/Year are on the PayrollRun — extract from slip number (SAL-YYYY-MM-EMPID) as a quick proxy
            if (!string.IsNullOrEmpty(entry.Data.SalarySlipNo))
            {
                var parts = entry.Data.SalarySlipNo.Split('-');
                if (parts.Length >= 3 && int.TryParse(parts[1], out var y) && int.TryParse(parts[2], out var mo))
                { model.Year = y; model.Month = mo; }
            }

            var bytes = _pdf.Render(model);
            var monthName = model.Month > 0 ? CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(model.Month) : "";
            var fileName = $"TSSS_Salary_{model.EmployeeCode}_{monthName}_{model.Year}.pdf";
            return OperationResult<SalarySlipDownloadResult>.SuccessResult(new SalarySlipDownloadResult { Bytes = bytes, FileName = fileName });
        }
    }
}
