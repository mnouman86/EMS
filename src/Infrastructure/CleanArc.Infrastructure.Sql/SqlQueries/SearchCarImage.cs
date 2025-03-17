using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class SearchCarImageQueries
    {
        public static string GetByCarID_CarImage => "usp_GetByCarID_CarImage";
    }
}
