using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;

namespace CleanArc.Application.Features.Employee.Queries.GetAllEmployees;

public record GetAllEmployeesQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllEmployeesQueryResult>>>;
