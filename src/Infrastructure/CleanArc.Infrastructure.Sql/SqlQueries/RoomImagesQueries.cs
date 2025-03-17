using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class RoomImagesQueries
{
    public static string GetByHotelID_RoomImages => "usp_GetByHotelID_RoomImages";
  
}
