using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using MapsterMapper;
using Mediator;
using System.Collections.Generic;

namespace CleanArc.Application.Features.Inventory.Queries.CatalogueQueries
{
    /* INV-01: Categories */
    public record GetInventoryCategoriesQuery() : IRequest<OperationResult<List<InventoryCategoryResult>>>;

    public class InventoryCategoryResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }

    internal class GetInventoryCategoriesQueryHandler : IRequestHandler<GetInventoryCategoriesQuery, OperationResult<List<InventoryCategoryResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetInventoryCategoriesQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<InventoryCategoryResult>>> Handle(GetInventoryCategoriesQuery r, CancellationToken ct)
        {
            var res = await _u.InventoryRepository.GetCategoriesAsync();
            if (res.Code != 200) return OperationResult<List<InventoryCategoryResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<InventoryCategoryResult>>.SuccessResult(_m.Map<List<InventoryCategoryResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }

    /* INV-01: Items */
    public record GetInventoryItemsQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<InventoryItemResult>>>;

    public class InventoryItemResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string UnitOfMeasure { get; set; }
        public string Description { get; set; }
        public decimal ReorderLevel { get; set; }
        public decimal CurrentStock { get; set; }
        public bool IsActive { get; set; }
    }

    internal class GetInventoryItemsQueryHandler : IRequestHandler<GetInventoryItemsQuery, OperationResult<List<InventoryItemResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetInventoryItemsQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<InventoryItemResult>>> Handle(GetInventoryItemsQuery r, CancellationToken ct)
        {
            var res = await _u.InventoryRepository.GetItemsAsync(r.searchRequest);
            if (res.Code != 200) return OperationResult<List<InventoryItemResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<InventoryItemResult>>.SuccessResult(_m.Map<List<InventoryItemResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }

    /* INV-05 detail: single item */
    public record GetInventoryItemByIdQuery(int Id) : IRequest<OperationResult<InventoryItemResult>>;

    internal class GetInventoryItemByIdQueryHandler : IRequestHandler<GetInventoryItemByIdQuery, OperationResult<InventoryItemResult>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetInventoryItemByIdQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<InventoryItemResult>> Handle(GetInventoryItemByIdQuery r, CancellationToken ct)
        {
            var res = await _u.InventoryRepository.GetItemByIdAsync(r.Id);
            if (res.Code != 200 || res.Data == null) return OperationResult<InventoryItemResult>.FailureResult(res.Message ?? "Not found", res.Code);
            return OperationResult<InventoryItemResult>.SuccessResult(_m.Map<InventoryItemResult>(res.Data));
        }
    }
}
