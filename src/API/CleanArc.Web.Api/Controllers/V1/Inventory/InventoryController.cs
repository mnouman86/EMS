using Asp.Versioning;
using CleanArc.Application.Features.Inventory.Command.CatalogueCommands;
using CleanArc.Application.Features.Inventory.Command.IssueRequestCommands;
using CleanArc.Application.Features.Inventory.Command.MovementCommands;
using CleanArc.Application.Features.Inventory.Queries.CatalogueQueries;
using CleanArc.Application.Features.Inventory.Queries.IssueRequestQueries;
using CleanArc.Application.Features.Inventory.Queries.MyIssuedQueries;
using CleanArc.Application.Features.Inventory.Queries.ReportQueries;
using CleanArc.Application.Features.Inventory.Queries.StockQueries;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CleanArc.Web.Api.Controllers.V1.Inventory;

/// <summary>
/// Inventory Management — INV-01..INV-08.
/// Teachers can initiate issue requests (via existing Issue endpoint with their own staff id);
/// Admin/Accountant own purchases, adjustments and approvals.
/// </summary>
[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/Inventory")]
public class InventoryController : ControllerBase
{
    private readonly ISender _sender;
    public InventoryController(ISender sender) { _sender = sender; }

    private int CurrentUserId
        => int.TryParse(User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? User?.Identity?.Name, out var id) ? id : 0;

    private IActionResult Wrap<T>(OperationResult<T> r)
    {
        if (r is null) return StatusCode(500, new { Message = "Server Error" });
        return StatusCode(r.StatusCode, new { Data = r.Result, r.Message, r.StatusCode, r.IsSuccess, r.TotalCount });
    }

    /* ---------- INV-01: Catalogue ---------- */

    [Authorize,HttpPost("InventoryUpsertCategory")]
    public async Task<IActionResult> UpsertCategory([FromBody] UpsertInventoryCategoryCommand cmd) { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [HttpPost("InventoryGetCategories")]
    public async Task<IActionResult> GetCategories() => Wrap(await _sender.Send(new GetInventoryCategoriesQuery()));

    [Authorize,HttpPost("InventoryUpsertItem")]
    public async Task<IActionResult> UpsertItem([FromBody] UpsertInventoryItemCommand cmd) { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [Authorize,HttpPost("InventoryDeleteItem")]
    public async Task<IActionResult> DeleteItem([FromBody] DeleteInventoryItemCommand cmd) { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [HttpPost("InventoryGetItems")]
    public async Task<IActionResult> GetItems([FromBody] GetInventoryItemsQuery q) => Wrap(await _sender.Send(q));

    [HttpPost("InventoryGetItemById")]
    public async Task<IActionResult> GetItemById([FromBody] GetInventoryItemByIdQuery q) => Wrap(await _sender.Send(q));

    /* ---------- INV-02 / INV-03 / INV-04 / INV-07: Movements ---------- */

    [Authorize,HttpPost("InventoryRecordPurchase")]
    public async Task<IActionResult> RecordPurchase([FromBody] RecordPurchaseCommand cmd) { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [Authorize, HttpPost("InventoryIssueItems")]
    public async Task<IActionResult> IssueItems([FromBody] IssueInventoryItemsCommand cmd) { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [Authorize,HttpPost("InventoryRecordReturn")]
    public async Task<IActionResult> RecordReturn([FromBody] RecordInventoryReturnCommand cmd) { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [Authorize,HttpPost("InventoryRecordAdjustment")]
    public async Task<IActionResult> RecordAdjustment([FromBody] RecordStockAdjustmentCommand cmd) { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    /* ---------- INV-05 / INV-06: Dashboard + Alerts ---------- */

    [HttpPost("InventoryGetStockDashboard")]
    public async Task<IActionResult> StockDashboard([FromBody] GetStockDashboardQuery q) => Wrap(await _sender.Send(q));

    [Authorize,HttpPost("InventoryGetLowStockAlerts")]
    public async Task<IActionResult> LowStockAlerts() => Wrap(await _sender.Send(new GetLowStockAlertsQuery()));

    [Authorize,HttpPost("InventorySnoozeAlert")]
    public async Task<IActionResult> SnoozeAlert([FromBody] SnoozeInventoryAlertCommand cmd) { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    /* ---------- INV-08: Reports ---------- */

    [Authorize,HttpPost("InventoryGetPurchaseRegister")]
    public async Task<IActionResult> PurchaseRegister([FromBody] GetPurchaseRegisterQuery q) => Wrap(await _sender.Send(q));

    [Authorize,HttpPost("InventoryGetIssueRegister")]
    public async Task<IActionResult> IssueRegister([FromBody] GetIssueRegisterQuery q) => Wrap(await _sender.Send(q));

    [Authorize,HttpPost("InventoryGetItemLedger")]
    public async Task<IActionResult> ItemLedger([FromBody] GetItemLedgerQuery q) => Wrap(await _sender.Send(q));

    [Authorize,HttpPost("InventoryGetIssueDetail")]
    public async Task<IActionResult> IssueDetail([FromBody] GetIssueDetailQuery q) => Wrap(await _sender.Send(q));

    /* ---------- "My Issued Items" (any authenticated user) ---------- */

    [Authorize, HttpPost("InventoryGetMyIssued")]
    public async Task<IActionResult> MyIssued([FromBody] GetMyIssuedInventoryQuery q)
    {
        q.CallerUserId = CurrentUserId;
        return Wrap(await _sender.Send(q));
    }

    /* ---------- Issue request workflow ----------
       Create: any authenticated user.
       List:   any authenticated user (MineOnly is enforced by the caller — UI flips it on for teachers).
       Approve / Reject / Fulfill: admin / principal / accountant. */

    [Authorize, HttpPost("InventoryCreateIssueRequest")]
    public async Task<IActionResult> CreateIssueRequest([FromBody] CreateInventoryIssueRequestCommand cmd)
    { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [Authorize, HttpPost("InventoryGetIssueRequests")]
    public async Task<IActionResult> GetIssueRequests([FromBody] GetInventoryIssueRequestsQuery q)
    {
        q.CallerUserId = CurrentUserId;
        // Non-finance callers (teachers etc.) can only see their own requests, regardless
        // of what they pass in MineOnly. Admin / Principal / Accountant see everything.
        var isFinance = User.IsInRole(Roles.Admin) || User.IsInRole(Roles.Principal) || User.IsInRole(Roles.Accountant);
        if (!isFinance) q = q with { MineOnly = true };
        return Wrap(await _sender.Send(q));
    }

    [Authorize, HttpPost("InventoryGetIssueRequestLines")]
    public async Task<IActionResult> GetIssueRequestLines([FromBody] GetInventoryIssueRequestLinesQuery q)
        => Wrap(await _sender.Send(q));

    [Authorize(Roles = Roles.FinanceRoles), HttpPost("InventoryApproveIssueRequest")]
    public async Task<IActionResult> ApproveIssueRequest([FromBody] ApproveInventoryIssueRequestCommand cmd)
    { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [Authorize(Roles = Roles.FinanceRoles), HttpPost("InventoryRejectIssueRequest")]
    public async Task<IActionResult> RejectIssueRequest([FromBody] RejectInventoryIssueRequestCommand cmd)
    { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }

    [Authorize(Roles = Roles.FinanceRoles), HttpPost("InventoryFulfillIssueRequest")]
    public async Task<IActionResult> FulfillIssueRequest([FromBody] FulfillInventoryIssueRequestCommand cmd)
    { cmd.UserId = CurrentUserId; return Wrap(await _sender.Send(cmd)); }
}
