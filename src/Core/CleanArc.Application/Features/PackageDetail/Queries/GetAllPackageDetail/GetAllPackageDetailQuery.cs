using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.PackageDetail.Queries.GetAllPackageDetail;

public record GetAllPackageDetailQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllPackageDetailQueryResult>>>;

