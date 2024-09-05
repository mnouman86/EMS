using CleanArc.Application.Features.SearchHotel.Queries.GetAllSearchHotels;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.CarRentalSearchFilter.Queries.GetAllCarRentalSearchFilter;

public record GetAllCarRentalSearchFilterQuery(CarRentalSearchFilterRequest carRentalSearchFilterRequest) : IRequest<OperationResult<List<GetAllCarRentalSearchFilterQueryResult>>>;

