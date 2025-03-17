using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class SearchCountryCitiesQueries
    {
        public static string GetALL_Cities => "usp_GetAll_CitytoCountryID";
    }
}

