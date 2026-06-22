using Asp.Versioning;
using CleanArc.Application.Features.Fee.Command.ArrearsCommands;
using CleanArc.Application.Features.Fee.Command.ConcessionCommands;
using CleanArc.Application.Features.Fee.Command.FeeTypeCommands;
using CleanArc.Application.Features.Fee.Command.InvoiceCommands;
using CleanArc.Application.Features.Fee.Command.PaymentCommands;
using CleanArc.Application.Features.Fee.Command.ReminderCommands;
using CleanArc.Application.Features.Fee.Queries.ArrearsQueries;
using CleanArc.Application.Features.Fee.Queries.ClassBoardQueries;
using CleanArc.Application.Features.Fee.Queries.ConcessionQueries;
using CleanArc.Application.Features.Fee.Queries.DashboardQueries;
using CleanArc.Application.Features.Fee.Queries.FeeTypeQueries;
using CleanArc.Application.Features.Fee.Queries.LedgerQueries;
using CleanArc.Application.Features.Fee.Queries.ParentFeeSearch;
using CleanArc.Application.Features.Fee.Queries.ReceiptQuery;
using CleanArc.Application.Features.Fee.Queries.ReportQueries;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CleanArc.Web.Api.Controllers.V1.Fee;

/// <summary>
/// Fee Management module — FEE-01..FEE-14.
/// Bespoke controller (not _BaseController) — surface is workflow-oriented.
/// Role gating:
///   - Admin / Accountant: configuration, collection, reversal, reports
///   - Principal: read-only access via the same endpoints (verify via role checks)
///   - Parent: only FeeParentSearch (AllowAnonymous)
/// </summary>
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/Fee")]
public class FeeController : ControllerBase
{
    private readonly ISender _sender;
    public FeeController(ISender sender) { _sender = sender; }

    private int CurrentUserId
        => int.TryParse(User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? User?.Identity?.Name, out var id) ? id : 0;

    private IActionResult Wrap<T>(OperationResult<T> r)
    {
        if (r is null) return StatusCode(500, new { Message = "Server Error" });
        return StatusCode(r.StatusCode, new { Data = r.Result, r.Message, r.StatusCode, r.IsSuccess, r.TotalCount });
    }

    /* ---------- FEE-01: Configuration ---------- */

    [Authorize,HttpPost("FeeUpsertType")]
    public async Task<IActionResult> UpsertType([FromBody] UpsertFeeTypeCommand cmd) { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [Authorize,HttpPost("FeeDeleteType")]
    public async Task<IActionResult> DeleteType([FromBody] DeleteFeeTypeCommand cmd) { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [Authorize,HttpPost("FeeGetTypes")]
    public async Task<IActionResult> GetTypes([FromBody] GetFeeTypesQuery q) => Wrap(await _sender.Send(q));

    [Authorize,HttpPost("FeeUpsertAmount")]
    public async Task<IActionResult> UpsertAmount([FromBody] UpsertFeeTypeAmountCommand cmd) { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [Authorize,HttpPost("FeeGetStructureForClass")]
    public async Task<IActionResult> GetStructure([FromBody] GetFeeStructureForClassQuery q) => Wrap(await _sender.Send(q));

    [Authorize,HttpPost("FeeConfigureCalendar")]
    public async Task<IActionResult> ConfigCalendar([FromBody] ConfigureFeeCalendarCommand cmd) { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    /* ---------- FEE-02: Invoices ---------- */

    [Authorize,HttpPost("FeeGenerateMonthlyInvoices")]
    public async Task<IActionResult> Generate([FromBody] GenerateMonthlyInvoicesCommand cmd) { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [Authorize,HttpPost("FeeCancelInvoice")]
    public async Task<IActionResult> CancelInvoice([FromBody] CancelInvoiceCommand cmd) { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    /* ---------- FEE-03 / FEE-08 / FEE-11: Payments ---------- */

    [Authorize,HttpPost("FeeRecordPayment")]
    public async Task<IActionResult> RecordPayment([FromBody] RecordPaymentCommand cmd) { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [Authorize,HttpPost("FeeClearCheque")]
    public async Task<IActionResult> ClearCheque([FromBody] ClearChequeCommand cmd) { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [Authorize,HttpPost("FeeApplyAdvance")]
    public async Task<IActionResult> ApplyAdvance([FromBody] ApplyAdvanceCommand cmd) { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [Authorize,HttpPost("FeeReversePayment")]
    public async Task<IActionResult> ReversePayment([FromBody] ReversePaymentCommand cmd) { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    /* ---------- FEE-04: Receipt PDF ---------- */

    [Authorize,HttpPost("FeeGetReceiptPdf")]
    public async Task<IActionResult> GetReceiptPdf([FromBody] GetFeeReceiptQuery q)
    {
        var res = await _sender.Send(q);
        if (!res.IsSuccess || res.Result?.Bytes == null)
            return StatusCode(res?.StatusCode ?? 500, new { Message = res?.Message ?? "Server Error" });
        return File(res.Result.Bytes, res.Result.ContentType, res.Result.FileName);
    }

    /* ---------- FEE-04 / FEE-13: Ledger ---------- */

    [Authorize,HttpPost("FeeGetStudentLedger")]
    public async Task<IActionResult> GetLedger([FromBody] GetStudentLedgerQuery q) => Wrap(await _sender.Send(q));

    /* ---------- FEE-05: Dashboard ---------- */

    [Authorize,HttpPost("FeeGetCollectionSummary")]
    public async Task<IActionResult> Summary([FromBody] GetCollectionSummaryQuery q) => Wrap(await _sender.Send(q));

    [Authorize,HttpPost("FeeGetCollectionByClass")]
    public async Task<IActionResult> ByClass([FromBody] GetCollectionByClassQuery q) => Wrap(await _sender.Send(q));

    [Authorize,HttpPost("FeeGetCollectionByDay")]
    public async Task<IActionResult> ByDay([FromBody] GetCollectionByDayQuery q) => Wrap(await _sender.Send(q));

    /* ---------- FEE-06 / FEE-07 / FEE-14: Reports ---------- */

    [Authorize,HttpPost("FeeGetPendingList")]
    public async Task<IActionResult> Pending([FromBody] GetPendingFeeListQuery q) => Wrap(await _sender.Send(q));

    /* ---------- Class Fee Board (bulk-collection view) ---------- */

    [Authorize,HttpPost("FeeGetClassBoard")]
    public async Task<IActionResult> ClassBoard([FromBody] GetFeeClassBoardQuery q) => Wrap(await _sender.Send(q));

    [Authorize,HttpPost("FeeGetMonthlyNonSubmitted")]
    public async Task<IActionResult> MonthlyNonSubmitted([FromBody] GetMonthlyNonSubmittedQuery q) => Wrap(await _sender.Send(q));

    [Authorize,HttpPost("FeeGetOutstandingAgeing")]
    public async Task<IActionResult> Ageing([FromBody] GetOutstandingAgeingQuery q) => Wrap(await _sender.Send(q));

    /* ---------- FEE-08 / FEE-09 ---------- */

    [Authorize,HttpPost("FeeGetAdvanceBalance")]
    public async Task<IActionResult> Advance([FromBody] GetAdvanceBalanceQuery q) => Wrap(await _sender.Send(q));

    [Authorize,HttpPost("FeeGrantConcession")]
    public async Task<IActionResult> GrantConcession([FromBody] GrantConcessionCommand cmd) { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [Authorize,HttpPost("FeeRevokeConcession")]
    public async Task<IActionResult> RevokeConcession([FromBody] RevokeConcessionCommand cmd) { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [Authorize,HttpPost("FeeGetConcessions")]
    public async Task<IActionResult> GetConcessions([FromBody] GetConcessionsQuery q) => Wrap(await _sender.Send(q));

    /* ---------- FEE-10: Reminders ---------- */

    [Authorize,HttpPost("FeeSendReminders")]
    public async Task<IActionResult> SendReminders([FromBody] SendFeeRemindersCommand cmd) { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    /* ---------- FEE-12: Arrears ---------- */

    [Authorize,HttpPost("FeeCarryForwardArrears")]
    public async Task<IActionResult> CarryForward([FromBody] CarryForwardArrearsCommand cmd) { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [Authorize,HttpPost("FeeWriteOffArrear")]
    public async Task<IActionResult> WriteOff([FromBody] WriteOffArrearCommand cmd) { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [Authorize,HttpPost("FeeGetArrears")]
    public async Task<IActionResult> GetArrears([FromBody] GetFeeArrearsQuery q) => Wrap(await _sender.Send(q));

    /* ---------- FEE-13: Parent self-service (anonymous) ---------- */

    [AllowAnonymous, HttpPost("FeeParentSearch")]
    public async Task<IActionResult> ParentSearch([FromBody] ParentFeeSearchQuery q)
    {
        q.ClientIp = HttpContext.Connection.RemoteIpAddress?.ToString();
        return Wrap(await _sender.Send(q));
    }
}
