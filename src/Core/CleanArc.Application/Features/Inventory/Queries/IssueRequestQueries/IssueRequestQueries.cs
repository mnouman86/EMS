using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using MapsterMapper;
using Mediator;
using System;
using System.Collections.Generic;

namespace CleanArc.Application.Features.Inventory.Queries.IssueRequestQueries
{
    public record GetInventoryIssueRequestsQuery(bool MineOnly, string? Status, DateTime? FromDate, DateTime? ToDate)
        : IRequest<OperationResult<List<InventoryIssueRequestRowResult>>>
    {
        public int CallerUserId { get; set; }
    }

    public class InventoryIssueRequestRowResult
    {
        public int Id { get; set; }
        public string RequestCode { get; set; }
        public DateTime RequestDate { get; set; }
        public int RequestedByUserId { get; set; }
        public string RequestedByName { get; set; }
        public string Purpose { get; set; }
        public string Status { get; set; }
        public int? ApprovedByUserId { get; set; }
        public string ApprovedByName { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public string RejectedReason { get; set; }
        public int? FulfilledIssueId { get; set; }
        public string FulfilledIssueCode { get; set; }
        public int LineCount { get; set; }
        public decimal TotalQuantity { get; set; }
    }

    internal class GetInventoryIssueRequestsQueryHandler : IRequestHandler<GetInventoryIssueRequestsQuery, OperationResult<List<InventoryIssueRequestRowResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetInventoryIssueRequestsQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<InventoryIssueRequestRowResult>>> Handle(GetInventoryIssueRequestsQuery r, CancellationToken ct)
        {
            var res = await _u.InventoryRepository.GetIssueRequestsAsync(r.CallerUserId, r.MineOnly, r.Status, r.FromDate, r.ToDate);
            if (res.Code != 200) return OperationResult<List<InventoryIssueRequestRowResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<InventoryIssueRequestRowResult>>.SuccessResult(_m.Map<List<InventoryIssueRequestRowResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }

    public record GetInventoryIssueRequestLinesQuery(int RequestId) : IRequest<OperationResult<List<InventoryIssueRequestLineRowResult>>>;

    public class InventoryIssueRequestLineRowResult
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string Code { get; set; }
        public string UnitOfMeasure { get; set; }
        public decimal Quantity { get; set; }
        public decimal CurrentStock { get; set; }
    }

    internal class GetInventoryIssueRequestLinesQueryHandler : IRequestHandler<GetInventoryIssueRequestLinesQuery, OperationResult<List<InventoryIssueRequestLineRowResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetInventoryIssueRequestLinesQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<InventoryIssueRequestLineRowResult>>> Handle(GetInventoryIssueRequestLinesQuery r, CancellationToken ct)
        {
            var res = await _u.InventoryRepository.GetIssueRequestLinesAsync(r.RequestId);
            if (res.Code != 200) return OperationResult<List<InventoryIssueRequestLineRowResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<InventoryIssueRequestLineRowResult>>.SuccessResult(_m.Map<List<InventoryIssueRequestLineRowResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }
}
