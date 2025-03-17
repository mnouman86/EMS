using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class HomeSliderQueries
{
    public static string Create_HomeSlider => "usp_Create_HomeSlider";
    public static string Update_HomeSlider => "usp_Update_HomeSlider";
    public static string Delete_HomeSlider => "usp_Delete_HomeSlider";
    public static string GetALL_HomeSlider => "usp_GetAll_HomeSlider";
    public static string GetByID_HomeSlider => "usp_GetByID_HomeSlider";

}
