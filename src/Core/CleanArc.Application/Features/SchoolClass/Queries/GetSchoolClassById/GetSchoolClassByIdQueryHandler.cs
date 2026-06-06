using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArc.Application.Features.SchoolClass.Queries.GetSchoolClassById;

internal class GetSchoolClassByIdQueryHandler : IRequestHandler<GetSchoolClassByIdQuery, OperationResult<GetSchoolClassByIdQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetSchoolClassByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetSchoolClassByIdQueryHandler(
        IUnitOfWork unitOfWork,
        ILogger<GetSchoolClassByIdQueryHandler> logger,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async ValueTask<OperationResult<GetSchoolClassByIdQueryResult>> Handle(GetSchoolClassByIdQuery request, CancellationToken cancellationToken)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);

        var response = await _unitOfWork.SchoolClassRepository.GetByIdAsync(request.searchRequestById);

        if (response.Code != 200)
        {
            return OperationResult<GetSchoolClassByIdQueryResult>.FailureResult(response.Message, response.Code);
        }

        var mapped = _mapper.Map<GetSchoolClassByIdQueryResult>(response.Data);
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mapped);

        return OperationResult<GetSchoolClassByIdQueryResult>.SuccessResult(mapped, response.Code, response.Message);
    }
}
