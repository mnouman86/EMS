using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;

namespace CleanArc.Application.Features.SchoolClass.Queries.GetSchoolClassById;

public record GetSchoolClassByIdQuery(SearchRequestById searchRequestById) : IRequest<OperationResult<GetSchoolClassByIdQueryResult>>;
