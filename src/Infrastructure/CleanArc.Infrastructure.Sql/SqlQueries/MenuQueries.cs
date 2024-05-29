using System.Diagnostics.CodeAnalysis;

namespace CleanArc.Infrastructure.Sql;
/// <summary>
/// Static class containing SQL queries related to Menus.
/// </summary>
[ExcludeFromCodeCoverage]
public static class MenuQueries
{
    /// <summary>
    /// SQL query for retrieving all Menus.
    /// </summary>
    public static string AllMenus => "usp_GetAllMenu";

    /// <summary>
    /// SQL query for retrieving a Menu by its unique identifier.
    /// </summary>
    public static string MenuById => "usp_GetAllMenuByID";

    /// <summary>
    /// SQL query for adding a new Menu.
    /// </summary>
    public static string AddMenu => @"Create_Menu";

    /// <summary>
    /// SQL query for updating a Menu.
    /// </summary>
    public static string UpdateMenu => "Update_Menu";

    /// <summary>
    /// SQL query for deleting a Menu.
    /// </summary>
    public static string DeleteMenu => "Delete_Menu";
}
