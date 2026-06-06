using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;

namespace CleanArc.Application.Features.Subject.Queries.GetSubjectById;

public record GetSubjectByIdQuery(SearchRequestById searchRequestById) : IRequest<OperationResult<GetSubjectByIdQueryResult>>;
