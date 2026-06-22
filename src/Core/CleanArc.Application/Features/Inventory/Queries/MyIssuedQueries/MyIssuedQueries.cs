using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using MapsterMapper;
using Mediator;
using System;
using System.Collections.Generic;

namespace CleanArc.Application.Features.Inventory.Queries.MyIssuedQueries
{
    public record GetMyIssuedInventoryQuery(DateTime FromDate, DateTime ToDate)
        : IRequest<OperationResult<List<MyIssuedInventoryRowResult>>>
    {
        public int CallerUserId { get; set; }
    }

    public class MyIssuedInventoryRowResult
    {
        public int IssueId { get; set; }
        public string IssueCode { get; set; }
        public DateTime IssueDate { get; set; }
        public string Purpose { get; set; }
        public int IssueLineId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string Code { get; set; }
        public string CategoryName { get; set; }
        public string UnitOfMeasure { get; set; }
        public decimal Quantity { get; set; }
        public decimal ReturnedQuantity { get; set; }
        public decimal RemainingQuantity { get; set; }
    }

    internal class GetMyIssuedInventoryQueryHandler : IRequestHandler<GetMyIssuedInventoryQuery, OperationResult<List<MyIssuedInventoryRowResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetMyIssuedInventoryQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<MyIssuedInventoryRowResult>>> Handle(GetMyIssuedInventoryQuery r, CancellationToken ct)
        {
            var res = await _u.InventoryRepository.GetMyIssuedAsync(r.CallerUserId, r.FromDate, r.ToDate);
            if (res.Code != 200) return OperationResult<List<MyIssuedInventoryRowResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<MyIssuedInventoryRowResult>>.SuccessResult(_m.Map<List<MyIssuedInventoryRowResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }
}
