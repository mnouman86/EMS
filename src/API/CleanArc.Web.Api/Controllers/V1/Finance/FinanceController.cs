using Asp.Versioning;
using CleanArc.Application.Features.Finance.Command.SetOpeningCashBalanceCommand;
using CleanArc.Application.Features.Finance.Queries.CashFlowQueries;
using CleanArc.Application.Features.Finance.Queries.ExpenseDashboardQueries;
using CleanArc.Application.Features.Finance.Queries.IncomeQueries;
using CleanArc.Application.Features.Finance.Queries.PnLPdfQuery;
using CleanArc.Application.Features.Finance.Queries.ReportQueries;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CleanArc.Web.Api.Controllers.V1.Finance;

/// <summary>
/// Finance & Accounts — FIN-01..FIN-05. Pure read/aggregation layer over Fee + Expense.
/// All reads gated to FinanceRoles (Admin/Principal/Accountant). Setting opening balance is Admin-only.
/// </summary>
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/Finance")]
public class FinanceController : ControllerBase
{
    private readonly ISender _sender;
    public FinanceController(ISender sender) { _sender = sender; }

    private int CurrentUserId
        => int.TryParse(User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? User?.Identity?.Name, out var id) ? id : 0;

    private IActionResult Wrap<T>(OperationResult<T> r)
    {
        if (r is null) return StatusCode(500, new { Message = "Server Error" });
        return StatusCode(r.StatusCode, new { Data = r.Result, r.Message, r.StatusCode, r.IsSuccess, r.TotalCount });
    }

    /* ---------- FIN-01 Income ---------- */

    [Authorize,HttpPost("FinanceGetIncomeSummary")]
    public async Task<IActionResult> IncomeSummary([FromBody] GetIncomeSummaryQuery q) => Wrap(await _sender.Send(q));

    [Authorize,HttpPost("FinanceGetIncomeByMonth")]
    public async Task<IActionResult> IncomeByMonth([FromBody] GetIncomeByMonthQuery q) => Wrap(await _sender.Send(q));

    [Authorize,HttpPost("FinanceGetIncomeByClass")]
    public async Task<IActionResult> IncomeByClass([FromBody] GetIncomeByClassQuery q) => Wrap(await _sender.Send(q));

    [Authorize,HttpPost("FinanceGetIncomeByFeeType")]
    public async Task<IActionResult> IncomeByFeeType([FromBody] GetIncomeByFeeTypeQuery q) => Wrap(await _sender.Send(q));

    /* ---------- FIN-02 Expense ---------- */

    [Authorize,HttpPost("FinanceGetExpenseSummary")]
    public async Task<IActionResult> ExpenseSummary([FromBody] GetExpenseSummaryQuery q) => Wrap(await _sender.Send(q));

    [Authorize,HttpPost("FinanceGetExpenseByMonth")]
    public async Task<IActionResult> ExpenseByMonth([FromBody] GetExpenseByMonthQuery q) => Wrap(await _sender.Send(q));

    [Authorize,HttpPost("FinanceGetExpenseByCategory")]
    public async Task<IActionResult> ExpenseByCategory([FromBody] GetExpenseByCategoryQuery q) => Wrap(await _sender.Send(q));

    /* ---------- FIN-03 P&L ---------- */

    [Authorize,HttpPost("FinanceGetPnL")]
    public async Task<IActionResult> PnL([FromBody] GetPnLStatementQuery q) => Wrap(await _sender.Send(q));

    [Authorize,HttpPost("FinanceGetPnLPdf")]
    public async Task<IActionResult> PnLPdf([FromBody] GetPnLPdfQuery q)
    {
        var res = await _sender.Send(q);
        if (!res.IsSuccess || res.Result?.Bytes == null)
            return StatusCode(res?.StatusCode ?? 500, new { Message = res?.Message ?? "Server Error" });
        return File(res.Result.Bytes, res.Result.ContentType, res.Result.FileName);
    }

    /* ---------- FIN-04 Reports ---------- */

    [Authorize,HttpPost("FinanceGetMonthlySummary")]
    public async Task<IActionResult> MonthlySummary([FromBody] GetMonthlySummaryQuery q) => Wrap(await _sender.Send(q));

    [Authorize,HttpPost("FinanceGetAnnualSummary")]
    public async Task<IActionResult> AnnualSummary([FromBody] GetAnnualSummaryQuery q) => Wrap(await _sender.Send(q));

    [Authorize,HttpPost("FinanceGetCategoryWiseExpense")]
    public async Task<IActionResult> CategoryWiseExpense([FromBody] GetCategoryWiseExpenseQuery q) => Wrap(await _sender.Send(q));

    [Authorize,HttpPost("FinanceGetFeeCollectionVsTarget")]
    public async Task<IActionResult> FeeVsTarget([FromBody] GetFeeCollectionVsTargetQuery q) => Wrap(await _sender.Send(q));

    /* ---------- FIN-05 Cash Flow ---------- */

    [Authorize,HttpPost("FinanceSetOpeningBalance")]
    public async Task<IActionResult> SetOpening([FromBody] SetOpeningCashBalanceCommand cmd) { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [Authorize,HttpPost("FinanceGetOpeningBalance")]
    public async Task<IActionResult> GetOpening([FromBody] GetOpeningCashBalanceQuery q) => Wrap(await _sender.Send(q));

    [Authorize,HttpPost("FinanceGetCashFlowLedger")]
    public async Task<IActionResult> CashFlow([FromBody] GetCashFlowLedgerQuery q) => Wrap(await _sender.Send(q));
}
