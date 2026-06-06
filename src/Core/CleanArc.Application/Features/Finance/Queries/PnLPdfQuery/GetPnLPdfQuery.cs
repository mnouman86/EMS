using CleanArc.Application.Contracts.Pdf;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Finance;
using Mediator;
using System;

namespace CleanArc.Application.Features.Finance.Queries.PnLPdfQuery
{
    /* FIN-03 PDF export */
    public record GetPnLPdfQuery(DateTime FromDate, DateTime ToDate, string? PeriodLabel, string? GeneratedBy)
        : IRequest<OperationResult<PnLPdfDownloadResult>>;

    public class PnLPdfDownloadResult
    {
        public byte[] Bytes { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; } = "application/pdf";
    }

    internal class GetPnLPdfQueryHandler : IRequestHandler<GetPnLPdfQuery, OperationResult<PnLPdfDownloadResult>>
    {
        private readonly IUnitOfWork _u;
        private readonly IPdfRenderer<PnLStatementModel> _pdf;
        public GetPnLPdfQueryHandler(IUnitOfWork u, IPdfRenderer<PnLStatementModel> pdf) { _u = u; _pdf = pdf; }

        public async ValueTask<OperationResult<PnLPdfDownloadResult>> Handle(GetPnLPdfQuery r, CancellationToken ct)
        {
            var rows = await _u.FinanceRepository.GetPnLAsync(r.FromDate, r.ToDate);
            if (rows.Code != 200)
                return OperationResult<PnLPdfDownloadResult>.FailureResult(rows.Message, rows.Code);

            var model = new PnLStatementModel
            {
                PeriodLabel = r.PeriodLabel ?? $"{r.FromDate:dd-MM-yyyy} → {r.ToDate:dd-MM-yyyy}",
                GeneratedBy = r.GeneratedBy
            };
            decimal income = 0, expense = 0;
            foreach (var row in rows.Data)
            {
                model.Lines.Add(new PnLStatementLine { Section = row.Section, Line = row.Line, Amount = row.Amount });
                if (string.Equals(row.Section, "Income", StringComparison.OrdinalIgnoreCase)) income += row.Amount;
                else if (string.Equals(row.Section, "Expense", StringComparison.OrdinalIgnoreCase)) expense += row.Amount;
            }
            model.NetSurplusOrDeficit = income - expense;

            var bytes = _pdf.Render(model);
            var fileName = $"TSSS_PnL_{r.FromDate:yyyyMMdd}_{r.ToDate:yyyyMMdd}.pdf";
            return OperationResult<PnLPdfDownloadResult>.SuccessResult(new PnLPdfDownloadResult { Bytes = bytes, FileName = fileName });
        }
    }
}
