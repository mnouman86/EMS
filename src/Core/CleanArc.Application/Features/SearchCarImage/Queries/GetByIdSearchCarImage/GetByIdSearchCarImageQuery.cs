using CleanArc.Application.Features.SearchHotel.Queries.GetSearchHotelById;
using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.SearchCarImage.Queries.GetByIdSearchCarImage;

public record GetByIdSearchCarImageQuery : IRequest<OperationResult<GetByIdSearchCarImageQueryResult>>
{
    public int Id { get; set; }

}
