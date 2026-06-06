using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Result;
using MapsterMapper;
using Mediator;

namespace CleanArc.Application.Features.Result.Queries.GetClassResultSheet;

internal class GetClassResultSheetQueryHandler : IRequestHandler<GetClassResultSheetQuery, OperationResult<List<ClassSheetRowResult>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetClassResultSheetQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork; _mapper = mapper;
    }

    public async ValueTask<OperationResult<List<ClassSheetRowResult>>> Handle(GetClassResultSheetQuery request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.ResultRepository.GetClassResultSheetAsync(new ClassSheetRequest
        {
            ResultSessionId = request.ResultSessionId,
            SchoolClassId = request.SchoolClassId
        });
        if (response.Code != 200)
            return OperationResult<List<ClassSheetRowResult>>.FailureResult(response.Message, response.Code);
        var mapped = _mapper.Map<List<ClassSheetRowResult>>(response.Data);
        return OperationResult<List<ClassSheetRowResult>>.SuccessResult(mapped, response.Code, response.Message, response.TotalCount);
    }
}
