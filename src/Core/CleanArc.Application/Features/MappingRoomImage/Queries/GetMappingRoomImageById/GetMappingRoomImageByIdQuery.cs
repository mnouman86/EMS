using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.MappingRoomImage.Queries.GetMappingRoomImageById
{
    public class GetMappingRoomImageByIdQuery : IRequest<OperationResult<GetMappingRoomImageByIdQueryResult>>
    {
        public int Id { get; set; }

    }
}
