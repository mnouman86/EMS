using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using MapsterMapper;
using Mediator;
using System;

namespace CleanArc.Application.Features.AcademicYear.Queries.GetCurrentAcademicYear
{
    public record GetCurrentAcademicYearQuery() : IRequest<OperationResult<CurrentAcademicYearResult>>;

    public class CurrentAcademicYearResult
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsOpen { get; set; }
    }

    internal class GetCurrentAcademicYearQueryHandler : IRequestHandler<GetCurrentAcademicYearQuery, OperationResult<CurrentAcademicYearResult>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetCurrentAcademicYearQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<CurrentAcademicYearResult>> Handle(GetCurrentAcademicYearQuery r, CancellationToken ct)
        {
            var res = await _u.AcademicYearRepository.GetCurrentAsync();
            if (res.Code != 200 || res.Data == null)
                return OperationResult<CurrentAcademicYearResult>.FailureResult(res.Message ?? "No open academic year", 404);
            return OperationResult<CurrentAcademicYearResult>.SuccessResult(_m.Map<CurrentAcademicYearResult>(res.Data));
        }
    }
}
