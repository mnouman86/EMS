using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Result;
using MapsterMapper;
using Mediator;

namespace CleanArc.Application.Features.Result.Queries.GetPreLockReport;

internal class GetPreLockReportQueryHandler : IRequestHandler<GetPreLockReportQuery, OperationResult<List<PreLockMissingRow>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPreLockReportQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork; _mapper = mapper;
    }

    public async ValueTask<OperationResult<List<PreLockMissingRow>>> Handle(GetPreLockReportQuery request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.ResultRepository.GetPreLockReportAsync(new LockResultsDTO
        {
            ResultSessionId = request.ResultSessionId,
            SchoolClassId = request.SchoolClassId
        });
        if (response.Code != 200)
            return OperationResult<List<PreLockMissingRow>>.FailureResult(response.Message, response.Code);
        var mapped = _mapper.Map<List<PreLockMissingRow>>(response.Data);
        return OperationResult<List<PreLockMissingRow>>.SuccessResult(mapped, response.Code, response.Message, response.TotalCount);
    }
}
