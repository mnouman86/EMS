using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.ActivityAddressMapping.Queries.GetActivityAddressMappingById
{
    public class GetActivityAddressMappingByIdQuery:IRequest<OperationResult<GetActivityAddressMappingByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
