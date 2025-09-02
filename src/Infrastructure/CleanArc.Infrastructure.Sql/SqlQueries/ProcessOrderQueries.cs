
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class ProcessOrderQueries
{
    public static string Create_PackageDetail => "usp_Create_PackageDetail";
    public static string Create_OrderPayment => "usp_Create_GeneralOrderDetail";
    public static string Update_OrderPayment => "usp_Update_GeneralOrderDetailStatus";
    public static string Delete_OrderPayment => "usp_Delete_GeneralOrderDetailStatus";
    public static string GetAll_OrderPayment => "usp_GetAll_GeneralOrderDetail";
    public static string GetByID_OrderPayment => "usp_GetByID_GeneralOrderDetail";


}
