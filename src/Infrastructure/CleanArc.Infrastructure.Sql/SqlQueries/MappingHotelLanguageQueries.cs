using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class MappingHotelLanguageQueries
{
    public static string Create_Mapping_HotelLanguages => "usp_Create_LanguageMapping";
    public static string Update_Mapping_HotelLanguages => "usp_Update_LanguageMapping";
}
