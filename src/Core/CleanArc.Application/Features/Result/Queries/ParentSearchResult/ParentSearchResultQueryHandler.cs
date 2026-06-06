using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Result;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace CleanArc.Application.Features.Result.Queries.ParentSearchResult;

internal class ParentSearchResultQueryHandler : IRequestHandler<ParentSearchResultQuery, OperationResult<ParentSearchResultQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ParentSearchResultQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork; _mapper = mapper; _httpContextAccessor = httpContextAccessor;
    }

    public async ValueTask<OperationResult<ParentSearchResultQueryResult>> Handle(ParentSearchResultQuery request, CancellationToken cancellationToken)
    {
        var clientIp = request.ClientIp ?? _httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "unknown";

        var response = await _unitOfWork.ResultRepository.ParentSearchAsync(new ParentSearchDTO
        {
            StudentCode = request.StudentCode,
            SecondFactor = request.SecondFactor,
            ClientIp = clientIp
        });

        // RES-07: generic "no published result found" to prevent data leakage on any failure
        if (response.Code != 200 || response.Data == null)
            return OperationResult<ParentSearchResultQueryResult>.FailureResult("No published result found", 404);

        var mapped = _mapper.Map<ParentSearchResultQueryResult>(response.Data);
        return OperationResult<ParentSearchResultQueryResult>.SuccessResult(mapped, 200, "Success");
    }
}
