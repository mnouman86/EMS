using CleanArc.Application.Features.SearchHotelRoomDetail.Queries.GetAllSearchHotelRoomDetail;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.SearchRoomAmenities.Queries.GetAllSearchRoomAmenities;

public record GetAllSearchRoomAmenitiesQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllSearchRoomAmenitiesQueryResult>>>;

