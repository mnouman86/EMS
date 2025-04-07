using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class GenericAddressQueries
{
    public static string Create_GenericAddress => "usp_Create_GenericAddress";
    public static string Update_GenericAddress => "usp_Update_GenericAddress";
    public static string Delete_GenericAddress => "usp_Delete_GenericAddress";
    public static string GetAll_GenericAddress => "usp_GetAll_GenericAddress";
    public static string GetByID_GenericAddress => "usp_GetByID_GenericAddress";


}
