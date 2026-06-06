using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using MapsterMapper;
using Mediator;
using System.Collections.Generic;

namespace CleanArc.Application.Features.Expense.Queries.CategoryQueries
{
    /* EXP-01 list */
    public record GetExpenseCategoriesQuery(bool IncludeInactive) : IRequest<OperationResult<List<ExpenseCategoryResult>>>;

    public class ExpenseCategoryResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int? ParentCategoryId { get; set; }
        public decimal? MonthlyBudget { get; set; }
        public bool IsActive { get; set; }
    }

    internal class GetExpenseCategoriesQueryHandler : IRequestHandler<GetExpenseCategoriesQuery, OperationResult<List<ExpenseCategoryResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetExpenseCategoriesQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<ExpenseCategoryResult>>> Handle(GetExpenseCategoriesQuery r, CancellationToken ct)
        {
            var res = await _u.ExpenseRepository.GetCategoriesAsync(r.IncludeInactive);
            if (res.Code != 200) return OperationResult<List<ExpenseCategoryResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<ExpenseCategoryResult>>.SuccessResult(_m.Map<List<ExpenseCategoryResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }

    /* EXP-02 list of recurring templates */
    public record GetRecurringTemplatesQuery() : IRequest<OperationResult<List<RecurringTemplateResult>>>;

    public class RecurringTemplateResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public decimal Amount { get; set; }
        public string DefaultPaymentMode { get; set; }
        public string PaidTo { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }

    internal class GetRecurringTemplatesQueryHandler : IRequestHandler<GetRecurringTemplatesQuery, OperationResult<List<RecurringTemplateResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetRecurringTemplatesQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<RecurringTemplateResult>>> Handle(GetRecurringTemplatesQuery r, CancellationToken ct)
        {
            var res = await _u.ExpenseRepository.GetRecurringTemplatesAsync();
            if (res.Code != 200) return OperationResult<List<RecurringTemplateResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<RecurringTemplateResult>>.SuccessResult(_m.Map<List<RecurringTemplateResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }
}
