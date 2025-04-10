using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class ActivityPricePerParticipantQueries
{
    public static string Create_ActivityPricePerParticipant => "Create_ActivityPricePerParticipant";
    public static string Activity_Update_ActivityIDPerParticiption => "usp_Update_PerParticipantPrice";
    public static string Delete_ActivityPricePerParticipant => "Delete_ActivityPricePerParticipant";
    public static string GetAll_ActivityPricePerParticipant => "usp_GetAll_ActivityPricePerParticipant";
    public static string GetByID_ActivityPricePerParticipant => "usp_GetByID_ActivityPricePerParticipant";


}
