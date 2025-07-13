using CleanArc.Application.Contracts.Mappers;
using CleanArc.Domain.Entities.SearchHotelDetail;
using CleanArc.Infrastructure.Persistence.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CleanArc.Infrastructure.Persistence.Providers.BookingWhizz
{
    //Implements IHotelResponseMapper<XDocument>
    public class BookingWhizzResponseMapper : IHotelResponseMapper<XDocument>
    {
        public List<SearchDetail> Map(XDocument source, int? noOfRooms,int? noOfDays)
        {
            if (source == null) return new List<SearchDetail>();

            return source.Descendants("result")
                         .Select(x => BookingWhizzHotelMapper.MapHotelFromXml(x, noOfRooms, noOfDays))
                         .Where(h => h != null)
                         .ToList();
        }
    }
}
