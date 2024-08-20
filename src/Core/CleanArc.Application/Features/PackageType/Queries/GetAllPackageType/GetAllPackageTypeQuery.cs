using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.PackageType.Queries.GetAllPackageType;

public record GetAllPackageTypeQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllPackageTypeQueryResult>>>;

