using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Flights;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Flights.Queries
{
    public record GetFlightsQuery(FlightSearchRequestDto Request) : IRequest<OperationResult<FlightSearchResultDto>>;

}
