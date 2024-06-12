using CleanArc.Application.Features.AgeType.Queries.GetAllAgeType;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.MappingHotelAmenity.Queries.GetAllMappingHotelAmenity;

public record GetAllMappingHotelAmenityQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllMappingHotelAmenityQueryResult>>>;


