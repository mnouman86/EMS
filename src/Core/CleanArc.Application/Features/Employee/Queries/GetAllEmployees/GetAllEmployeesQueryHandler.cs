using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArc.Application.Features.Employee.Queries.GetAllEmployees;

internal class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, OperationResult<List<GetAllEmployeesQueryResult>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllEmployeesQueryHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetAllEmployeesQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        ILogger<GetAllEmployeesQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async ValueTask<OperationResult<List<GetAllEmployeesQueryResult>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);

        var response = await _unitOfWork.EmployeeRepository.GetAllAsync(request.searchRequest);

        if (response.Code != 200)
            return OperationResult<List<GetAllEmployeesQueryResult>>.FailureResult(response.Message, response.Code);

        var mapped = _mapper.Map<List<GetAllEmployeesQueryResult>>(response.Data);
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mapped);

        return OperationResult<List<GetAllEmployeesQueryResult>>.SuccessResult(
            mapped, response.Code, response.Message, response.TotalCount);
    }
}
