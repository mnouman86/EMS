using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.PackageType.Queries.GetPackageTypeById
{
    public class GetPackageTypeByIdQuery:IRequest<OperationResult<GetPackageTypeByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
