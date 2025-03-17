using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class SearchFilterThingsToDoQueries
{
    public static string Create_SearchFilterThingsToDo => "Create_SearchFilterThingsToDo";
    public static string Update_SearchFilterThingsToDo => "update_SearchFilterThingsToDo";
    public static string Delete_SearchFilterThingsToDo => "Delete_SearchFilterThingsToDo";
    public static string GetAll_SearchFilterThingsToDo => "usp_GetAll_ThingsToDoSearchFilter";
    public static string GetByID_SearchFilterThingsToDo => "GetByID_SearchFilterThingsToDo";


}
