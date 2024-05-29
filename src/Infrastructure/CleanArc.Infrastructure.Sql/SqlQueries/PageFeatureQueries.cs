using System.Diagnostics.CodeAnalysis;

namespace CleanArc.Infrastructure.Sql;
/// <summary>
/// Static class containing SQL queries related to PageFeatures.
/// </summary>
[ExcludeFromCodeCoverage]
public static class PageFeatureQueries
{
    /// <summary>
    /// SQL query for retrieving all PageFeatures.
    /// </summary>
    public static string AllPageFeatures => "usp_GetAllPageFeature";

    /// <summary>
    /// SQL query for retrieving a PageFeature by its unique identifier.
    /// </summary>
    public static string PageFeatureById => "usp_GetAllPageFeatureByID";

    /// <summary>
    /// SQL query for adding a new PageFeature.
    /// </summary>
    public static string AddPageFeature => @"Create_PageFeature";

    /// <summary>
    /// SQL query for updating a PageFeature.
    /// </summary>
    public static string UpdatePageFeature => "Update_PageFeature";

    /// <summary>
    /// SQL query for deleting a PageFeature.
    /// </summary>
    public static string DeletePageFeature => "Delete_PageFeature";
}
