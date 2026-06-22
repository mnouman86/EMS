using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using MapsterMapper;
using Mediator;
using System;
using System.Collections.Generic;

namespace CleanArc.Application.Features.Inventory.Queries.ReportQueries
{
    /* INV-08: Purchase register */
    public record GetPurchaseRegisterQuery(
        DateTime FromDate, DateTime ToDate,
        int? CategoryId = null, string? VendorName = null)
        : IRequest<OperationResult<List<PurchaseRegisterRow>>>;

    public class PurchaseRegisterRow
    {
        public int Id { get; set; }
        public string PurchaseCode { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string VendorName { get; set; }
        public string VendorInvoiceNo { get; set; }
        public string PaymentMode { get; set; }
        public decimal GrandTotal { get; set; }
        public int? LinkedExpenseId { get; set; }
    }

    internal class GetPurchaseRegisterQueryHandler : IRequestHandler<GetPurchaseRegisterQuery, OperationResult<List<PurchaseRegisterRow>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetPurchaseRegisterQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<PurchaseRegisterRow>>> Handle(GetPurchaseRegisterQuery r, CancellationToken ct)
        {
            var res = await _u.InventoryRepository.GetPurchaseRegisterAsync(r.FromDate, r.ToDate, r.CategoryId, r.VendorName);
            if (res.Code != 200) return OperationResult<List<PurchaseRegisterRow>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<PurchaseRegisterRow>>.SuccessResult(_m.Map<List<PurchaseRegisterRow>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }

    /* INV-08: Issue register */
    public record GetIssueRegisterQuery(
        DateTime FromDate, DateTime ToDate,
        int? CategoryId = null, int? ItemId = null,
        string? IssuedToType = null, int? IssuedToId = null, int? IssuedByUserId = null)
        : IRequest<OperationResult<List<IssueRegisterRow>>>;

    public class IssueRegisterRow
    {
        public int Id { get; set; }
        public string IssueCode { get; set; }
        public DateTime IssueDate { get; set; }
        public string IssuedToType { get; set; }
        public int? IssuedToId { get; set; }
        public string IssuedToName { get; set; }
        public string Purpose { get; set; }
        public int? IssuedBy { get; set; }
    }

    internal class GetIssueRegisterQueryHandler : IRequestHandler<GetIssueRegisterQuery, OperationResult<List<IssueRegisterRow>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetIssueRegisterQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<IssueRegisterRow>>> Handle(GetIssueRegisterQuery r, CancellationToken ct)
        {
            var res = await _u.InventoryRepository.GetIssueRegisterAsync(
                r.FromDate, r.ToDate, r.CategoryId, r.ItemId, r.IssuedToType, r.IssuedToId, r.IssuedByUserId);
            if (res.Code != 200) return OperationResult<List<IssueRegisterRow>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<IssueRegisterRow>>.SuccessResult(_m.Map<List<IssueRegisterRow>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }

    /* INV-08: Item ledger (movement history) */
    public record GetItemLedgerQuery(int ItemId, DateTime FromDate, DateTime ToDate)
        : IRequest<OperationResult<List<ItemLedgerResult>>>;

    public class ItemLedgerResult
    {
        public DateTime When { get; set; }
        public string MovementType { get; set; }
        public string Reference { get; set; }
        public decimal Quantity { get; set; }
        public decimal RunningStock { get; set; }
        public string Notes { get; set; }
    }

    internal class GetItemLedgerQueryHandler : IRequestHandler<GetItemLedgerQuery, OperationResult<List<ItemLedgerResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetItemLedgerQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<ItemLedgerResult>>> Handle(GetItemLedgerQuery r, CancellationToken ct)
        {
            var res = await _u.InventoryRepository.GetItemLedgerAsync(r.ItemId, r.FromDate, r.ToDate);
            if (res.Code != 200) return OperationResult<List<ItemLedgerResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<ItemLedgerResult>>.SuccessResult(_m.Map<List<ItemLedgerResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }

    /* INV-04: Issue detail (lines + returnable balance) for the return screen */
    public record GetIssueDetailQuery(int IssueId) : IRequest<OperationResult<List<IssueDetailRowResult>>>;

    public class IssueDetailRowResult
    {
        public int IssueId { get; set; }
        public string IssueCode { get; set; }
        public DateTime IssueDate { get; set; }
        public string IssuedToType { get; set; }
        public string IssuedToName { get; set; }
        public string Purpose { get; set; }
        public int IssueLineId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string UnitOfMeasure { get; set; }
        public decimal Quantity { get; set; }
        public decimal ReturnedQuantity { get; set; }
        public decimal RemainingQuantity { get; set; }
    }

    internal class GetIssueDetailQueryHandler : IRequestHandler<GetIssueDetailQuery, OperationResult<List<IssueDetailRowResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetIssueDetailQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<IssueDetailRowResult>>> Handle(GetIssueDetailQuery r, CancellationToken ct)
        {
            var res = await _u.InventoryRepository.GetIssueDetailAsync(r.IssueId);
            if (res.Code != 200) return OperationResult<List<IssueDetailRowResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<IssueDetailRowResult>>.SuccessResult(_m.Map<List<IssueDetailRowResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }
}
