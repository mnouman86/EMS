using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArc.Application.Features.Employee.Queries.GetTeacherAssignments;

internal class GetTeacherAssignmentsQueryHandler : IRequestHandler<GetTeacherAssignmentsQuery, OperationResult<List<GetTeacherAssignmentsQueryResult>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetTeacherAssignmentsQueryHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetTeacherAssignmentsQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        ILogger<GetTeacherAssignmentsQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async ValueTask<OperationResult<List<GetTeacherAssignmentsQueryResult>>> Handle(GetTeacherAssignmentsQuery request, CancellationToken cancellationToken)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);

        var response = await _unitOfWork.EmployeeRepository.GetTeacherAssignmentsAsync(request.searchRequestById);

        if (response.Code != 200)
            return OperationResult<List<GetTeacherAssignmentsQueryResult>>.FailureResult(response.Message, response.Code);

        var mapped = _mapper.Map<List<GetTeacherAssignmentsQueryResult>>(response.Data);
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mapped);

        return OperationResult<List<GetTeacherAssignmentsQueryResult>>.SuccessResult(
            mapped, response.Code, response.Message, response.TotalCount);
    }
}
