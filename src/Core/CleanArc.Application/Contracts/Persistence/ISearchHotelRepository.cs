using CleanArc.Application.Common;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.AgeType;
using CleanArc.Domain.Entities.SearchHotelDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence
{
    public interface ISearchHotelRepository //: IRepository<SearchHotelDetail>
    {
        Task<SingleResponseWrapper<SearchHotelDetail>> GetAllAsync(CustomizedSearchRequest searchRequest);
    }
}
