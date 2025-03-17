using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class AmenityQueries
{
    public static string Create_Amenity => "usp_Create_Amenity";
    public static string Update_Amenity => "usp_Update_Amenity";
    public static string Delete_Amenity => "usp_Delete_Amenity";
    public static string GetALL_Amenity => "usp_GetALL_Amenity";
    public static string GetByID_Amenity => "usp_GetByID_Amenity";
}
