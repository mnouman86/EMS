using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using MapsterMapper;
using Mediator;
using System;
using System.Collections.Generic;

namespace CleanArc.Application.Features.Fee.Queries.FeeTypeQueries
{
    /* FEE-01: List fee types */
    public record GetFeeTypesQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<FeeTypeResult>>>;

    public class FeeTypeResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Category { get; set; }
        public bool IsActive { get; set; }
    }

    internal class GetFeeTypesQueryHandler : IRequestHandler<GetFeeTypesQuery, OperationResult<List<FeeTypeResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetFeeTypesQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<FeeTypeResult>>> Handle(GetFeeTypesQuery r, CancellationToken ct)
        {
            var res = await _u.FeeRepository.GetFeeTypesAsync(r.searchRequest);
            if (res.Code != 200) return OperationResult<List<FeeTypeResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<FeeTypeResult>>.SuccessResult(_m.Map<List<FeeTypeResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }

    /* FEE-01: Effective fee structure for a class (used by FEE-02 preview) */
    public record GetFeeStructureForClassQuery(int SchoolClassId, int AcademicYearId)
        : IRequest<OperationResult<List<FeeStructureRowResult>>>;

    public class FeeStructureRowResult
    {
        public int FeeTypeId { get; set; }
        public string FeeTypeName { get; set; }
        public string FeeTypeCode { get; set; }
        public string Category { get; set; }
        public int SchoolClassId { get; set; }
        public string ClassName { get; set; }
        public decimal Amount { get; set; }
        public DateTime EffectiveFrom { get; set; }
    }

    internal class GetFeeStructureForClassQueryHandler : IRequestHandler<GetFeeStructureForClassQuery, OperationResult<List<FeeStructureRowResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetFeeStructureForClassQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<FeeStructureRowResult>>> Handle(GetFeeStructureForClassQuery r, CancellationToken ct)
        {
            var res = await _u.FeeRepository.GetFeeStructureForClassAsync(r.SchoolClassId, r.AcademicYearId);
            if (res.Code != 200) return OperationResult<List<FeeStructureRowResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<FeeStructureRowResult>>.SuccessResult(_m.Map<List<FeeStructureRowResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }
}
