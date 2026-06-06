using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;

namespace CleanArc.Application.Features.Employee.Queries.GetEmployeeDocuments;

public record GetEmployeeDocumentsQuery(SearchRequestById searchRequestById) : IRequest<OperationResult<List<GetEmployeeDocumentsQueryResult>>>;
