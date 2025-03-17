using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class ActivityPrivateParticipantQueries
{
    public static string Create_ActivityPrivateParticipant => "usp_Create_PrivateParticipants";
    public static string Update_ActivityPrivateParticipant => "usp_update_PrivateParticipants";
    public static string Delete_ActivityPrivateParticipant => "usp_Delete_PrivateParticipants";
    public static string GetAll_PrivateParticipants => "usp_GetAll_PrivateParticipants";
    public static string GetByID_PrivateParticipants => "usp_GetByID_PrivateParticipants";


}
