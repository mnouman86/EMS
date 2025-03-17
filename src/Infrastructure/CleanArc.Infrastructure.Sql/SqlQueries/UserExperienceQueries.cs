using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class UserExperienceQueries
{
    public static string Create_UserExperience => "usp_Create_UserExperienceMapping";
    public static string Update_UserExperience => "usp_Update_UserExperienceMapping";
    public static string Delete_UserExperience => "usp_Delete_UserExperienceMapping";
    public static string GetAll_UserExperience => "Usp_GetAll_UserExperienceMapping";
    public static string GetByID_UserExperience => "usp_GetByID_UserExperienceMapping";


}
