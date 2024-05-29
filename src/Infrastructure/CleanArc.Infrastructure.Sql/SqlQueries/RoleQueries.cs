using System.Diagnostics.CodeAnalysis;

namespace CleanArc.Infrastructure.Sql;
/// <summary>
/// Static class containing SQL queries related to Roles.
/// </summary>
[ExcludeFromCodeCoverage]
public static class RoleQueries
{
    /// <summary>
    /// SQL query for retrieving all Roles.
    /// </summary>
    public static string AllRoles => "usp_GetAllRole";

    /// <summary>
    /// SQL query for retrieving a Role by its unique identifier.
    /// </summary>
    public static string RoleById => "usp_GetAllRoleByID";

    /// <summary>
    /// SQL query for adding a new Role.
    /// </summary>
    public static string AddRole => @"Create_Role";

    /// <summary>
    /// SQL query for updating a Role.
    /// </summary>
    public static string UpdateRole => "Update_Role";

    /// <summary>
    /// SQL query for deleting a Role.
    /// </summary>
    public static string DeleteRole => "Delete_Role";
}
