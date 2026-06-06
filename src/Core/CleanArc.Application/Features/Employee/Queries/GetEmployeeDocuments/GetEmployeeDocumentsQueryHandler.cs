using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArc.Application.Features.Employee.Queries.GetEmployeeDocuments;

internal class GetEmployeeDocumentsQueryHandler : IRequestHandler<GetEmployeeDocumentsQuery, OperationResult<List<GetEmployeeDocumentsQueryResult>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetEmployeeDocumentsQueryHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetEmployeeDocumentsQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        ILogger<GetEmployeeDocumentsQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async ValueTask<OperationResult<List<GetEmployeeDocumentsQueryResult>>> Handle(GetEmployeeDocumentsQuery request, CancellationToken cancellationToken)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);

        var response = await _unitOfWork.EmployeeRepository.GetDocumentsAsync(request.searchRequestById);

        if (response.Code != 200)
            return OperationResult<List<GetEmployeeDocumentsQueryResult>>.FailureResult(response.Message, response.Code);

        var mapped = _mapper.Map<List<GetEmployeeDocumentsQueryResult>>(response.Data);
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mapped);

        return OperationResult<List<GetEmployeeDocumentsQueryResult>>.SuccessResult(
            mapped, response.Code, response.Message, response.TotalCount);
    }
}
