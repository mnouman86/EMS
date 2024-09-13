using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.UserExperience.Queries.GetAllUserExperience;

public record GetAllUserExperienceQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllUserExperienceQueryResult>>>;

