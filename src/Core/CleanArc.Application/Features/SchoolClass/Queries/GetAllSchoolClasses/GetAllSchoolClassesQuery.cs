using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;

namespace CleanArc.Application.Features.SchoolClass.Queries.GetAllSchoolClasses;

public record GetAllSchoolClassesQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllSchoolClassesQueryResult>>>;
