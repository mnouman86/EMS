using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Advertisement.Queries.GetAdvertisementById
{
    public class GetAdvertisementByIdQuery:IRequest<OperationResult<GetAdvertisementByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
