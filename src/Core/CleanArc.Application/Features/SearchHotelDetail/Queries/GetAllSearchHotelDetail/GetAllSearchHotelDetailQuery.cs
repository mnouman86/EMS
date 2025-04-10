using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.SearchHotelDetail.Queries.GetAllSearchHotelDetail;

public record GetAllSearchHotelDetailQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllSearchHotelDetailQueryResult>>>;

