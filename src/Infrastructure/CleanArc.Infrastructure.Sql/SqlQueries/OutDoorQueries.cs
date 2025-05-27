using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class OutDoorQueries
    {
        public static string Create_OutDoor => "usp_Create_OutDoor";
        public static string Update_OutDoor => "usp_Update_OutDoor";
        public static string Delete_OutDoor => "usp_Delete_OutDoor";
        public static string GetALL_OutDoor => "usp_GetALL_OutDoor";
        public static string GetByID_OutDoor => "usp_GetByID_OutDoor";
    }
}
