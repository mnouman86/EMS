using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.SearchHotelDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Providers
{
    //Interface for any hotel provider
    public interface IHotelProvider
    {
        string ProviderName { get; }
        Task<List<SearchDetail>> SearchHotelsAsync(CustomizedSearchRequest request);
    }
}
