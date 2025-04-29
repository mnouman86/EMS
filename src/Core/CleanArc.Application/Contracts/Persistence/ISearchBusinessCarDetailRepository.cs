using CleanArc.Application.Common;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.SearchBusinessCarDetail;
using CleanArc.Domain.Entities.SearchHotelDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public interface ISearchBusinessCarDetailRepository//:IRepository<SearchCarDetail>
{
    Task<SingleResponseWrapper<SearchCarDetail>> GetAllAsync(CarSearchRequest searchRequest);
}
