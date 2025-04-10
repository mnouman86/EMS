
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.SearchHotelImage.Queries.GetByIdSearchHotelImage;

public record GetByIdSearchHotelImageQuery(SearchRequestById searchRequestById) : IRequest<OperationResult<GetByIdSearchHotelImageQueryResult>>;
