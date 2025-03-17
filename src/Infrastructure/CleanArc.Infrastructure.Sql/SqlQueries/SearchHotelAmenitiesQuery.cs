using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class SearchHotelAmenitiesQuery
    {
        public static string GetByHotelID_HotelAmenities => "usp_GetByHotelID_HotelAmenityMapping";
    }
}
