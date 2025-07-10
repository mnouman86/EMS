using CleanArc.Domain.Entities.SearchHotelDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Mappers
{
    //Interface for all response mappers
    public interface IHotelResponseMapper<T>
    {
        List<SearchDetail> Map(T source, int? noOfRooms, int? noOfDays);
    }
}
