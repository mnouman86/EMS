using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.PackageDetail.Queries.GetPackageDetailById
{
    public class GetPackageDetailByIdQuery:IRequest<OperationResult<GetPackageDetailByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
