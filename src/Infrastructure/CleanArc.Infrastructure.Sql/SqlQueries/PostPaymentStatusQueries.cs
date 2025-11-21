
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class PostPaymentStatusQueries
{
    public static string Create_PostPaymentStatus => "usp_Create_PostPaymentStatus";
    public static string Update_PostPaymentStatus => "usp_Update_PostPaymentStatus";
    public static string Delete_PostPaymentStatus => "usp_Delete_PostPaymentStatus";
    public static string GetAll_PostPaymentStatus => "usp_GetAll_PostPaymentStatus";
    public static string GetByID_PostPaymentStatus => "usp_GetByID_PostPaymentStatus";


}
