using CleanArc.Application.Features.AmenityMapping.Queries.GetAmenityMappingByID;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.MappingRoomAmenity.Queries.GetMappingRoomAmenityById;

public record GetMappingRoomAmenityByIdQuery(SearchRequestById searchRequestById):IRequest<OperationResult<GetMappingRoomAmenityByIdQueryResult>>;
