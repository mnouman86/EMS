using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using MapsterMapper;
using Mediator;
using System;
using System.Collections.Generic;

namespace CleanArc.Application.Features.Fee.Queries.ClassBoardQueries
{
    /* Class Fee Board — per-student fee state for a single class.
       Optional period (year + month) narrows the figures to that month's invoices;
       leaving both NULL returns lifetime totals. */
    public record GetFeeClassBoardQuery(
        int ClassId, int? AcademicYearId = null, int? PeriodYear = null, int? PeriodMonth = null)
        : IRequest<OperationResult<List<FeeClassBoardRowResult>>>;

    public class FeeClassBoardRowResult
    {
        public int StudentId { get; set; }
        public string StudentCode { get; set; }
        public string FormNo { get; set; }
        public string FullName { get; set; }
        public string ParentName { get; set; }
        public string ParentMobile { get; set; }
        public decimal TotalInvoiced { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal Outstanding { get; set; }
        public DateTime? LastPaymentDate { get; set; }
        public decimal? LastPaymentAmount { get; set; }
        public string MonthsDue { get; set; }
        public int InvoiceCount { get; set; }
    }

    internal class GetFeeClassBoardQueryHandler : IRequestHandler<GetFeeClassBoardQuery, OperationResult<List<FeeClassBoardRowResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetFeeClassBoardQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<FeeClassBoardRowResult>>> Handle(GetFeeClassBoardQuery r, CancellationToken ct)
        {
            var res = await _u.FeeRepository.GetClassFeeBoardAsync(r.ClassId, r.AcademicYearId, r.PeriodYear, r.PeriodMonth);
            if (res.Code != 200) return OperationResult<List<FeeClassBoardRowResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<FeeClassBoardRowResult>>.SuccessResult(
                _m.Map<List<FeeClassBoardRowResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }
}
