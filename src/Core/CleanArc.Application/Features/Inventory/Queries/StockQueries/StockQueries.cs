using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using MapsterMapper;
using Mediator;
using System;
using System.Collections.Generic;

namespace CleanArc.Application.Features.Inventory.Queries.StockQueries
{
    /* INV-05: Stock dashboard */
    public record GetStockDashboardQuery(int? CategoryId, string? Status, DateTime FromDate, DateTime ToDate)
        : IRequest<OperationResult<List<StockDashboardResult>>>;

    public class StockDashboardResult
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string Code { get; set; }
        public string CategoryName { get; set; }
        public string UnitOfMeasure { get; set; }
        public decimal OpeningStock { get; set; }
        public decimal Purchased { get; set; }
        public decimal Issued { get; set; }
        public decimal Adjusted { get; set; }
        public decimal Returned { get; set; }
        public decimal CurrentStock { get; set; }
        public decimal ReorderLevel { get; set; }
        public string Status { get; set; }
    }

    internal class GetStockDashboardQueryHandler : IRequestHandler<GetStockDashboardQuery, OperationResult<List<StockDashboardResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetStockDashboardQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<StockDashboardResult>>> Handle(GetStockDashboardQuery r, CancellationToken ct)
        {
            var res = await _u.InventoryRepository.GetStockDashboardAsync(r.CategoryId, r.Status, r.FromDate, r.ToDate);
            if (res.Code != 200) return OperationResult<List<StockDashboardResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<StockDashboardResult>>.SuccessResult(_m.Map<List<StockDashboardResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }

    /* INV-06: Low-stock alerts */
    public record GetLowStockAlertsQuery() : IRequest<OperationResult<List<LowStockAlertResult>>>;

    public class LowStockAlertResult
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string Code { get; set; }
        public string CategoryName { get; set; }
        public decimal CurrentStock { get; set; }
        public decimal ReorderLevel { get; set; }
        public DateTime? LastPurchasedAt { get; set; }
        public DateTime? SnoozedUntil { get; set; }
    }

    internal class GetLowStockAlertsQueryHandler : IRequestHandler<GetLowStockAlertsQuery, OperationResult<List<LowStockAlertResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetLowStockAlertsQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<LowStockAlertResult>>> Handle(GetLowStockAlertsQuery r, CancellationToken ct)
        {
            var res = await _u.InventoryRepository.GetLowStockAlertsAsync();
            if (res.Code != 200) return OperationResult<List<LowStockAlertResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<LowStockAlertResult>>.SuccessResult(_m.Map<List<LowStockAlertResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }
}
