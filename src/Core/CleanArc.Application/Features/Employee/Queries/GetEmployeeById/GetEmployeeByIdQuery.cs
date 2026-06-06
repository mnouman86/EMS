using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;

namespace CleanArc.Application.Features.Employee.Queries.GetEmployeeById;

public record GetEmployeeByIdQuery(SearchRequestById searchRequestById) : IRequest<OperationResult<GetEmployeeByIdQueryResult>>;
