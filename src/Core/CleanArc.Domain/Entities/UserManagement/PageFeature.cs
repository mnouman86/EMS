using CleanArc.Domain.Common;

namespace CleanArc.Domain.Entities.UserManagement;
/// <summary>
/// Represents a page feature entity in the user management domain.
/// </summary>
public class PageFeature
{
    /// <summary>
    /// Gets or sets the unique identifier for the page feature.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the page feature.
    /// </summary>
    public string FeatureName { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier code for the page feature.
    /// </summary>
    public string FeatureCode { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the associated URL.
    /// </summary>
    public int URLId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the page feature is marked as deleted.
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Gets or sets the user ID who created the page feature.
    /// </summary>
    public int CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the user ID who last updated the page feature.
    /// </summary>
    public int UpdatedBy { get; set; }
}