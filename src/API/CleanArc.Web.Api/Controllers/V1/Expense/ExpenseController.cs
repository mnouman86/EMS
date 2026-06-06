using Asp.Versioning;
using CleanArc.Application.Features.Expense.Command.CategoryCommands;
using CleanArc.Application.Features.Expense.Command.ExpenseCommands;
using CleanArc.Application.Features.Expense.Command.PayrollCommands;
using CleanArc.Application.Features.Expense.Command.RecurringCommands;
using CleanArc.Application.Features.Expense.Queries.CategoryQueries;
using CleanArc.Application.Features.Expense.Queries.ExpenseQueries;
using CleanArc.Application.Features.Expense.Queries.PayrollQueries;
using CleanArc.Application.Features.Expense.Queries.SalarySlipQuery;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CleanArc.Web.Api.Controllers.V1.Expense;

/// <summary>
/// Expense Management — EXP-01..EXP-05.
/// Admin/Accountant own configuration + entry; Principal can view dashboards.
/// </summary>
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/Expense")]
public class ExpenseController : ControllerBase
{
    private readonly ISender _sender;
    public ExpenseController(ISender sender) { _sender = sender; }

    private int CurrentUserId
        => int.TryParse(User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? User?.Identity?.Name, out var id) ? id : 0;

    private IActionResult Wrap<T>(OperationResult<T> r)
    {
        if (r is null) return StatusCode(500, new { Message = "Server Error" });
        return StatusCode(r.StatusCode, new { Data = r.Result, r.Message, r.StatusCode, r.IsSuccess, r.TotalCount });
    }

    /* ---------- EXP-01: Categories ---------- */

    [Authorize,HttpPost("ExpenseUpsertCategory")]
    public async Task<IActionResult> UpsertCategory([FromBody] UpsertExpenseCategoryCommand cmd) { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [Authorize,HttpPost("ExpenseDeleteCategory")]
    public async Task<IActionResult> DeleteCategory([FromBody] DeleteExpenseCategoryCommand cmd) { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [Authorize,HttpPost("ExpenseGetCategories")]
    public async Task<IActionResult> GetCategories([FromBody] GetExpenseCategoriesQuery q) => Wrap(await _sender.Send(q));

    /* ---------- EXP-02: Recurring templates + Record + Generate ---------- */

    [Authorize,HttpPost("ExpenseUpsertRecurringTemplate")]
    public async Task<IActionResult> UpsertTemplate([FromBody] UpsertRecurringTemplateCommand cmd) { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [Authorize,HttpPost("ExpenseGetRecurringTemplates")]
    public async Task<IActionResult> GetTemplates() => Wrap(await _sender.Send(new GetRecurringTemplatesQuery()));

    [Authorize,HttpPost("ExpenseGenerateRecurring")]
    public async Task<IActionResult> GenerateRecurring([FromBody] GenerateRecurringExpensesCommand cmd) { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [Authorize,HttpPost("ExpenseRecord")]
    public async Task<IActionResult> Record([FromBody] RecordExpenseCommand cmd) { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [Authorize,HttpPost("ExpenseDelete")]
    public async Task<IActionResult> Delete([FromBody] DeleteExpenseCommand cmd) { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    /* ---------- EXP-03: List ---------- */

    [Authorize,HttpPost("ExpenseGetAll")]
    public async Task<IActionResult> GetAll([FromBody] GetExpensesQuery q) => Wrap(await _sender.Send(q));

    /* ---------- EXP-04: Budget monitoring ---------- */

    [Authorize,HttpPost("ExpenseGetBudgetMonitoring")]
    public async Task<IActionResult> BudgetMonitoring([FromBody] GetBudgetMonitoringQuery q) => Wrap(await _sender.Send(q));

    /* ---------- EXP-05: Payroll ---------- */

    [Authorize,HttpPost("ExpenseStartPayroll")]
    public async Task<IActionResult> StartPayroll([FromBody] StartPayrollRunCommand cmd) { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [Authorize,HttpPost("ExpenseAdjustPayrollEntry")]
    public async Task<IActionResult> AdjustEntry([FromBody] AdjustPayrollEntryCommand cmd) { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [Authorize,HttpPost("ExpenseConfirmPayroll")]
    public async Task<IActionResult> ConfirmPayroll([FromBody] ConfirmPayrollRunCommand cmd) { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [Authorize,HttpPost("ExpenseGetPayrollRuns")]
    public async Task<IActionResult> GetPayrollRuns([FromBody] GetPayrollRunsQuery q) => Wrap(await _sender.Send(q));

    [Authorize,HttpPost("ExpenseGetPayrollEntries")]
    public async Task<IActionResult> GetEntries([FromBody] GetPayrollEntriesQuery q) => Wrap(await _sender.Send(q));

    [Authorize, HttpPost("ExpenseGetSalarySlipPdf")]
    public async Task<IActionResult> SlipPdf([FromBody] GetSalarySlipPdfQuery q)
    {
        var res = await _sender.Send(q);
        if (!res.IsSuccess || res.Result?.Bytes == null)
            return StatusCode(res?.StatusCode ?? 500, new { Message = res?.Message ?? "Server Error" });
        return File(res.Result.Bytes, res.Result.ContentType, res.Result.FileName);
    }
}
