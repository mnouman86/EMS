using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class SearchActivityDetailQueries
    {
        public static string GetAllByBusinessID_Activities => "usp_GetAllByBusinessID_Activities";
    }
}

