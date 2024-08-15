using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class ActivityPrivateParticipantQueries
{
    public static string Create_ActivityPrivateParticipant => "Create_PrivateParticipants";
    public static string update_ActivityPrivateParticipant => "update_PrivateParticipants";
    public static string Delete_ActivityPrivateParticipant => "Delete_PrivateParticipants";
    public static string GetAll_PrivateParticipants => "GetAll_PrivateParticipants";
    public static string GetByID_PrivateParticipants => "GetByID_PrivateParticipants";


}
