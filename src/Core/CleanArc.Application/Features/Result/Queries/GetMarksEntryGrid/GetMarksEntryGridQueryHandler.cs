using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using MapsterMapper;
using Mediator;

namespace CleanArc.Application.Features.Result.Queries.GetMarksEntryGrid;

internal class GetMarksEntryGridQueryHandler : IRequestHandler<GetMarksEntryGridQuery, OperationResult<List<MarksEntryGridRow>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetMarksEntryGridQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork; _mapper = mapper;
    }

    public async ValueTask<OperationResult<List<MarksEntryGridRow>>> Handle(GetMarksEntryGridQuery request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.ResultRepository.GetMarksEntryGridAsync(request.request);
        if (response.Code != 200)
            return OperationResult<List<MarksEntryGridRow>>.FailureResult(response.Message, response.Code);
        var mapped = _mapper.Map<List<MarksEntryGridRow>>(response.Data);
        return OperationResult<List<MarksEntryGridRow>>.SuccessResult(mapped, response.Code, response.Message, response.TotalCount);
    }
}
