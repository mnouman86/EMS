
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.SearchHotelImage.Queries.GetAllSearchHotelImage;

public record GetAllSearchHotelImageQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllSearchHotelImageQueryResult>>>;

