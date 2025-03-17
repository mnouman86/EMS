using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class SearchHotelImageQueries
    {
        public static string GetByHotelID_HotelImage => "usp_GetAll_Media";
        
    }
}
