using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class GroupActivityParticipantsQueries
{
    public static string Create_GroupActivityParticipants => "usp_Create_GroupActivityParticipant";
    public static string Update_GroupActivityParticipants => "usp_Update_GroupActivityParticipant";
    public static string Delete_GroupActivityParticipants => "usp_Delete_GroupActivityParticipant";
    public static string GetAll_GroupActivityParticipants => "usp_GetAll_GroupActivityParticipant";
    public static string GetByID_GroupActivityParticipants => "usp_GetByID_GroupActivityParticipant";


}
