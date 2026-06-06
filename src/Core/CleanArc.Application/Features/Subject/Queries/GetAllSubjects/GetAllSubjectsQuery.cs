using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;

namespace CleanArc.Application.Features.Subject.Queries.GetAllSubjects;

public record GetAllSubjectsQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllSubjectsQueryResult>>>;
