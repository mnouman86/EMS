using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using MapsterMapper;
using Mediator;
using System;

namespace CleanArc.Application.Features.AcademicYear.Queries.GetAcademicYears
{
    public record GetAcademicYearsQuery(SearchRequest searchRequest)
        : IRequest<OperationResult<List<GetAcademicYearsQueryResult>>>;

    public class GetAcademicYearsQueryResult
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsOpen { get; set; }
    }

    internal class GetAcademicYearsQueryHandler : IRequestHandler<GetAcademicYearsQuery, OperationResult<List<GetAcademicYearsQueryResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetAcademicYearsQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<GetAcademicYearsQueryResult>>> Handle(GetAcademicYearsQuery r, CancellationToken ct)
        {
            var res = await _u.AcademicYearRepository.GetAllAsync(r.searchRequest);
            if (res.Code != 200) return OperationResult<List<GetAcademicYearsQueryResult>>.FailureResult(res.Message, res.Code);
            var mapped = _m.Map<List<GetAcademicYearsQueryResult>>(res.Data);
            return OperationResult<List<GetAcademicYearsQueryResult>>.SuccessResult(mapped, res.Code, res.Message, res.TotalCount);
        }
    }
}
