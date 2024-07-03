using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.AdvertisementPage.Queries.GetAdvertisementPageById
{
    public class GetAdvertisementPageByIdQuery:IRequest<OperationResult<GetAdvertisementPageByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
