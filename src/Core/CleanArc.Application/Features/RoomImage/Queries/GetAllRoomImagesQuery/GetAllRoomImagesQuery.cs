using CleanArc.Application.Features.SearchHotelImage.Queries.GetAllSearchHotelImage;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.RoomImage.Queries.GetAllRoomImagesQuery;

public record GetAllRoomImagesQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllRoomImagesQueryResult>>>;

