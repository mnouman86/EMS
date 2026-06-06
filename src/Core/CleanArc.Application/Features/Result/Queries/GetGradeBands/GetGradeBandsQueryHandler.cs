using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using MapsterMapper;
using Mediator;

namespace CleanArc.Application.Features.Result.Queries.GetGradeBands;

internal class GetGradeBandsQueryHandler : IRequestHandler<GetGradeBandsQuery, OperationResult<List<GetGradeBandsQueryResult>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetGradeBandsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork; _mapper = mapper;
    }

    public async ValueTask<OperationResult<List<GetGradeBandsQueryResult>>> Handle(GetGradeBandsQuery request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.ResultRepository.GetGradeBandsAsync();
        if (response.Code != 200)
            return OperationResult<List<GetGradeBandsQueryResult>>.FailureResult(response.Message, response.Code);
        var mapped = _mapper.Map<List<GetGradeBandsQueryResult>>(response.Data);
        return OperationResult<List<GetGradeBandsQueryResult>>.SuccessResult(mapped, response.Code, response.Message, response.TotalCount);
    }
}
