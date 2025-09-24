using CleanArc.Application.Models.Flights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence
{
    public interface ILookupService
    {
        Task<IReadOnlyList<AirportDto>> GetAirportsAsync(CancellationToken ct = default);
        Task<AirlineInfoDto?> GetAirlineInfoAsync(string airlineCode, CancellationToken ct = default);
    }

}
