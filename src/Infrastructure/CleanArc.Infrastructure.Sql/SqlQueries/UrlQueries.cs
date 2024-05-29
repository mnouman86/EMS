using System.Diagnostics.CodeAnalysis;

namespace CleanArc.Infrastructure.Sql;
/// <summary>
/// Static class containing SQL queries related to URLs.
/// </summary>
[ExcludeFromCodeCoverage]
public static class UrlQueries
{
    /// <summary>
    /// SQL query for retrieving all URLs.
    /// </summary>
    public static string AllUrls => "usp_GetAllURL";

    /// <summary>
    /// SQL query for retrieving a URL by its unique identifier.
    /// </summary>
    public static string UrlById => "usp_GetAllURLByID";

    /// <summary>
    /// SQL query for adding a new URL.
    /// </summary>
    public static string AddUrl => @"Create_Url";

    /// <summary>
    /// SQL query for updating a URL.
    /// </summary>
    public static string UpdateUrl => "Update_URL";

    /// <summary>
    /// SQL query for deleting a URL.
    /// </summary>
    public static string DeleteURL => "Delete_URL";
}
