using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;

namespace CleanArc.Application.Features.Subject.Queries.GetSubjectsByClass;

public record GetSubjectsByClassQuery(SearchRequestById searchRequestById) : IRequest<OperationResult<List<GetSubjectsByClassQueryResult>>>;
