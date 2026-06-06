using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArc.Application.Features.Employee.Queries.GetEmployeeById;

internal class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, OperationResult<GetEmployeeByIdQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetEmployeeByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetEmployeeByIdQueryHandler(
        IUnitOfWork unitOfWork,
        ILogger<GetEmployeeByIdQueryHandler> logger,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async ValueTask<OperationResult<GetEmployeeByIdQueryResult>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);

        var response = await _unitOfWork.EmployeeRepository.GetByIdAsync(request.searchRequestById);

        if (response.Code != 200)
            return OperationResult<GetEmployeeByIdQueryResult>.FailureResult(response.Message, response.Code);

        var mapped = _mapper.Map<GetEmployeeByIdQueryResult>(response.Data);
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mapped);

        return OperationResult<GetEmployeeByIdQueryResult>.SuccessResult(mapped, response.Code, response.Message);
    }
}
