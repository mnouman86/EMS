using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArc.Application.Features.Subject.Queries.GetSubjectById;

internal class GetSubjectByIdQueryHandler : IRequestHandler<GetSubjectByIdQuery, OperationResult<GetSubjectByIdQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetSubjectByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetSubjectByIdQueryHandler(
        IUnitOfWork unitOfWork,
        ILogger<GetSubjectByIdQueryHandler> logger,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async ValueTask<OperationResult<GetSubjectByIdQueryResult>> Handle(GetSubjectByIdQuery request, CancellationToken cancellationToken)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);

        var response = await _unitOfWork.SubjectRepository.GetByIdAsync(request.searchRequestById);

        if (response.Code != 200)
        {
            return OperationResult<GetSubjectByIdQueryResult>.FailureResult(response.Message, response.Code);
        }

        var mapped = _mapper.Map<GetSubjectByIdQueryResult>(response.Data);
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mapped);

        return OperationResult<GetSubjectByIdQueryResult>.SuccessResult(mapped, response.Code, response.Message);
    }
}
