using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class PackageDetailQueries
{
    public static string Create_PackageDetail => "usp_Create_PackageDetail";
    public static string Update_PackageDetail => "usp_Update_PackageDetail";
    public static string Delete_PackageDetail => "usp_Delete_PackageDetail";
    public static string GetAll_PackageDetail => "usp_GetAll_PackageDetail";
    public static string GetByID_PackageDetail => "usp_GetByID_PackageDetail";


}
