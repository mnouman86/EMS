using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class FAQsQueries
{
    public static string Create_FAQs => "usp_Create_FAQs";
    public static string Update_FAQs => "usp_update_FAQs";
    public static string Delete_FAQs => "usp_Delete_FAQs";
    public static string GetAll_FAQs => "usp_GetAll_FAQs";
    public static string GetByID_FAQs => "usp_GetByID_FAQs";


}
